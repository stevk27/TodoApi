// Services/ProductService.cs
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Services.categorie 
{
    public class CategorieService : ICategorieService
    {
        private readonly ApplicationDbContext _context;

        public CategorieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriesResponseDto>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(p => new CategoriesResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToListAsync();
        }

        public async Task<CategoriesResponseDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == id);

            if (category == null) return null;

            return new CategoriesResponseDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoriesResponseDto> CreateCategoryAsync(CategoriesCreateDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();


            return new CategoriesResponseDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<CategoriesResponseDto?> UpdateCategoryAsync(int id, CategoriesCreateDto categoryDto)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == id);

            if (category == null) return null;

            category.Name = categoryDto.Name;

            await _context.SaveChangesAsync();

            return new CategoriesResponseDto
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<CategoriesResponseDto?> GetCategorieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriesResponseDto> CreateCategorieAsync(CategoriesCreateDto categorietDto)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriesResponseDto?> UpdateCategorieAsync(int id, CategoriesCreateDto categorietDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategorieAsync(int id)
        {
            throw new NotImplementedException();
        }
    } 
        
}