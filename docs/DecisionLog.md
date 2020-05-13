Decision Log
=====

Summary
-----
This document is intended to provide a level of transparency into the decision making process for this project by documenting decisions that have been made so far, as well as providing some context for those decisions.

# How should we indicate whether or not an item/machine/etc. came from the base game or a mod?
![Generic badge](https://img.shields.io/badge/Status-Open-red.svg)

## Context and Problem Statement
We need to be able to differentiate between entities (e.g. items, machines, etc.) that come from the base Factorio game and those that come from mods.

## Considered Options
* Create an enum containing the options "Base Game" and "Mod", and 
* Leave any mod-related fields with NULL values for entities from the base game.

## Decision Outcome
Chosen option: N/A, because:

* 
* 

# Should we store duplicate entities for *every* individual release of the base game and mods?
![Generic badge](https://img.shields.io/badge/Status-Open-red.svg)

## Context and Problem Statement
The data we store representing entities from the base game and mods needs to be versioned in order to maintain fidelity with the actual game's behavior. The properties of most entities (e.g. crafting speed of machines, bonuses for modules) won't change very often, so creating new records for every version of the game/mod that is released could result in a *lot* of redundant data. On the other hand, linking entities to specific releases will increase the complexity of the system, and may make us dependent on mod authors using accurate versioning systems (e.g. proper semver).

## Considered Options
* Create a new record for every entity each time the base game or a mod is updated. This would use a *lot* of extra storage space.
* Tie entities to multiple releases of the base game or mods, creating a new entity whenever we detect a change. from one version to another.

## Decision Outcome
Chosen option: N/A, because:

* 
* 

# Should we link entities to a mod, or a mod_release?
![Generic badge](https://img.shields.io/badge/Status-Open-red.svg)

## Context and Problem Statement
Similar to the previous decision, we need to determine whether entities should be linked directly to a mod, or if we should link them through a mod_release record.
**Note:** This decision needs take the result of the decision "Should we store duplicate entities for *every* individual release of the base game and mods?" into consideration.

## Considered Options
* Link entities directly to a mod record.
* Link entities to a mod via a mod_release record.

## Decision Outcome
Chosen option: N/A, because:

* 
* 

# Should we link entities to a mod, or a mod_release?
![Generic badge](https://img.shields.io/badge/Status-Open-red.svg)

## Context and Problem Statement
Similar to the previous decision, we need to determine whether entities should be linked directly to a mod, or if we should link them through a mod_release record.
**Note:** This decision needs take the result of the decision "Should we store duplicate entities for *every* individual release of the base game and mods?" into consideration.

## Considered Options
* Link entities directly to a mod record.
* Link entities to a mod via a mod_release record.

## Decision Outcome
Chosen option: N/A, because:

* 
* 
