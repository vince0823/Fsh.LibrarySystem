using FSH.Learn.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System;

/// <summary>
///  书屋.
/// </summary>
public class BookRoom : AuditableEntity, IAggregateRoot
{

    /// <summary>
    /// 书屋名称.
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    /// 所属地址.
    /// </summary>
    public string Address { get; private set; } = default!;

    /// <summary>
    /// 负责人.
    /// </summary>
    public Guid? DutyUserId { get; set; }

    public BookRoom()
    {
    }

    public BookRoom(string name, string address, Guid? dutyUserId)
    {
        Name = name;
        Address = address;
        DutyUserId = dutyUserId;
    }

    public BookRoom Update(string name, string address, Guid? dutyUserId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (address is not null && Address?.Equals(address) is not true) Address = address;
        if (dutyUserId is not null && DutyUserId?.Equals(dutyUserId) is not true) DutyUserId = dutyUserId;
        return this;
    }
}
