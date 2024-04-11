using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public async static Task SeedAsync(StoreContext _dbContext)
		{
			//Seeding data from brands.json
			if (_dbContext.ProductBrands.Count() == 0 /*check if table not contain any element*/)
			{
				var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json"); //Read data from json file

				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);


				if (brands?.Count() > 0 /*products is not null && products.Count() > 0*/)
				{
					//brands = brands.Select(b =>  new ProductBrand()
					//{
					//	Name = b.Name

					//}).ToList();


					foreach (var item in brands)
					{
						_dbContext.Set<ProductBrand>().Add(item);
					}

					await _dbContext.SaveChangesAsync();
				} 
			}

			//Seeding data from category.json
			if (_dbContext.ProductCategories.Count() == 0 /*check if table not contain any element*/)
			{
				var categoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");

				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);


				if (categories?.Count() > 0 /*products is not null && products.Count() > 0*/)
				{
					//brands = brands.Select(b =>  new ProductBrand()
					//{
					//	Name = b.Name

					//}).ToList();


					foreach (var item in categories)
					{
						_dbContext.Set<ProductCategory>().Add(item);
					}

					await _dbContext.SaveChangesAsync();
				}
			}

			//Seeding data from products.json
			if (_dbContext.Products.Count() == 0 /*check if table not contain any element*/)
			{
				var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");

				var products = JsonSerializer.Deserialize<List<Product>>(productsData);


				if (products?.Count() > 0 /*products is not null && products.Count() > 0*/)
				{
					//brands = brands.Select(b =>  new ProductBrand()
					//{
					//	Name = b.Name

					//}).ToList();


					foreach (var item in products)
					{
						_dbContext.Set<Product>().Add(item);
					}

					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
