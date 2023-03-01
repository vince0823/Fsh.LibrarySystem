using FSH.Learn.Application.System.Commands.BookRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.IServices;
public interface IBookRecordAddService : ITransientService
{
    Task<Guid> BookRecordAdd(BookRecordAddCommand command,CancellationToken cancellationToken);
}
