using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WebDatingApp.API.Data;

namespace WebDatingApp.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        // Service Filter
        // Nesta classe, podemos executar alguma ação enquanto uma task esta sendo executada, ou
        // depois ou antes de algum metodo ter sido chamado
        // ex: sempre q o GetUsers for acionado, atualizar a data de "last activity"
        // ActionExecutingContext = enquanto algo esta executando
        // ActionExecutionDelegate = após ou antes de algo ser executado
        // Dps colocamos no controller que queremos associar, exemplo: UsersControles
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var repo  = resultContext.HttpContext.RequestServices.GetService<IDatingRepository>();

            var user = await repo.GetUser(userId);

            user.LastActive = DateTime.Now;

            await repo.SaveAll();
        }
    }
}