ToDo  List
=====

* Look into how mod changes are applied to the base game. Do they overwrite specific recipes and entities, or simply exclude the originals and inject their own?
* Look into how we can allow production cells to reference other production cells within themselves.
* Look into whether or not we should create some kind of base "item space" or "project" layer, which would allow users to define specific mods and versions within just that "item space" or "project".
* Look into the schema that we're going to use for production cells, and whether or not it would benefit from storing calculated values for the items/sec for inputs and outputs.
* The webserver should probably have some kind of retry strategy defined. Look into Polly for this.
* What's a Value Object, and should I be using any for my entities??
* Figure out if it's ok to include validators in the Domain layer like I have. It seems like the right place to validate an Entity object, as a given entity may be references in commands for a different entity in the Application layer.
* Back up the mappings that we're allowing AutoMapper to perform with unit tests.
* In fact, just write any tests at all. That would be great.
* Need to do a deep dive into MediatR, and figure out how it plays into everything in the Application layer. I believe that it has something to do with request handling and processing.
* Figure out whether or not all commands and queries in the Application Layer should return a view model. If my worker services are going to be using those commands and queries, it seems kind of silly that something with no display concerns is going to consume a response that's specifically structured for display.
* Domain is
  * Entities = core units that business logic uses and interacts with and their validation
* Application is
  * Use Cases - All the ways that the user might want to interact with the entities. Some use cases might return some information in the form of a model (view model or otherwise), and some might not need to(though it's rare).
  * View models are a way that we can deliver a tidy package of data to an app that's responsible for showing it to a user.
* Webapp is
  * An application that allows users to perform those use cases via a web-based interface
* Worker Service is
  * We can imagine a worker service as an automated user. It still interacts with the system through use cases and has an identity of its own, but it just doesn't need a UI to do any of it.
* Figure out a better way to structure the validation we're going in the entity constructors.
* Look into the Database.EnsureCreated() method, which may be useful for seeding the database with necessary data (e.g. a default language of english, user accounts for the ModUpdater services, etc.).
