using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			

			var builder = WebApplication.CreateBuilder(args);

			#region Configure Service
			// Add services to the container.

			builder.Services.AddControllers();
			//Register Required Web APIs Services to the DI Container
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			//Must add project reference of [Talabat.Repository] to use AddDbContext()
			builder.Services.AddDbContext<StoreContext>(op =>
			{
				//Get connection string from [appsettings.json]
				op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			//builder.Services.AddScoped<IGenericRepository<Product>, IGenericRepository<Product>>();
			//builder.Services.AddScoped<IGenericRepository<ProductBrand>, IGenericRepository<ProductBrand>>();
			//builder.Services.AddScoped<IGenericRepository<ProductCategory>, IGenericRepository<ProductCategory>>();

			//Allow DI Generic for all modules 

			builder.Services.AddScoped(typeof(IGenericRepository<>) , typeof(GenericRepository<>));
			//lama 7d yetlop obj mn class be3mel implement l IGenericRepository mn type mo3en 
			//e3mel create l obj mn IGenericRepository mn nfs al type 

			//builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			builder.Services.AddAutoMapper(typeof(MappingProfiles));
			#endregion

			builder.Services.Configure<ApiBehaviorOptions>(O =>
			{
				O.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
														 .SelectMany(P => P.Value.Errors)
														 .Select(E => E.ErrorMessage)
														 .ToList();

					var response = new ApiValidationErrorsResponse()
					{ Errors = errors };

					return new BadRequestObjectResult(response);
				};

				

			});

			var app = builder.Build();


			#region ask CLR to create objext from StoreContext [Explicity]

			/*[using] to Dispose the Scope {Try, Finally}*/
			using var scope = app.Services.CreateScope();

			var services = scope.ServiceProvider;

			var _dbContext = services.GetRequiredService<StoreContext>();

			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			//Ask CLR to create Object from ILoggerFactory

			try
			{
				//apply migration
				await _dbContext.Database.MigrateAsync();

				//seeding the data 
				await StoreContextSeed.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{
				#region Log the exception
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error has been occured during apply the migration ");  
				#endregion

			}
			#endregion


			#region Configure Kestrel Midlewares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			//app.UseAuthorization();

			app.UseStaticFiles();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
