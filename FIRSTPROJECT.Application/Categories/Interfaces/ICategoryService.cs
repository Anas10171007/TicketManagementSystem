using FIRSTPROJECT.Application.Categories.DTOs;

namespace FIRSTPROJECT.Application.Categories.Interfaces;

// ICategoryService tells that this is a contract (a promise).
// The real implementation will be in CategoryService.
public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();

    Task<CategoryDto?> GetByIdAsync(Guid id);

    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);

    Task UpdateAsync(Guid id, UpdateCategoryDto dto);

    Task DeleteAsync(Guid id);
}