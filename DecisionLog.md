Currently, this space is just being used as a ToDo list combines with a list of questions we'll have to figure out. Soon, these list items will be converted into proper decision log.

Welcome to any GitHub archaeologists! :P

# Open questions and ideas
* Should I make an enum to indicate whether something comes from the base game or a mod?
* How are we going to indicate whether or not an item/machine/recipe/module comes from a mod? Do we link to the mod itself, or the mod_release?
* If we store items/machines/recipes/modules based on mod_release, we'll end up with a lot of duplicate rows. Is there a way to only add new records when a mod version changes? We'd really have to trust that mod makers are using semantic versioning for this to work. Alternatively, we could maintain our own version order value for releases, but that doesn't seem ideal.
* How will we be able to tell when a mod overwrites something from the base game?
* Need to figure out how to allow production cells inside of productions cells
* Need to figure out how to handle production cell input/output quantities. Technically these are calculated values so we don't need to store them, but this could get dangerous based on the recursive nature of production cells.

# ToDo  List
* Need to add unique identifier to item.name.
* Need to add descriptions and instructions for config files in README.
* See if there's a way to use a constraint on production_cell_recipe to ensure that it only stores valid recipe/machine combinations per recipe_valid_machine
* Postgres will skip database initialization if the volume (and thus database) from the previous container still exists. This means that we need to ensure that either the database or volume from the previous container is dropped before startup (or at container shutdown).
* Create enum to indicate whether an item is a item or a fluid and add a column on the item table. Also, find out if there's any reason that these may need to be separate tables (e.g. unique properties).
* Look into whether or not we need the ability to link an item to also being a machine (e.g. Electric Furnace is a machine, but also used to create production science packs).