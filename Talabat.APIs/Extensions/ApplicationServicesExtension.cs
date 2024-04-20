using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			//lama 7d yetlop obj mn class be3mel implement l IGenericRepository mn type mo3en 
			//e3mel create l obj mn IGenericRepository mn nfs al type 

			//builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));

			services.AddAutoMapper(typeof(MappingProfiles));

			services.Configure<ApiBehaviorOptions>(O =>
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

			return services;

		}
	}
}
