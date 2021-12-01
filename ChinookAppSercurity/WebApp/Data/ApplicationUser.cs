
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


#region Additional Namespaces
using Microsoft.AspNetCore.Identity;
using AppSecurity.Models;
#endregion

namespace WebApp.Data
{
    public class ApplicationUser : IdentityUser, IIdentifyEmployee
    {
        public int? EmployeeId { get; set; }
    }

}
