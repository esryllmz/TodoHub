using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoHub.Models.Entities;

public class User : IdentityUser
{

    // Kullanıcının kendi görev listesi
    public List<Todo> Todos { get; set; } = new List<Todo>();
}



