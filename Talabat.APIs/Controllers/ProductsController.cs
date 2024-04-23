﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productsRepo;
		private readonly IGenericRepository<ProductBrand> _brandsRepo;
		private readonly IGenericRepository<ProductCategory> _categoriesRepo;
		private readonly IMapper _mapper;

		public ProductsController(
			IGenericRepository<Product> productsRepo ,
			IGenericRepository<ProductBrand> brandsRepo ,
			IGenericRepository<ProductCategory> categoriesRepo ,
			IMapper mapper)
        {
			_productsRepo = productsRepo;
			_brandsRepo = brandsRepo;
			_categoriesRepo = categoriesRepo;
			_mapper = mapper;
		}

		/*Get All Products End Point*/
		//  /api/Products 
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string? sort , int? brandId , int? categoryId)
		{
			var spec = new ProductWithBrandsAndCategoriesSpecifications(sort , brandId , categoryId);
			var products = await _productsRepo.GetAllWithSpecAsync(spec);


			return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
		}

		/*Get Product By ID End Point*/
		//  /api/Products/1


		//Improving Swagger Documentation

		[ProducesResponseType(typeof(ProductToReturnDto) , StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

		[HttpGet("{id}")] 

		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandsAndCategoriesSpecifications(id);
			var product = await _productsRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new ApiResponse(404)); // 400

			return Ok(_mapper.Map<Product, ProductToReturnDto>(product)); // 200

		}

		[HttpGet("brands")]

		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _brandsRepo.GetAllAsync();

			return Ok(brands);

		}

		[HttpGet("{id}")]

		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var categories = await _categoriesRepo.GetAllAsync();

			return Ok(categories);

		}


	}
}
