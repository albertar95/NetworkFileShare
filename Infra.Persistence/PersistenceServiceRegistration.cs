using Application.Persistence.Contract;
using Application.Persistence.Contract.Common;
using Infra.Persistence.Context;
using Infra.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services,
            string ConnectionString = "Server=.;Database=NetworkFileDb;User Id=sa;Password=safa@123;TrustServerCertificate=true;") 
        {
            services.AddDbContext<NetworkFileDbContext>(o => o.UseSqlServer(ConnectionString
            , sql => sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<IBaseRepository, BaseRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
