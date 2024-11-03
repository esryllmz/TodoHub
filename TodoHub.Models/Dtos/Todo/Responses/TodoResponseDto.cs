using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Models.Entities;

namespace TodoHub.Models.Dtos.Todo.Responses
{
    public sealed record TodoResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Priority Priority { get; set; }
        public bool Completed { get; set; }
        public string CategoryName { get; set; } 
        public string UserName { get; set; } 
    }
   
}
