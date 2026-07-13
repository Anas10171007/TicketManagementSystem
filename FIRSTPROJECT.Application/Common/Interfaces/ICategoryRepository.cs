namespace FIRSTPROJECT.Application.Common.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
}
//Inheriting from IRepository<Category> means that ICategoryRepository will have all the methods defined in IRepository for Category entity.
//Later on, we can add more specific methods for Category repository if needed, for example, methods to get categories by name or to get active categories only.