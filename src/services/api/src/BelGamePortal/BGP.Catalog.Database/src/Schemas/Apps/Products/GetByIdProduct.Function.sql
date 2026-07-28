


CREATE OR REPLACE FUNCTION apps.GetByIdProduct(ProductId INT)
RETURNS SETOF apps.Products AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM apps.Products
    WHERE Id = ProductId;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Product with ID % does not exist', product_id;
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        RAISE NOTICE 'An error occurred: %', SQLERRM;
        RETURN;

END;
$$ LANGUAGE plpgsql;
