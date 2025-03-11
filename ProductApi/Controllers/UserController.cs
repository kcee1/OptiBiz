using BusinessLogicLayer.IServices;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace OptiBizApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserVerificationService userVerificationService;


        public UserController(IUserService userService, IUserVerificationService userVerificationService)
        {
            this.userService = userService;
            this.userVerificationService = userVerificationService;
        }



        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(userService.Users());
        }


        [HttpGet]
        public IActionResult GetTenantUsers(int tenantId)
        {
            return Ok(userService.GetTenantUser(tenantId));
        }


        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(await userService.User(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            (GetUserDto?, string message) result = await userService.CreateUser(createUserDto);

            if(result.Item1 == null)
            {
                return BadRequest(result.message);
            }

            
            return Ok(result.Item1);

        }


        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string userId, string roleName)
        {
            bool result = await userService.AssignUserToRole(userId, roleName);

            if (!result)
            {
                return BadRequest("Unable to assign user to role");

            }

            return Ok("Assigned Successfully");
        }


        
        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string userId, string roleName)
        {
            bool result = await userService.RemoveUserFromRole(userId, roleName);

            if (!result)
            {
                return BadRequest("Unable to assign user to role");

            }

            return Ok("Assigned Successfully");


        }


        [HttpGet]
        public async Task<IActionResult> SendOtp(string userId, string email)
        {
            (bool, string message) result = await userVerificationService.CreateOtp(userId, email);

            if (result.Item1)
            {
                return Ok(result.message);
            }

            return BadRequest(result.message);


        }

        [HttpGet]
        public async Task<IActionResult> VerifyOtp(string userId, string Otp)
        {
            bool result = await userVerificationService.VerifyOtp(userId, Otp);

            if (result)
            {
                return Ok("Verified Successfully");
            }

            return BadRequest("Invalid Otp");


        }


        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string userId, string Otp)
        {
            bool result = await userVerificationService.VerifyOtp(userId, Otp);

            if (result)
            {
                (bool, string message) theResult = await userService.UpdateUser(userId);
                if (theResult.Item1)
                {
                    return Ok(theResult.message);
                }

                return BadRequest("Invalid Otp");

            }

            return BadRequest("Invalid Otp");


        }











    }
}
