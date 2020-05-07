-- Remove the old version of the schema if it exists, then create a new one. (this could be highly destructive to your data...)
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO public;

-- Add the uuid-ossp extension so we can use uuid_generate_v4() for primary keys.
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

/*------------------------------------------------------------------------------------------
System tables
------------------------------------------------------------------------------------------*/
CREATE TABLE IF NOT EXISTS language
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    english_name VARCHAR(100) NOT NULL,
    language_code VARCHAR(10) NOT NULL, -- The "language_lode" column is composed of the ISO-639 language code, then a hyphen, followed by the ISO-3166 country code e.g. "en-us"
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW()

);

CREATE TABLE IF NOT EXISTS user_account
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    email_address VARCHAR(200),
    username VARCHAR(100) UNIQUE,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    update_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

/*------------------------------------------------------------------------------------------
Item tables
------------------------------------------------------------------------------------------*/
CREATE TABLE IF NOT EXISTS item
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(200) NOT NULL,
    --mod_release UUID REFERENCES mod
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS item_translation
(
    item_id UUID NOT NULL REFERENCES item(id),
    language_id UUID NOT NULL REFERENCES language(id),
    display_name TEXT NOT NULL,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (item_id, language_id)
);

/*------------------------------------------------------------------------------------------
Machine tables
------------------------------------------------------------------------------------------*/
CREATE TABLE IF NOT EXISTS machine
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(200) NOT NULL,
    crafting_speed INT NOT NULL,  -- Double check data type on this...
    module_slots INT NOT NULL,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS machine_translation
(
    machine_id UUID NOT NULL REFERENCES machine(id),
    language_id UUID NOT NULL REFERENCES language(id),
    title VARCHAR(200) NOT NULL,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (machine_id, language_id)
);

/*------------------------------------------------------------------------------------------
Recipe tables
------------------------------------------------------------------------------------------*/
CREATE TABLE IF NOT EXISTS recipe
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(200) NOT NULL,
    crafting_time INT, -- Double check data type on this...
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS recipe_translation
(
    recipe_id UUID NOT NULL REFERENCES recipe(id),
    language_id UUID NOT NULL REFERENCES language(id),
    display_name TEXT NOT NULL,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (recipe_id, language_id)
);

CREATE TABLE IF NOT EXISTS recipe_ingredient
(
    recipe_id UUID NOT NULL REFERENCES recipe(id),
    item_id UUID NOT NULL REFERENCES item(id),
    Quantity int NOT NULL, -- Double check data type on this...
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (recipe_id, item_id)
);

CREATE TABLE IF NOT EXISTS recipe_product
(
    recipe_id UUID NOT NULL REFERENCES recipe(id),
    item_id UUID NOT NULL REFERENCES item(id),
    Quantity INT NOT NULL, -- Double check data type on this...
    Percentage INT, -- Double check data type on this...
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (recipe_id, item_id)
);

CREATE TABLE IF NOT EXISTS recipe_valid_machine
(
    recipe_id UUID NOT NULL REFERENCES recipe(id),
    machine_id UUID NOT NULL REFERENCES machine(id),
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (recipe_id, machine_id)
);

/*------------------------------------------------------------------------------------------
Modules tables
------------------------------------------------------------------------------------------*/
CREATE TABLE IF NOT EXISTS module
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(200) NOT NULL,
    productivity_bonus INT NOT NULL,  -- Double check data type on this...
    speed_bonus INT NOT NULL, -- Double check data type on this...
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS module_translation
(
    module_id UUID NOT NULL REFERENCES module(id),
    language_id UUID NOT NULL REFERENCES language(id),
    title VARCHAR(200) NOT NULL,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (module_id, language_id)
);

/*------------------------------------------------------------------------------------------
Mod tables
------------------------------------------------------------------------------------------*/
CREATE TABLE IF NOT EXISTS mod
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(200) NOT NULL,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    update_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS mod_translation
(
    mod_id UUID NOT NULL REFERENCES mod(id),
    language_id UUID NOT NULL REFERENCES language(id),
    title VARCHAR(200) NOT NULL,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (mod_id, language_id)
);

CREATE TABLE IF NOT EXISTS mod_release
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    mod_id UUID NOT NULL REFERENCES mod(id),
    version varchar(50),
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

/*------------------------------------------------------------------------------------------
Production Cell tables
------------------------------------------------------------------------------------------*/
CREATE TABLE IF NOT EXISTS production_cell
(
    id UUID NOT NULL PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(200) NOT NULL,
    description TEXT,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    update_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS production_cell_recipe
(
    production_cell_id UUID NOT NULL REFERENCES production_cell(id),
    recipe_id UUID NOT NULL REFERENCES recipe(id),
    machine_id UUID NOT NULL REFERENCES machine(id),
    quantity INT NOT NULL DEFAULT 0,
    module_type UUID REFERENCES module(id),
    module_quantity INT NOT NULL DEFAULT 0,
    beacon_module_type UUID REFERENCES module(id),
    beacon_module_quantity INT NOT NULL DEFAULT 0,
    beacon_quantity INT NOT NULL DEFAULT 0,
    add_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (production_cell_id, recipe_id)
);
