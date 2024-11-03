using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Entities;

namespace TodoHub.Models.Entities;
public class Category:Entity<int>
{
    
    public string Name { get; set; }

    
    public List<Todo> ToDos { get; set; }
}



