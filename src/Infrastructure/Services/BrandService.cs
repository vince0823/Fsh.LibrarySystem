using FSH.Learn.Application.Catalog.Brands;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.System.Services;
using FSH.Learn.Domain.Catalog;
using FSH.Learn.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Infrastructure.Services;
public class BrandService : IBrandService
{

    private readonly IReadRepository<Brand> _repository;
    public BrandService(IReadRepository<Brand> repository)
    {
        _repository = repository;
    }

    public async Task<List<Brand>> GetBrands()
    {
        return await _repository.ListAsync();

    }
}
