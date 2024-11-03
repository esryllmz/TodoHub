using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoHub.Models.Dtos.Category.Requests;

public sealed record CreateCategoryRequest(string Name);
