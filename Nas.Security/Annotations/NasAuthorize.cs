using NasDTOUtils.Dto;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;


namespace Nas.Security.Annotations
{
    public class NasAuthorize : AuthorizeAttribute
    {
       
        public override void OnAuthorization(HttpActionContext actionContext)
        { 
            
            base.OnAuthorization(actionContext);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }


        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                var response = actionContext.Request.CreateResponse
                                   (new NasError() { Message = "Unauthorized - Invalid permissions", Code = 403});
                response.StatusCode = HttpStatusCode.Forbidden;
                actionContext.Response = response;
            }
            else { 
                var response = actionContext.Request.CreateResponse
                                    (new NasError() { Message = "Invalid Token", Code = 401 });
                response.StatusCode = HttpStatusCode.Unauthorized;

                actionContext.Response = response;
            }
        }
    }
}
