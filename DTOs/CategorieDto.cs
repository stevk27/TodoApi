// DTOs/ProductDto.cs
namespace TodoApi.DTOs
{
    public class CategoriesCreateDto
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CategoriesResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}