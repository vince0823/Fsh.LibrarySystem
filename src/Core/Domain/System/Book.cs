using Ardalis.GuardClauses;
using FSH.Learn.Domain.System.EnumExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System;
public class Book : AuditableEntity, IAggregateRoot
{
    private readonly List<BookRecord> _items = new();
    public string Name { get; set; } = default!;
    public string Author { get; set; } = default!;
    public BookType BookType { get; set; } = default!;
    public bool IsBorrowed { get; set; } = false;
    public Guid BookShelfLayerId { get; set; }
    public string? Description { get; set; }
    public virtual BookShelfLayer BookShelfLayer { get; set; } = default!;
    public IReadOnlyCollection<BookRecord> Items => _items.AsReadOnly();

    public Book()
    {

    }

    public Book(string name, string author, BookType bookType, Guid bookShelfLayerId, string? description)
    {
        Name = name;
        Author = author;
        BookType = bookType;
        BookShelfLayerId = bookShelfLayerId;
        Description = description;
    }

    public Book Update(string? name, string? author, BookType? bookType, Guid? bookShelfLayerId, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (author is not null && Author?.Equals(author) is not true) Author = author;
        if (bookType.HasValue && Enum.IsDefined(typeof(BookType), bookType) && !BookType.Equals(bookType)) BookType = bookType.Value;
        if (bookShelfLayerId.HasValue && bookShelfLayerId.Value != Guid.Empty && !BookShelfLayerId.Equals(bookShelfLayerId.Value)) BookShelfLayerId = bookShelfLayerId.Value;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }

    public void Borrowed(bool isBorrowed)
    {
        IsBorrowed = isBorrowed;
    }

    public void Back(bool isBorrowed)
    {
        IsBorrowed = isBorrowed;
    }
}
