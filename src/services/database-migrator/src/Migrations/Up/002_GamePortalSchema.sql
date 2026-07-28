CREATE TABLE IF NOT EXISTS game_definitions (
    id SERIAL PRIMARY KEY,
    code VARCHAR(64) NOT NULL UNIQUE,
    name VARCHAR(256) NOT NULL,
    description TEXT,
    image_url TEXT,
    image_alt TEXT,
    game_page_app_url VARCHAR(256)
);

CREATE TABLE IF NOT EXISTS game_sessions (
    id SERIAL PRIMARY KEY,
    game_definition_id INT NOT NULL REFERENCES game_definitions (id) ON DELETE CASCADE,
    title VARCHAR(256) NOT NULL,
    status VARCHAR(32) NOT NULL DEFAULT 'open',
    created_at TIMESTAMPTZ NOT NULL DEFAULT (NOW())
);

CREATE TABLE IF NOT EXISTS game_lobbies (
    id SERIAL PRIMARY KEY,
    game_definition_id INT NOT NULL REFERENCES game_definitions (id) ON DELETE CASCADE,
    name VARCHAR(256) NOT NULL,
    is_open BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMPTZ NOT NULL DEFAULT (NOW())
);

INSERT INTO game_definitions (code, name, description, image_url, image_alt, game_page_app_url)
VALUES
    ('kroko', 'Kroko', 'Draw and go go', 'assets/images/brushes-paint-and-palletes.png', 'Kroko', 'kroko'),
    ('geo-guesser', 'GeoGuesser', 'Find a belarussian place', 'assets/images/piece-of-papper-map.png', 'GeoGuesser', 'geo-guesser'),
    ('phaser-test', 'PhaserTest', 'Phaser test game', 'assets/images/piece-of-papper-map.png', 'PhaserTest', 'phaser-test')
ON CONFLICT (code) DO NOTHING;

INSERT INTO game_sessions (game_definition_id, title, status)
SELECT gd.id, gd.name || ' demo session', 'open'
FROM game_definitions gd
WHERE NOT EXISTS (
    SELECT 1 FROM game_sessions s WHERE s.game_definition_id = gd.id
);

INSERT INTO game_lobbies (game_definition_id, name, is_open)
SELECT gd.id, gd.name || ' lobby', TRUE
FROM game_definitions gd
WHERE NOT EXISTS (
    SELECT 1 FROM game_lobbies l WHERE l.game_definition_id = gd.id
);
