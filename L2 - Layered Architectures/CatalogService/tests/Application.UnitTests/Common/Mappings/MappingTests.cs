using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using CatalogService.Application.Categories.Queries.GetCategory;
using CatalogService.Application.Common.Interfaces;
using CatalogService.Application.Common.Models;
using CatalogService.Domain.Entities;
using CleanArchitecture.Application.Products.Queries.GetProduct;
using NUnit.Framework;

namespace CatalogService.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Category), typeof(CategoryDto))]
    [TestCase(typeof(Product), typeof(ProductDto))]
    [TestCase(typeof(Category), typeof(LookupDto))]
    [TestCase(typeof(Product), typeof(LookupDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
