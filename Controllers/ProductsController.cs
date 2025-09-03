// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Services;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des produits");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
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
    public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] ProductCreateDto productDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du produit");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponseDto>> UpdateProduct(int id, [FromBody] ProductCreateDto productDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.UpdateProductAsync(id, productDto);
            if (product == null)
            {
                return NotFound($"Produit avec l'ID {id} introuvable");
            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour du produit {Id}", id);
            return StatusCode(500, "Erreur interne du serveur");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        try
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound($"Produit avec l'ID {id} introuvable");
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