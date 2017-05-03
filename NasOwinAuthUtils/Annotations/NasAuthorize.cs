using System.Net;
using System.Web.Mvc;


namespace NasOwinAuthUtils.Annotations
{
    public class NasAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var response = actionContext.Request.CreateResponse<NasError>
                                    (new NasError() { Message = "My failing reason", Code = 401 });
            response.StatusCode = HttpStatusCode.Unauthorized;

            actionContext.Response = response;
        }
    }
}
