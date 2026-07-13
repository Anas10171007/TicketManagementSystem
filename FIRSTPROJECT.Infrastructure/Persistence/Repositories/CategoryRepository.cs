namespace FIRSTPROJECT.Infrastructure.Persistence.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository//I am implementing the ICategoryRepository interface and inheriting from the generic Repository<Category> class. This means that CategoryRepository will have all the methods defined in Repository for Category entity, and it can also have additional methods specific to Category repository if needed.
{
    public CategoryRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}