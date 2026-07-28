CREATE OR REPLACE FUNCTION GeByIdProduct(@ProductId INT)
RETURNS TABLE (
    ID              SERIAL,
    Name            VARCHAR(100),
    Description     VARCHAR(100),
    Price           NUMERIC(10, 2),
    CreatedAt       TIMESTAMP
) AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM apps.Products
    WHERE Id = @ProductId;
END;
$$ LANGUAGE plpgsql;