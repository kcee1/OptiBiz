using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepositories;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace OptiBizApi.MiddleWare
{
    /// <summary>
    /// Basic Authentication Handler
    /// </summary>
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public BasicAuthenticationHandler(
        IUserRepository userRepository,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        UserManager<User> userManager)
        : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }


        /// <summary>
        /// Handle Authenticate Async
        /// </summary>
        /// <returns>Authenticate Result</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {


                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var username = credentials[0];
                var password = credentials[1];

               
                // Replace this with your actual user validation logic
                User? theUser = await _userRepository.getUserbyUserName(username);
                if (theUser == null || !await _userManager.CheckPasswordAsync(theUser, password))
                {
                    return AuthenticateResult.Fail("Invalid User name or Password");
                }


                var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, theUser.Id.ToString()),
                new Claim(ClaimTypes.Name, username),
            };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }

    }
}
