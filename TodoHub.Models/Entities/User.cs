using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoHub.Models.Entities;

public class User : IdentityUser
{

    string FirstName { get; set; }=string.Empty;
    public string LastName { get; set; } = string.Empty;

    public List<Todo> Todos { get; set; } = new List<Todo>();
}



