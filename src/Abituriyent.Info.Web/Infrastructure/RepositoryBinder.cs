using Abituriyent.Info.DataAccess;
using Abituriyent.Info.Repository.UOW;
using Abituriyent.Info.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Abituriyent.Info.Web.Infrastructure
{
    public static class RepositoryBinder
    {
        public static void BindServiceImplementations(this IServiceCollection services)
        {
            //#region Data Repositories Bindings

            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();

            //#endregion

            #region Services Bindings

            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            #endregion
        }
    }
}
