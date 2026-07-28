# Build context is the gamesapp repository root.
FROM node:20-alpine AS build
WORKDIR /app

COPY src/services/ui/package.json src/services/ui/package-lock.json ./
RUN npm ci

COPY src/services/ui/ ./
RUN npm run build -- --configuration=local

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/dist/ui/browser ./
COPY src/services/ui/.docker/local/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
