﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Menus;
public class MenuDetailsDto : IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? DisPlayName { get; set; }
    public string? Url { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
    public string? ParentName { get; set; }
    public int Order { get; set; }
}
