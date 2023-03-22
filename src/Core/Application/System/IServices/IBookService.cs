﻿using FSH.Learn.Domain.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.IServices;
public interface IBookService : ITransientService
{
    Task<List<Book>> GetBooks();
}
