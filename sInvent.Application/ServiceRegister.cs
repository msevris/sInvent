using Microsoft.Extensions.DependencyInjection;
using sInvent.Application.UsersAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sInvent.Application
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection @this)
        {
            @this.AddTransient<CreateUser>();

            return @this;
        }
    }
}
