using AutoMapper;
using NasService;
using NasServiceAPI.Annotations;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading.Tasks;
using NasModel.AuthModel;
using NasDTOUtils.Dto;

namespace NasServiceAPI.Controllers
{
    [RoutePrefix("api/v1/members")]    
    public class UserController : ApiController
    {
        private ApplicationUserService MemberService { get; }


        public UserController(ApplicationUserService MemberService)
        {
            this.MemberService = MemberService;
        }
        
        [NasAuthorize]
        [Route("")]
        public IHttpActionResult Get([FromUri]int offset = 0, [FromUri]int limit = 50) {            

            List<UserInfo> userInfoList = MemberService.GetAll(offset, limit).ProjectTo<UserInfo>(Mapper.Instance.ConfigurationProvider).ToList();

            int total = MemberService.Count();

            return Ok(new { Data = userInfoList, Paging = new {
                    Total = total,
                    Offset = offset,
                    Limit = limit,
                    returned = userInfoList.Count
            } });
        }


        [NasAuthorize]
        [HttpGet]        
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(string id)
        {
            ApplicationUser userInfo = await MemberService.FindUser(id);
            if (userInfo == null)
                return NotFound();

            return Ok(Mapper.Map<UserInfo>(userInfo));
        }       

        /*[Htt]
        [Route("registration")]
        public IHttpActionResult Create([FromBody] PostUserRequest userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            MemberInfo userInfo = MemberService.Create(userRequest);
            return CreatedAtRoute("DefaultUserApi", new { id = userInfo.id }, userInfo);
        }*/
    }
}
