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
                .Select(c => new CategoriesResponseDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoriesResponseDto?> GetCategorieByIdAsync(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);

            if (categorie == null) return null;

            return new CategoriesResponseDto
            {
                Id = categorie.Id,
                Name = categorie.Name
            };
        }

        public async Task<CategoriesResponseDto> CreateCategorieAsync(CategoriesCreateDto categorieDto)
        {
            var categorie = new Category
            {
                Name = categorieDto.Name
            };

            _context.Categories.Add(categorie);
            await _context.SaveChangesAsync();

            return new CategoriesResponseDto
            {
                Id = categorie.Id,
                Name = categorie.Name
            };
        }

        public async Task<CategoriesResponseDto?> UpdateCategorieAsync(int id, CategoriesCreateDto categorieDto)
        {
            var categorie = await _context.Categories.FindAsync(id);

            if (categorie == null) return null;

            categorie.Name = categorieDto.Name;

            await _context.SaveChangesAsync();

            return new CategoriesResponseDto
            {
                Id = categorie.Id,
                Name = categorie.Name
            };
        }

        public async Task<bool> DeleteCategorieAsync(int id)
        {
            var categorie = await _context.Categories.FindAsync(id);

            if (categorie == null) return false;

            _context.Categories.Remove(categorie);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}