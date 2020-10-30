using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Avatar { get; set; }
    }
}
