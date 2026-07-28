CREATE TABLE apps.Products (
    Id              SERIAL PRIMARY KEY,
    Name            VARCHAR(100) NOT NULL,
    Description     VARCHAR(100) NOT NULL,
    Price           NUMERIC(10, 2) NOT NULL,
    CreatedAt       TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);