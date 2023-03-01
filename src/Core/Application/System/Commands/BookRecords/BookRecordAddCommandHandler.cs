using Ardalis.GuardClauses;
using FSH.Learn.Application.Common.Persistence;
using FSH.Learn.Application.System.Commands.Books;
using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotFoundException = FSH.Learn.Application.Common.Exceptions.NotFoundException;

namespace FSH.Learn.Application.System.Commands.BookRecords;
public class BookRecordAddCommandHandler : IRequestHandler<BookRecordAddCommand, Guid>
{
    private readonly IRepository<Book> _bookrepository;
    private readonly ICurrentUser _currentUser;
    private readonly IRepositoryBase<BookRecord> _bookRecordrepository;// 此处无法实现对 _bookRecordrepository 的解析  IRepository   只对聚合根生效 ，故使用服务来添加记录 
    public BookRecordAddCommandHandler(IRepository<Book> bookrepository, ICurrentUser currentUser, IRepositoryBase<BookRecord> bookRecordrepository)
    {
        _bookrepository = bookrepository;
        _currentUser = currentUser;
        _bookRecordrepository = bookRecordrepository;
    }

    public async Task<Guid> Handle(BookRecordAddCommand command, CancellationToken cancellationToken)
    {
        var book = await _bookrepository.GetByIdAsync(command.BookId, cancellationToken);
        _ = book ?? throw new NotFoundException("书籍不存在");

        // Guard.Against.Null(book);
        var bookRecord = new BookRecord(command.BookId, command.BookRecordType, _currentUser.GetUserId().ToString());
        await _bookRecordrepository.AddAsync(bookRecord, cancellationToken);
        return bookRecord.Id;
    }
}
