using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Services;
public interface IBookShelfLayerService : ITransientService
{
    Task<string> GetBookAdress(Guid layId, CancellationToken cancellationToken);

}
