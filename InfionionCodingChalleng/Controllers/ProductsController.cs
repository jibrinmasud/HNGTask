using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfionionCodingChalleng.Dto.Products;
using Microsoft.AspNetCore.Mvc;

namespace InfionionCodingChalleng.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddProducts")]
        public IActionResult AddProducts(CreateProductDto createProductDto)
        {
            var prodductModel = new Product() 
            {
                ProductName = createProductDto.ProductName,
                Description = createProductDto.Description,
                Quantity = createProductDto.Quantity,
                Category = createProductDto.Category,
                Price = createProductDto.Price,
                CreatedAt = createProductDto.CreatedAt
            };
            _context.Products.Add(prodductModel);
            _context.SaveChanges();

            return Ok(prodductModel);
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProduct()
        {
            var AllProducts =_context.Products.ToList();
            return Ok(AllProducts);
        }

        [HttpGet]
        [Route("GetProductById")]
        public IActionResult GetAllProductsById(Guid id)
        {
            var products = _context.Products.Find(id);
            if(products is null)
            {
                return NotFound();
            }
            return Ok(products);

        }

        [HttpPut]
        [Route("UpdateProducts")]
        public IActionResult UpdateProducts(Guid id, UpdateProductDto updateProductDto )
        {
            var products = _context.Products.Find(id);

            if(products is null )
            {
                return NotFound();
            }
            products.ProductName = updateProductDto.ProductName;
            products.Description = updateProductDto.Description;
            products.Category = updateProductDto.Category;
            products.Quantity = updateProductDto.Quantity;
            products.Price = updateProductDto.Price;
            products.CreatedAt = updateProductDto.CreatedAt;

            _context.SaveChanges();

            return Ok(products);

        }

        [HttpDelete]
        [Route("DeleteProduct")]

        public IActionResult DeleteProducts(Guid id)
        {
            var products = _context.Products.Find(id);
            if(products is null)
            {
                return NotFound();
            }
            _context.Products.Remove(products);
            _context.SaveChanges();

            return Ok(products);
        }

    }
}