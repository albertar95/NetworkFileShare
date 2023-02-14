using Application.Helpers;
using Application.Helpers.Contract;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static string FileSystemAddress = "";
        public static IServiceCollection ConfigureApplicationService(this IServiceCollection services,string FileSystemPath = "E:\\Network") 
        {
            FileSystemAddress= FileSystemPath;
            //services.AddScoped<IApiHelper, ApiHelper>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
