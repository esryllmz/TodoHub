using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Entities;

namespace TodoHub.Models.Dtos.Todo.Requests
{
    public sealed record UpdateTodoRequestDto(
        Guid Id,
        string Title,
        string Description,
        DateTime StartDate,
        DateTime EndDate,
        Priority Priority,
        int CategoryId,
        bool Completed);
    
}
