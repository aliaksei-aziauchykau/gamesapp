


CREATE OR REPLACE FUNCTION apps.GetAllProducts()
RETURNS SETOF apps.Products AS $$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM apps.Products;

    IF NOT FOUND THEN
        RAISE EXCEPTION 'Products not found';
    END IF;

EXCEPTION
    WHEN OTHERS THEN
        RAISE NOTICE 'An error occurred: %', SQLERRM;
        RETURN;

END;
$$ LANGUAGE plpgsql;
