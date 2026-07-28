DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.schemata
        WHERE schema_name = 'apps'
    ) THEN
        EXECUTE 'CREATE SCHEMA apps';
    END IF;
END $$;