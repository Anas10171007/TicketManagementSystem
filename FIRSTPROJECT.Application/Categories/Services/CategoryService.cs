using FIRSTPROJECT.Application.Categories.DTOs;
using FIRSTPROJECT.Application.Categories.Interfaces;
using FIRSTPROJECT.Application.Common;
using FIRSTPROJECT.Domain.Constants;
using System.Net;

namespace FIRSTPROJECT.Application.Categories.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        }).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        await _categoryRepository.AddAsync(category);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }

    public async Task<ServiceResult> UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult.Fail(HttpStatusCode.NotFound, ResponseMessages.CategoryNotFound);
        }

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.IsActive = dto.IsActive;

        await _categoryRepository.UpdateAsync(category);

        return ServiceResult.Ok();
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult.Fail(HttpStatusCode.NotFound, ResponseMessages.CategoryNotFound);
        }

        await _categoryRepository.DeleteAsync(category);

        return ServiceResult.Ok(HttpStatusCode.NoContent);
    }
}