namespace FIRSTPROJECT.Domain.Common;

public abstract class BaseEntity//abstract class that represents a base entity with a unique identifier
{
    public Guid Id { get; set; }
}//this abstract class cannot be created as a new instance, but can be inherited by other classes to provide a common property (Id) for all entities in the domain.