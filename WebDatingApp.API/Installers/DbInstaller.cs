using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDatingApp.API.Data;

namespace WebDatingApp.API.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(x => x.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        
            // AddSingleton não é muito bom quando temos varias instancias
            //services.AddSingleton()
            // O Transient é bom para aplicaçoes leves
            //services.AddTransient()
            // O Scoped é o ideal para varios requests HTTPs simultaneos
            services.AddScoped<IAuthRepository, AuthRepository>();        
            services.AddScoped<IDatingRepository, DatingRepository>();        
        }
   }
}