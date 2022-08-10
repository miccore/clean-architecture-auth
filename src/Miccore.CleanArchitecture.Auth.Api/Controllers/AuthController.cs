using System.Net;
using MediatR;
using Miccore.CleanArchitecture.Auth.Api.Validators.User;
using Miccore.CleanArchitecture.Auth.Application.Commands.Auth;
using Miccore.CleanArchitecture.Auth.Application.Queries.Auth;
using Miccore.CleanArchitecture.Auth.Application.Responses.Auth;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Enumerations;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.Pagination.Model;
using Miccore.Pagination.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Miccore.CleanArchitecture.Auth.Api.Controllers
{
    /// <summary>
    /// user api controller
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// create user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(template: "login", Name = nameof(Login))]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginCommand command)
        {
            try
            {
                // validate command
                var validator = new LoginValidator();
                var validate = validator.Validate(command);
                if(!validate.IsValid){
                    throw new ValidatorException(validate.ToString());
                }

                // call command
                var loggedIn = await _mediator.Send(command);

                //Response cookies
                Response.Cookies.Append("X-Refresh-Token", loggedIn.User.RefreshToken, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.None, Secure = true});
                Response.Cookies.Append("X-Access-Token", loggedIn.Token, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.None, Secure = true});

                // return response
                return HandleCreatedResponse(loggedIn);

            }
            // not found exception
            catch (NotFoundException notFound)
            {
                return HandleErrorResponse(HttpStatusCode.NotFound, notFound.Message);
            }
            // invalid data validation exception
            catch (ValidatorException invalid)
            {
                return HandleErrorResponse(HttpStatusCode.BadRequest, invalid.Message);
            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// refresh token
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "refresh/token", Name = nameof(RefreshToken))]
        [EnableCors("CorsPolicy")]
        [Authorize]
        public async Task<ActionResult<AuthResponse>> RefreshToken()
        {
            try
            {
                // check cookies
                if (!(Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)))
                    return HandleErrorResponse(HttpStatusCode.BadRequest, ExceptionEnum.COOCKIE_NOT_FOUND.ToString());

                var refresh = Request.Cookies.Where(x => x.Key == "X-Refresh-Token").FirstOrDefault();


                // create command
                RefreshTokenCommand command = new RefreshTokenCommand(refresh.Value);

                // set command
                var refreshed = await _mediator.Send(command);

                 //Response cookies
                Response.Cookies.Append("X-Refresh-Token", refreshed.User.RefreshToken, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.None, Secure = true});
                Response.Cookies.Append("X-Access-Token", refreshed.Token, new CookieOptions(){HttpOnly = true, SameSite = SameSiteMode.None, Secure = true});
                
                // return response
                return HandleSuccessResponse(refreshed);

            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// get authenticated user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "authenticated", Name = nameof(Authenticated))]
        public async Task<ActionResult<UserResponse>> Authenticated(int id)
        {
            try
            {
                // check cookies
                if (!(Request.Headers.TryGetValue("Authorization", out var accessToken)))
                    return HandleErrorResponse(HttpStatusCode.BadRequest, ExceptionEnum.HEADER_NOT_FOUND.ToString());
                
                var access = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault();
                accessToken = access.Value.ToString().Replace("Bearer ", "");

                // create query
                GetAuthenticatedUserQuery query = new GetAuthenticatedUserQuery(accessToken);
                
                // get user
                var user = await _mediator.Send(query);

                // return response  
                return HandleSuccessResponse(user);

            }
            // not found exception
            catch (NotFoundException notFound)
            {
                return HandleErrorResponse(HttpStatusCode.NotFound, notFound.Message);
            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
