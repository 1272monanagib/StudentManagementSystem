using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DAL.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
