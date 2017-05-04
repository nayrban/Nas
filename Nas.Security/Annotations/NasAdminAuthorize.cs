using System.Web.Http;
using System.Web.Http.Controllers;


namespace Nas.Security.Annotations
{
    public class NasAdminAuthorize : AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            this.R


            return true;
        }        
    }
}
