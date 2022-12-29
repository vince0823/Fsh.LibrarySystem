using FSH.Learn.Domain.Catalog;
using FSH.Learn.Domain.Common.Contracts;
using FSH.Learn.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System;
public class Department : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentId { get; set; }
    public string? DutyUserId { get; set; }

    public Department(string name, string? description, Guid? parentId, string? dutyUserId)
    {
        Name = name;
        Description = description;
        ParentId = parentId;
        DutyUserId = dutyUserId;

    }

    public Department Update(string name, string? description, Guid? parentId, string? dutyUserId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (parentId.HasValue && parentId.Value != Guid.Empty && !ParentId.Equals(parentId.Value)) ParentId = parentId.Value;
        if (dutyUserId is not null && DutyUserId?.Equals(dutyUserId) is not true) DutyUserId = dutyUserId;
        return this;
    }
}
