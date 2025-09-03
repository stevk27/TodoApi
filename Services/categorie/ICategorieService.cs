using TodoApi.DTOs;

namespace TodoApi.Services.categorie
{
    public interface ICategorieService
    {
        Task<IEnumerable<CategoriesResponseDto>> GetAllCategoriesAsync();
        Task<CategoriesResponseDto?> GetCategorieByIdAsync(int id);
        Task<CategoriesResponseDto> CreateCategorieAsync(CategoriesCreateDto categorietDto);
        Task<CategoriesResponseDto?> UpdateCategorieAsync(int id, CategoriesCreateDto categorietDto);
        Task<bool> DeleteCategorieAsync(int id);
    }
}