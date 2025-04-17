-- NOTE you will need to create this table today
-- you must send a GET request to the accounts endpoint with your bearer token in order to add your user to the sql database
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';
CREATE TABLE cars(
  -- NOTE make sure your id column is the first column you define
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  make TINYTEXT NOT NULL,
  model TINYTEXT NOT NULL,
  year INT UNSIGNED NOT NULL,
  color TINYTEXT NOT NULL,
  price MEDIUMINT UNSIGNED NOT NULL,
  mileage MEDIUMINT UNSIGNED NOT NULL,
  engine_type ENUM(
    'small',
    'medium',
    'large',
    'super-size',
    'battery'
  ),
  img_url TEXT NOT NULL,
  has_clean_title BOOLEAN NOT NULL DEFAULT true,
  creator_id VARCHAR(255) NOT NULL,
  -- NOTE this will validate that an actual id for an account was used when INSERTING a car into the data base
  -- this will also delete an accounts created cars if the user deletes their account
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);
INSERT INTO cars (
    make,
    model,
    year,
    price,
    color,
    mileage,
    engine_type,
    img_url,
    has_clean_title,
    creator_id
  )
VALUES (
    'honda',
    's2000',
    2008,
    20000,
    'silver',
    200000,
    'medium',
    'https://images.unsplash.com/photo-1723407338018-709fbf9ed494?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fHMyMDAwfGVufDB8fDB8fHww',
    false,
    '670ff93326693293c631476f'
  );
CREATE TABLE IF NOT EXISTS houses (
  id INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
  sqft MEDIUMINT UNSIGNED NOT NULL,
  bedrooms TINYINT UNSIGNED NOT NULL,
  bathrooms DOUBLE NOT NULL,
  img_url VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  price INT NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  has_pool BOOLEAN NOT NULL DEFAULT false,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);
INSERT INTO houses (
    sqft,
    bedrooms,
    bathrooms,
    img_url,
    description,
    price,
    has_pool,
    creator_id
  )
VALUES (
    2000,
    3,
    2,
    'https://images.unsplash.com/photo-1572120360610-d971b9d7767c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D',
    'Lot''s of surrounding trails and forest destinations nearby. The house is on an quiet road and has a 1/4 acre yard, no HOA.',
    358000,
    false,
    '67e1d5c3643b99e3ead7997b'
  );

-- ALL HOUSES

SELECT houses.*,
  accounts.*
FROM houses
  INNER JOIN accounts ON accounts.id = houses.creator_id;

--HOUSE BY ID
SELECT houses.*, accounts.*
FROM houses
  INNER JOIN accounts ON accounts.id = houses.creator_id
WHERE houses.id = 3;


-- NOTE JOIN is how we include multiple rows of data on the same row
-- INNER JOIN denotes that there must be a match between the two columns, or no data is returned
-- ON tells our database when to match up data, otherwise it will match everything to everything
SELECT *
FROM accounts;
SELECT *
FROM cars;
SELECT cars.*,
  accounts.*
FROM cars
  INNER JOIN accounts ON accounts.id = cars.creator_id;

SELECT cars.*,
  accounts.*
FROM cars
  INNER JOIN accounts ON accounts.id = cars.creator_id
WHERE cars.id = 3;
UPDATE cars
SET make = "mazda",
  model = "miata"
WHERE id = 5
LIMIT 1;