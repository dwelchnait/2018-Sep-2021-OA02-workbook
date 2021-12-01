
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AppSecurity.DAL;
using AppSecurity.BLL;
#endregion

namespace AppSecurity
{
    public static class StartupExtensions
    {
        public static void AddAppSecurityDependencies(this IServiceCollection services,
                Action<DbContextOptionsBuilder> options)
        {
            //add the context class of your application library to the service collection
            //pass in the connection string options.
            services.AddDbContext<AppSecurityDbContext>(options);

            //add any business logic layer class to the service collection so our
            //  web app has access to the methods within the BLL class.
            services.AddTransient<SecurityService>((serviceProvider) =>
            {
                //get the dbcontext class
                var context = serviceProvider.GetRequiredService<AppSecurityDbContext>();
                return new SecurityService(context);
            });
        }
    }
}
