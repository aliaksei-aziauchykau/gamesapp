DO $$
BEGIN
    CREATE TABLE IF NOT EXISTS apps.Products (
        Id              SERIAL PRIMARY KEY,
        Name            VARCHAR(100) NOT NULL,
        Description     VARCHAR(100) NOT NULL,
        Price           NUMERIC(10, 2) NOT NULL,
        CreatedAt       TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

EXCEPTION
    WHEN duplicate_table THEN
        RAISE NOTICE 'Table products already exists.';

END $$;

