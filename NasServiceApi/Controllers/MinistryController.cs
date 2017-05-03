using AutoMapper;
using NasDTOUtils.Dto;
using NasDTOUtils.Dto.Request;
using NasDTOUtils.Dto.Response;
using NasModel.Model;
using NasService;
using NasServiceAPI.Annotations;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace NasServiceAPI.Controllers
{
    [RoutePrefix("api/v1/ministry")]
    public class MinistryController : ApiController
    {
        private readonly IMinistryService minisrtyService;

        public MinistryController(IMinistryService minisrtyService)
        {
            this.minisrtyService = minisrtyService;
        }

        [NasAuthorize]
        [HttpPost]
        [Route("create/ministrycode")]
        public async Task<IHttpActionResult> CreateMinistryCode([FromBody] MinistryCodeRequest userRequest)
        {
            if (userRequest == null || !ModelState.IsValid)
                return BadRequest(ModelState);

             MinistryCode ministryCode =  minisrtyService.CreateMinistryCode(userRequest.MinistryId);

            bool created = await minisrtyService.SaveChangesAsync();

            if (!created)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new NasError() { Message = "Can not create the ministry code", Code = 400 }, new JsonMediaTypeFormatter());

            }

            return Ok(Mapper.Map<MinistryCodeResponse>(ministryCode));
        }
    }
}
