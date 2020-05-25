using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebDatingApp.API.Installers;
using WebDatingApp.API;

namespace WebDatingApp.API.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            // Injeçao de dependencia: selecionando os installers, criando instancias e fazendo Cast para obter IInstaller
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x => 
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            
            installers.ForEach(inst => inst.InstallServices(services, configuration));
        }
    }
}