using FIRSTPROJECT.Application.Categories.DTOs;
using FIRSTPROJECT.Application.Common;

namespace FIRSTPROJECT.Application.Categories.Interfaces;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(Guid id);
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
    Task<ServiceResult> UpdateAsync(Guid id, UpdateCategoryDto dto);
    Task<ServiceResult> DeleteAsync(Guid id);
}