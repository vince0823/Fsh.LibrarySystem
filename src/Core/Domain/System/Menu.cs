using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System;
public class Menu : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; } = default!;
    public string Url { get; set; }
    public Guid? ParentId { get; set; }
    public string Icon { get; set; }
    public int Order { get; set; }

    public Menu(string name, string url, Guid? parentId, string icon, int order)
    {
        Name = name;
        Url = url;
        ParentId = parentId;
        Icon = icon;
        Order = order;
    }

    public Menu Update(string name, string url, Guid? parentId, string icon, int order)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (url is not null && Url?.Equals(url) is not true) Url = url;
        if (parentId.HasValue && parentId.Value != Guid.Empty && !ParentId.Equals(parentId.Value)) ParentId = parentId.Value;
        if (icon is not null && Icon?.Equals(icon) is not true) Icon = icon;
        if (Order.Equals(order) is not true) Order = order;
        return this;
    }
}

