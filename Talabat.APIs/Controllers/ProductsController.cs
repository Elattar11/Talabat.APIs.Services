using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;

		public ProductsController(IGenericRepository<Product> productsRepo)
        {
			_productsRepo = productsRepo;
		}

		/*Get All Products End Point*/
		//  /api/Products 
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var spec = new ProductWithBrandsAndCategoriesSpecifications();
			var products = await _productsRepo.GetAllWithSpecAsync(spec);


			return Ok(products);
		}

		/*Get Product By ID End Point*/
		//  /api/Products/1

		[HttpGet("{id}")] 

		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandsAndCategoriesSpecifications(id);
			var product = await _productsRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new { Message = "Not Found" , StatusCode = 404}); // 400

			return Ok(product); // 200

		}


	}
}
