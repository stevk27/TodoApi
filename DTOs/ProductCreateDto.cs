// DTOs/ProductCreateDto.cs avec validation
using System.ComponentModel.DataAnnotations;

public class ProductCreateDto
{
    [Required(ErrorMessage = "Le nom est obligatoire")]
    [StringLength(200, ErrorMessage = "Le nom ne peut pas dépasser 200 caractères")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "La description ne peut pas dépasser 1000 caractères")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le prix est obligatoire")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à 0")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "La catégorie est obligatoire")]
    public int CategoryId { get; set; }
}