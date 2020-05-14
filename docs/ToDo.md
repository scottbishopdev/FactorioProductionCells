ToDo  List
=====

* See if there's a way to use a constraint on `production_cell_recipe` to ensure that it only stores valid `recipe`/`machine` combinations per `recipe_valid_machine`.
* Create enum to indicate whether an `item` is an actual item or a fluid, and add a column on the `item` table to reference this. Also, find out if there's any reason that these may need to be separate tables (e.g. unique properties).
* Look into whether or not we need the ability to link an `item` to also being a `machine` (e.g. Electric Furnace is a machine, but also used to create production science packs).
* Look into how mod changes are applied to the base game. Do they overwrite specific recipes and entities, or simply exclude the originals and inject their own?
* Look into how we can allow production cells to reference other production cells within themselves.
* Look into whether or not we should create some kind of base "project" layer, which would allow users to define specific mods and versions within just that "project".
* Look into the schema that we're going to use for production cells, and whether or not it would benefit from storing calculated values for the items/sec for inputs and outputs.
* The webserver should probably have some kind of retry strategy defined. Look into Polly for this.