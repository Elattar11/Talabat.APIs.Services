using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productsRepo , IMapper mapper)
        {
			_productsRepo = productsRepo;
			_mapper = mapper;
		}

		/*Get All Products End Point*/
		//  /api/Products 
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandsAndCategoriesSpecifications();
			var products = await _productsRepo.GetAllWithSpecAsync(spec);


			return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
		}

		/*Get Product By ID End Point*/
		//  /api/Products/1

		[HttpGet("{id}")] 

		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandsAndCategoriesSpecifications(id);
			var product = await _productsRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new { Message = "Not Found" , StatusCode = 404}); // 400

			return Ok(_mapper.Map<Product, ProductToReturnDto>(product)); // 200

		}


	}
}
