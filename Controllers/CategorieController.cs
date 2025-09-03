// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services.categorie;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategorieService _categorieService;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategorieService categorieService, ILogger<CategoriesController> logger)
    {
        _categorieService = categorieService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        try
        {
            var products = await _categorieService.GetAllCategoriesAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des produits");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetCategorie(int id)
    {
        try
        {
            var product = await _categorieService.GetCategorieByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Produit avec l'ID {id} introuvable");
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du produit {Id}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> CreateCategorie([FromBody] CategoriesCreateDto categorieDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categorie = await _categorieService.CreateCategorieAsync(categorieDto);
            return CreatedAtAction(nameof(GetCategorie), new { id = categorie.Id }, categorie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du produit");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponseDto>> UpdateProduct(int id, [FromBody] CategoriesCreateDto categorietDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categorie = await _categorieService.UpdateCategorieAsync(id, categorietDto);
            if (categorie == null)
            {
                return NotFound($"Produit avec l'ID {id} introuvable");
            }

            return Ok(categorie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour de la categorie {Id}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategorie(int id)
    {
        try
        {
            var success = await _categorieService.DeleteCategorieAsync(id);
            if (!success)
            {
                return NotFound($"Categorie avec l'ID {id} introuvable");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression du produit {Id}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }
}