using NasDTOUtils.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;


namespace NasServiceAPI.Annotations
{
    public class NasAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }else { 
                var response = actionContext.Request.CreateResponse
                                    (new NasError() { Message = "Invalid Token", Code = 401 });
                response.StatusCode = HttpStatusCode.Unauthorized;

                actionContext.Response = response;
            }
        }
    }
}
