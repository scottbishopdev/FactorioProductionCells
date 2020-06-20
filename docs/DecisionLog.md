Decision Log
=====

Summary
-----
This document is intended to provide a level of transparency into the decision making process for this project by documenting decisions that have been made so far, as well as providing some context for those decisions.

# When ModUpdateScheduler is comparing the new mod list to the database, should it do one big query, or many small ones?
![Generic badge](https://img.shields.io/badge/Status-Closed-green.svg)

## Context and Problem Statement
After the ModUpdateScheduler has retrieved the list of current mods from the mod portal, it needs to determine which mods don't exist in the database and need to be created, which exist but need to be updated, and which are already up-to-date. This means that eventually, it's going to have to probe the entire Mod table to determine what it needs to do.

## Considered Options
* **Pull the entire list of mods and releases in a single, large query.** This will result in a longer-running query with a much larger response, but significantly less requests for the database to process. It will also increase the memory demands of the ModUpdateScheduler, as it will basically have to keep the entire Mod table in memory for the process.
* **Loop through every mod that we received from the mod portal and individually query the database for it.** This will result in many more requests, but each one will be small and quick to process. It will also allow the ModUpdateScheduler to complete the processing for a given mod in a single operation, rather than handling adds, then updates separately. It will probably result in higher CPU usage for both the database and the ModUpdateScheduler, but a lower memory footprint.

## Decision Outcome
Chosen option: **Loop through every mod that we received from the mod portal and individually query the database for it.**, because:

* I'm more concerned with how much RAM I'll be using then CPU, and this options has a lower memory footprint.
* Doing the processing in a single step within a loop seems more appealing.


# How should we handle dependencies for sting column lengths on entities?
![Generic badge](https://img.shields.io/badge/Status-Closed-green.svg)

## Context and Problem Statement
In Clean Architecture, it's a rule that all dependencies need to point inward. This rule appears to be violated when it comes to entity validation in the Application layer - specifically for any entity properties that result in a column created with "character varying" data type. Normally, the width of these columns would be defined in the Configuration files in the Infrastructure layer, since that's where we define the exact structure of the database. Unfortunately, the Application layer also needs to be aware of these widths, though, so it can perform validation on entity objects in commands. This means that the command validators in the Application layer have a rather dangerous implicit dependency on Infrastructure, which is bad news.

## Considered Options
* **Manually keep the column widths in both Infrastructure and Application layers in sync.** As long as the schema doesn't change very often, this *shouldn't* be too hard. Seriously, though, who are we kidding? This is just asking for trouble. What happens when the width in the Application layer is accidentally extended, but Infrastructure isn't? Nope. Nope nope nope.
* **Enforce identical widths through tests.** We could conceivably create tests for both the Application and Infrastructure layers that would ensure that the column widths defined in both stay in sync. The dependency will still technically be there, but at long as we're vigilant about running our tests, we shouldn't get into a situation where the two get out of sync.
* **Define the column widths as part of the Domain layer.** Another alternative would be to simply define the expected length of string fields in the entities themselves via the addition of a corresponding ``<PropertyName>Length`` property for each string type property that's intended to be stored. We wouldn't want to save these properties, so any entity using this approach will need to set the ``<PropertyName>Length`` property to be ignored in it's configuration.

## Decision Outcome
Chosen option: **Define the column widths as part of the Domain layer.**, because:

* This way, we have a single source of truth for property/column widths.
* If you think about it, the data types of entity properties are defined in the Domain layer, and since these entities are intended to be stored in a database, the column width is an inherent part of the property, regardless of what type of database it's stored in.
* If column widths ever change, there's no need to update validation, as both the validation checks and messaging can be based off the width property set in the Domain layer.


# How should we store/use/represent a mod/dependency's version information?
![Generic badge](https://img.shields.io/badge/Status-Closed-green.svg)

## Context and Problem Statement
Releases of a mod are always versioned, which is communicated in the format "#.#.#". This format looks a lot like [semantic versioning](semver.org), but since mod versions are defined by mod authors with no explicit requirement to follow that standard, we can't implement functionality based off it. Furthermore, mods also always have a minimum of at least one other release that they are dependent on (all mods are dependent on the base mod), which also include version information.

This means that both our domain entities and persistance solution need to be able store this version information, and that our application code will need to be able to compare versions and dependencies and react accordingly. It would be nice if the method we choose for handling this data provided us some convenience as well, such as being able to compare versions using simple operators (``>``, ``==``, etc.) or methods (``.IsGreaterThan()``, etc.).

## Considered Options
* **Just keep it as a string.** This would be the most simplistic approach, as there would be no need to convert the version values into a format that a database can handle. Unfortunately, this gives us no benefit in terms of comparing versions.
* **Use ``System.Automation.Management.SemanticVersion``.** If we're not going to use a basic string to store versions, then we've already moved into the realm of complex types, so why not use the one that most closely represents the data we're working with? The SemanticVersion class provides almost all of the functionality we need, and is intentionally designed for versions that use the ``<Major>.<Minor>.<Patch>`` format. Unfortunately, this assembly requires us to target a version of the ``netcoreapp`` framework, and we'd like to keep our Domain and Application layers targeting the ``netstandard`` framework if possible.
* **Use ``System.Version``.** This class has the benefit of allowing us to target ``netstandard``, but it unfortunately appears to be designed in a way that is more specific to Microsoft's use of versioning. ``System.Version`` expects versions in the format ``<Major>.<Minor>.<Build>.<Revision>``, and while we *could* repurpose this class to fit our needs, that doesn't quite feel right.
* **Make my own value object to represent versions.** This option has the benefits of both allowing us to target the ``netstandard`` framework, *and* keeping the version information in a format that accurately reflects its nature. Although it feels like reinventing the wheel and will require us to create and maintain more code, we'll be able to implement comparison operators that will make working with this version information *much* easier in the outer layers of our system.

## Decision Outcome
Chosen option: **Make my own value object to represent versions.**, because:

* We get to continue to target ``netstandard`` in our Domain and Application layers.
* We get to store our version information in a manner that accurately fits it.
* We'll have full control over the implementation, allowing us to define comparison operators and methods that will make working with version information very easy.
* The Clean Architecture template has some examples of good implementations of value objects that we can base ours off of.
