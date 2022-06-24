using System.Net;
using MediatR;
using Miccore.CleanArchitecture.Auth.Api.Validators.Role;
using Miccore.CleanArchitecture.Auth.Application.Commands.Role;
using Miccore.CleanArchitecture.Auth.Application.Queries.Role;
using Miccore.CleanArchitecture.Auth.Application.Responses.Role;
using Miccore.CleanArchitecture.Auth.Core.Exceptions;
using Miccore.Pagination.Model;
using Miccore.Pagination.Service;
using Microsoft.AspNetCore.Mvc;

namespace Miccore.CleanArchitecture.Auth.Api.Controllers
{
    /// <summary>
    /// role api controller
    /// </summary>

    [Route("[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// create role
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost(template: "", Name = nameof(CreateRole))]
        public async Task<ActionResult<RoleResponse>> CreateRole([FromBody] CreateRoleCommand command)
        {
            try
            {
                // validate command
                var validator = new CreateRoleValidator();
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
        /// delete role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]    
        [HttpDelete(template: "{id}", Name = nameof(DeleteRole))]
        public async Task<IActionResult> DeleteRole(int id)
        {
            // delete existing movie
            try
            {
                // create command
                DeleteRoleCommand command = new DeleteRoleCommand(id);

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
        /// get all role paginate
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "", Name = nameof(GetAllRoles))]
        public async Task<ActionResult<PaginationModel<RoleResponse>>> GetAllRoles([FromQuery] PaginationQuery query)
        {
            try
            {
                // create query
                GetAllRoleQuery roleQuery = new GetAllRoleQuery(query);

                // call query
                var roles = await _mediator.Send(roleQuery);

                // return date if not paginate
                if(!query.paginate) return HandleSuccessResponse(roles);

                // add next and previous links
                roles.AddRouteLink(Url.RouteUrl(nameof(GetAllRoles)), query);

                // return response
                return HandleSuccessResponse(roles);

            }
            // general exception
            catch (Exception ex)
            {
                return HandleErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// get role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(template: "{id}", Name = nameof(GetRoleById))]
        public async Task<ActionResult<RoleResponse>> GetRoleById(int id)
        {
            try
            {
                // create query
                GetRoleByIdQuery roleQuery = new GetRoleByIdQuery(id);

                // call query
                var role = await _mediator.Send(roleQuery);

                // return response
                return HandleSuccessResponse(role);

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
        /// update role
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut(template: "", Name = nameof(UpdateRole))]
        public async Task<ActionResult<RoleResponse>> UpdateRole([FromBody] UpdateRoleCommand command)
        {
            try
            {
                // validate command
                var validator = new UpdateRoleValidator();
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