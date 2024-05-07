using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorPhidgets
{
    public static class Startup
    {
        public static IServiceCollection AddBlazorPhidgets(this IServiceCollection services)
        {
            services.AddScoped<USBConnection>();
            services.AddScoped<Manager>();
            services.AddScoped<NetworkConnection>();
            services.AddScoped<Phidgets>();
            //services.AddScoped<DigitalInputPhidget>();
            //services.AddScoped<DigitalOutputPhidget>();
            return services;
        }
    }
}
