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

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            return Ok(userService.Users());
        }


        [HttpGet("id")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await userService.User(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            (CreateUserDto?, string message) result = await userService.CreateUser(createUserDto);

            if(result.Item1 == null)
            {
                return BadRequest(result.message);
            }

            //Send Otp
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











    }
}
