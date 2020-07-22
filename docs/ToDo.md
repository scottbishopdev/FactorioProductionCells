ToDo  List
=====

* Look into how mod changes are applied to the base game. Do they overwrite specific recipes and entities, or simply exclude the originals and inject their own?
* Look into whether or not we should create some kind of base "item space" or "project" layer, which would allow users to define specific mods and versions within just that "item space" or "project".
* Look into the entity that we're going to use for production cells, and whether or not it would benefit from storing calculated values for the items/sec for inputs and outputs.
* The webserver and worker services should probably have some kind of retry strategy defined for database and message queue interactions. Look into Polly for this.
* Figure out if it's ok to include validators in the Domain layer like I have. It seems like the right place to validate an Entity object, as a given entity may be references in commands for a different entity in the Application layer.
* Back up the mappings that we're allowing AutoMapper to perform with unit tests.
* In fact, just write any tests at all. That would be great.
* Look into System.Globalization.CultureInfo and see if there's a way I can utilize a full culture for a user, instead of just a default language.
* ModUpdateWorker needs to validate that the mod it's working on actually needs to be added/updated.
* The application as a whole depends on this seed data. Also, Most things are supposed to interact with the database via the Application layer, right? Finally, the only reason that the Infrastructure layer (specifically, the dbContext) needs to be dependent on the the CurrentUserService is so it can populate the audit fields when data is saved, and the only thing mandating *that* dependency is the fact that our data seeding is done down in the Infrastructure layer. Could we eliminate the whole DbContext -> CurrentUserService -> UserManager -> DbContext circular dependency issue by moving both the data seeding and population of audit fields up to the Application layer?
* Look into whether or not DbSets for entities that don't need to be directly queries need to exist on the DbContext. I'm worried that schema changes might not be picked up for those entities if I remove them.
* Determine if there's a way to *safely* execute Lua scripts from mods on the server side. If this isn't possible, is there a safe way to execute them on the clients?
* Look at Web Storage API for client-side storage of user data without the need for a login/account.
* Look into whether or not a maliciously constructed mod name could be used as a vector for some kind of injection attack.
* Look into FluentValidation to see if wer can replace the existing validation classes in the Domain layer.
* See if there's a good way to either combine the various string generation methods in the test classes, or a method in an existing library that can easily create strings for us.
* Integration Test Ideas:
  * Make sure Database starts with default users
  * Make sure Database starts with default language
  * Ensure messages can be added to RabbitMQ
