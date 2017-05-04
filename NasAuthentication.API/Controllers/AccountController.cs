using Microsoft.AspNet.Identity;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using NasModel.AuthModel;
using NasService;
using NasDTOUtils.Dto;
using NasDTOUtils.Dto.Request;
using NasModel.Model;
using Nas.Security.Annotations;

namespace NasAuthentication.API.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {        
        private readonly IAuthClientService clientService;
        private readonly IApplicationUserService userService;
        private readonly IMemberService memberService;
        private readonly IDeviceService deviceService;

        public AccountController( IApplicationUserService userService, IMemberService memberService, IDeviceService deviceService, IAuthClientService clientService)
        {
            this.userService = userService;
            this.clientService = clientService;
            this.memberService = memberService;
            this.deviceService = deviceService;
        }

        [NasAuthorize(Roles="Admin")]              
        [Route("register/client")]
        public async Task<IHttpActionResult> RegisterClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (clientService.ClientExist(client.Name))
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new NasError() { Message = "Client already exist", Code = 400 }, new JsonMediaTypeFormatter());
            }

            Client result =  clientService.CreateClient(client);
            bool created = await clientService.SaveChangesAsync();

            if (!created)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new NasError() { Message = "Can not create the client", Code = 400 }, new JsonMediaTypeFormatter());               
            }

            return Ok(result);
        }

        // POST api/Account/Register
        [NasAuthorize(Roles="Admin, Web")]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserInfo userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser() { UserName = userModel.UserName };            

            IdentityResult result = await userService.RegisterUser(user,userModel.Password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("register/app")]
        public async Task<IHttpActionResult> Register(PostUserRequest userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = new ApplicationUser() { UserName = userModel.UserInfo.UserName };

            IdentityResult result = await userService.RegisterUser(user, userModel.UserInfo.Password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }


            Member member = memberService.Cretate(user.Id);
            bool created = await memberService.SaveChangesAsync();

            if (!created)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new NasError() { Message = "Cant create the member", Code = 400 }, new JsonMediaTypeFormatter());
            }

            Device device = AutoMapper.Mapper.Map<Device>(userModel.DeviceInfo);
            device.MemberId = member.MemberId;
            deviceService.CreateDevice(device);
            created = await  deviceService.SaveChangesAsync();
            if (!created)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, new NasError() { Message = "Cant create the device", Code = 400 }, new JsonMediaTypeFormatter());
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("Message", error);
                    }
                }

                if (ModelState.IsValid)
                {
                   // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
                
                return Content(System.Net.HttpStatusCode.BadRequest, new NasError() { Message = "User already exists", Code = 400 }, new JsonMediaTypeFormatter());
            }

            return null;
        }
    }
}
