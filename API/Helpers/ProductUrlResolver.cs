using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class ProductUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductDTO, string>
{
    public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl)) return configuration["ApiUrl"] + source.PictureUrl;

        return null;
    }
}