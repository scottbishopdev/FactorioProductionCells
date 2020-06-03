namespace FactorioProductionCells.Domain.Enums
{
    // TODO: I'm using Guids everywhere else for identifiers, but I'm not sure if there's a good way to do so here with enums since we won't have the Guid until the entities are created in the db.
    public enum DependencyComparisonTypeId : int
    {
        LessThan = 0,
        LessThanOrEqualTo = 1,
        EqualTo = 2,
        GreaterThan = 3,
        GreaterThanOrEqualTo = 4
    }
}
