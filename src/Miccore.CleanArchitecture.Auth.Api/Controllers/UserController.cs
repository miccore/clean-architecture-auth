using System.Net;
using MediatR;
using Miccore.CleanArchitecture.Auth.Api.Validators.User;
using Miccore.CleanArchitecture.Auth.Application.Commands.User;
using Miccore.CleanArchitecture.Auth.Application.Queries.User;
using Miccore.CleanArchitecture.Auth.Application.Responses.User;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.Pagination.Model;
using Miccore.Pagination.Service;
using Microsoft.AspNetCore.Mvc;

namespace Miccore.CleanArchitecture.Auth.Api.Controllers
{
    /// <summary>
    /// user api controller
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
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
        [HttpPost(template: "", Name = nameof(CreateUser))]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserCommand command)
        {
            try
            {
                // validate command
                var validator = new CreateUserValidator();
                var validate = validator.Validate(command);
                if(!validate.IsValid){
                    throw new ValidatorException(validate.ToString());
                }

                // call command
                var created = await _mediator.Send(command);

                // return response
                return HandleCreatedResponse(created);

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
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [HttpDelete(template: "{id}", Name = nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // delete existing movie
            try
            {
                // create command
                DeleteUserCommand command = new DeleteUserCommand(id);

                // call command
                var deleted = await _mediator.Send(command);

                // return response
                return HandleDeletedResponse();
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

        /// <summary>
        /// get all user paginate
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "", Name = nameof(GetAllUsers))]
        public async Task<ActionResult<PaginationModel<UserResponse>>> GetAllUsers([FromQuery] PaginationQuery query)
        {
            try
            {
                // create query
                GetAllUserQuery userQuery = new GetAllUserQuery(query);

                // call query
                var users = await _mediator.Send(userQuery);

                // return date if not paginate
                if(!query.paginate) return HandleSuccessResponse(users);

                // add next and previous links
                users.AddRouteLink(Url.RouteUrl(nameof(GetAllUsers)), query);

                // return response
                return HandleSuccessResponse(users);

            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "{id}", Name = nameof(GetUserById))]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            try
            {
                // create query
                GetUserByIdQuery userQuery = new GetUserByIdQuery(id);

                // call query
                var user = await _mediator.Send(userQuery);

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


        /// <summary>
        /// update user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(template: "", Name = nameof(UpdateUser))]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromBody] UpdateUserCommand command)
        {
            try
            {
                // validate command
                var validator = new UpdateUserValidator();
                var validate = validator.Validate(command);
                if(!validate.IsValid){
                    throw new ValidatorException(validate.ToString());
                }

                // call command
                var updated = await _mediator.Send(command);

                // return response
                return HandleSuccessResponse(updated);

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
    }
}