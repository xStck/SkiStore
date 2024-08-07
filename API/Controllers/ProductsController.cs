using API.DTOs;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController(IGenericRepository<Product> productRepository,
    IGenericRepository<ProductBrand> productBrandRepository,
    IGenericRepository<ProductType> productTypeRepository,
    IMapper mapper) : BaseApiController
{
    [Cached(600)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts([FromQuery] ProductSpecParams productParams)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
        var countSpec = new ProductWithFiltersForCountSpecification(productParams);
        var totalItems = await productRepository.CountAllWithSpecAsync(countSpec);
        var products = await productRepository.ListAllWithSpecAsync(spec);
        var data = mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);
        return Ok(new Pagination<ProductDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    [Cached(600)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);
        var product = await productRepository.GetEntityWithSpecAsync(spec);
        if (product == null)
            return NotFound(new ApiResponse(404));
        return mapper.Map<Product, ProductDTO>(product);
    }

    [Cached(600)]
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await productBrandRepository.ListAllAsync());
    }

    [Cached(600)]
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
    {
        return Ok(await productTypeRepository.ListAllAsync());
    }
}