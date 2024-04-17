using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class ProductsPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
	{
		private readonly IConfiguration _conf;

		public ProductsPictureUrlResolver(IConfiguration conf)
        {
			_conf = conf;
		}
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if(!string.IsNullOrEmpty(source.PictureUrl))
				return $"{_conf["ApiBaseUrl"]}/{source.PictureUrl}";

			return string.Empty ;
		}
	}
}
