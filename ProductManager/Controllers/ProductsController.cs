using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.Controllers
{
    using Models;

    [Route(Constants.Routes.Template)]
    public class ProductsController : Controller
    {
        private AdventureWorksContext _context;

        public ProductsController(AdventureWorksContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            /*var products = _context
                .Product
                .ToArray();

            return Ok(products);*/
            
            return Ok("OK");
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var product = _context
                .Product
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Product.Add(product);
            _context.SaveChanges();

            return CreatedAtAction("Get", new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _context.Product.SingleOrDefault(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }
    }
}
