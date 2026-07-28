import { APP_BASE_HREF } from '@angular/common';
import { CommonEngine } from '@angular/ssr';
import { ThemeToken } from '@itdl/game-portal/ui';
import cookieParser from 'cookie-parser';
import express from 'express';
import * as fs from 'node:fs';
import * as http from 'node:http';
import * as https from 'node:https';
import { dirname, join, resolve } from 'node:path';
import { fileURLToPath } from 'node:url';
import bootstrap from './src/main.server';

// The Express app is exported so that it can be used by serverless Functions.
export function app(): express.Express {
    const server = express();
    const serverDistFolder = dirname(fileURLToPath(import.meta.url));
    const browserDistFolder = resolve(serverDistFolder, '../browser');
    const indexHtml = join(serverDistFolder, 'index.server.html');

    const commonEngine = new CommonEngine();

    server.use(cookieParser());

    server.set('view engine', 'html');
    server.set('views', browserDistFolder);

    // Example Express Rest API endpoints
    // server.get('/api/**', (req, res) => { });
    // Serve static files from /browser
    server.get(
        '**',
        express.static(browserDistFolder, {
            maxAge: '1y',
            index: 'index.html',
        }),
    );

    // All regular routes use the Angular engine
    server.get('**', (req, res, next) => {
        const { protocol, originalUrl, baseUrl, headers, cookies } = req;
        console.log('cookies', cookies);
        commonEngine
            .render({
                bootstrap,
                documentFilePath: indexHtml,
                url: `${protocol}://${headers.host}${originalUrl}`,
                publicPath: browserDistFolder,
                providers: [
                    { provide: APP_BASE_HREF, useValue: baseUrl },
                    { provide: ThemeToken, useValue: cookies['bgp.theme'] },
                ],
            })
            .then((html) => res.send(html))
            .catch((err) => next(err));
    });

    return server;
}

function run(): void {
    const serverDistFolder = dirname(fileURLToPath(import.meta.url));

    const envs = getEnvVariablesWithPrefix('UI_');

    const port = process.env['PORT'] || envs['port'] || 4000;
    const securePort = process.env['SECURE_PORT'] || envs['secure_port'] || 4001;

    // Start up the Node server
    const server = app();

    const privateKey = fs.readFileSync(`${serverDistFolder}/ssl/localhost.key`);
    const certificate = fs.readFileSync(`${serverDistFolder}/ssl/localhost.crt`);
    const credentials = { key: privateKey, cert: certificate };

    const httpServer = http.createServer(server);
    const httpsServer = https.createServer(credentials, server);

    httpServer.listen(port, () => {
        console.log(`Node Express server listening on http://localhost:${port}`);
    });

    httpsServer.listen(securePort, () => {
        console.log(`Node Express server listening on https://localhost:${securePort}`);
    });
}

function getEnvVariablesWithPrefix(prefix) {
    const envVars = {};
    for (const key in process.env) {
        if (key.startsWith(prefix)) {
            console.log('find key express', key);
            const transformedKey = toCamelCase(key.slice(prefix.length));
            envVars[transformedKey] = process.env[key];
        }
    }
    return envVars;
}

function toCamelCase(str) {
    return str
        .replace(/_/g, ' ') // Replace underscores with spaces
        .split(' ') // Split the string into words
        .map((word, index) =>
            index === 0 ? word.toLowerCase() : word.charAt(0).toUpperCase() + word.slice(1).toLowerCase(),
        ) // Capitalize each word
        .join(''); // Join words back without spaces
}

run();
