using System;
using Kasir.Application.Dto;
using Kasir.Domain.Entities;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Kasir.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IMapper _mapper;

        public MappingTests()
        {
            TypeAdapterConfig typeAdapterConfig = new TypeAdapterConfig();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(typeAdapterConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            _mapper = scope.ServiceProvider.GetService<IMapper>();
        }


        //[Test]
        //[TestCase(typeof(City), typeof(CityDto))]
        //[TestCase(typeof(District), typeof(DistrictDto))]
        //public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        //{
        //    var instance = Activator.CreateInstance(source);

        //    _mapper.Map(instance, source, destination);
        //}

        [Test]
        public void ShouldMappingCorrectly()
        {
            var country = new Country { Id = 1, Name = "Bursa" };
            var countryDto = _mapper.Map<Country, CountryDto>(country);
            countryDto.Name.Should().Be("Bursa");
        }
    }
}
