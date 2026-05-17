CREATE TABLE IF NOT EXISTS gamesapp_meta (
    id SERIAL PRIMARY KEY,
    name VARCHAR(128) NOT NULL UNIQUE,
    value TEXT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT (NOW())
);

INSERT INTO gamesapp_meta (name, value) VALUES ('schema', '001_initial')
ON CONFLICT (name) DO NOTHING;
