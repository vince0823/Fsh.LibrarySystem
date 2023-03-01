using Ardalis.Specification;
using FSH.Learn.Application.Common.Exceptions;
using FSH.Learn.Application.Common.Interfaces;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.System.Commands.BookRecords;
using FSH.Learn.Application.System.IServices;
using FSH.Learn.Domain.System;
using FSH.Learn.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FSH.Learn.Infrastructure.Services;
public class BookRecordAddService : IBookRecordAddService
{

    private readonly IRepository<Book> _bookrepository;
    private readonly ICurrentUser _currentUser;
    private readonly ApplicationDbContext _db;
    public BookRecordAddService(IRepository<Book> bookrepository, ICurrentUser currentUser, ApplicationDbContext db)
    {
        _bookrepository = bookrepository;
        _currentUser = currentUser;
        _db = db;
    }

    public async Task<Guid> BookRecordAdd(BookRecordAddCommand command, CancellationToken cancellationToken)
    {
        var book = await _bookrepository.GetByIdAsync(command.BookId, cancellationToken);
        _ = book ?? throw new NotFoundException("书籍不存在");

        // Guard.Against.Null(book);
        var bookRecord = new BookRecord(command.BookId, command.BookRecordType, _currentUser.GetUserId().ToString());
        await _db.BookRecords.AddAsync(bookRecord, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return bookRecord.Id;
    }
}
