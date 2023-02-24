using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Catalog.Brands;
public interface IBrandService : ITransientService
{
    Task<List<Brand>> GetBrands();
}
