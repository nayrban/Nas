using NAS.DTO;
using NAS.DTO.Request;
using NAS.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace NAS.Controllers
{
    [RoutePrefix("api/v1/users")]    
    public class UserController : ApiController
    {
        private IMemberService MemberService { get; }        

        public UserController(IMemberService MemberService)
        {
            this.MemberService = MemberService;
        }

        [Route("")]
        public IHttpActionResult Get([FromUri]int offset = 0, [FromUri]int limit = 50) {
            List<UserInfo> userInfoList = this.MemberService.GetAll(offset, limit);

            int total = this.MemberService.Count();

            return Ok(new { Data = userInfoList, Paging = new {
                    Total = total,
                    Offset = offset,
                    Limit = limit,
                    returned = userInfoList.Count
            } });
        }


        [HttpGet]        
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            UserInfo userInfo = MemberService.GetMemberByID(id);
            if (userInfo == null)
                return NotFound();

            return Ok(userInfo);
        }

        [HttpPost]
        [Route("registration")]
        public IHttpActionResult Create([FromBody] PostUserRequest userRequest)
        {
            if(!ModelState.IsValid)
                return  BadRequest(ModelState);

            UserInfo userInfo =   MemberService.Create(userRequest);
            return CreatedAtRoute("DefaultUserApi", new { id = userInfo.id}, userInfo );
        }

        /*[Htt]
        [Route("registration")]
        public IHttpActionResult Create([FromBody] PostUserRequest userRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            UserInfo userInfo = MemberService.Create(userRequest);
            return CreatedAtRoute("DefaultUserApi", new { id = userInfo.id }, userInfo);
        }*/
    }
}
