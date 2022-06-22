using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Models
{
    public class User:IdentityUser
    {
        [Required,MaxLength(100)]
        public string FullName { get; set; }
        public bool IsBlocked { get; set; }
    }
}
