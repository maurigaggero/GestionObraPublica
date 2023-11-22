using Microsoft.AspNetCore.Mvc;

namespace IISPI.Server.Controllers
{
    public class IISPIBaseController: ControllerBase
    {
        protected string ObtenerUserId()
        {
            return (HttpContext.User.Claims.Where(x => x.Type == "Id").FirstOrDefault()).Value;
        }
    }
}
