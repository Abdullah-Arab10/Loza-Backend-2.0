using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Loza.API.Contracts.Shared.Requests;
using Loza.API.Contracts.UserProfiles.Requests;
using Loza.Application.Models.SharedModels;
using Loza.Application.Features.UserProfiles.Queries;
using Loza.Application.Models.UserProfileModels;
using Loza.Application.Features.UserProfiles.Commands;

namespace Loza.API.Controllers
{
    [ApiController]
    [Route(ApiRoutes.BaseRoute)]
    public class UsersController : ResponsesController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper) : base(mapper)
        {
            
            _mediator = mediator;
            _mapper = mapper;
        
        }
        [HttpGet]
        [Route(ApiRoutes.User.GetAllUser)]
        public async Task<ActionResult> GetAllProfiles([FromQuery]QueriesRequest? queryRequest)
        {
            var queryMap = _mapper.Map<QueriesModel>(queryRequest);
            var query = new UserGetAllUsersQuery(queryMap);
            var response = await _mediator.Send(query);
            return HandleApiResponse(response);
        }

        [HttpGet]
        [Route(ApiRoutes.User.GetUserById)]
        public async Task<ActionResult> GetUserProfileById(int id)
        {
            var query = new UserGetUserByIdQuery { UserId = id };
            var response = await _mediator.Send(query);
            return HandleApiResponse(response);
        }



        [HttpPost]
        [Route(ApiRoutes.User.CreateUser)]
        public async Task<ActionResult> CreateUserProfile(UserCreateRequest user)
        {
            var userObj = _mapper.Map<UserCreateModel>(user);
            var command = new UserCreateCommand(userObj);
            var response = await _mediator.Send(command);
            return HandleApiResponse(response);
        }




        [HttpPut]
        [Route(ApiRoutes.User.UpdateUser)]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest requestData)
        {

            var newData = _mapper.Map<UserUpdateModel>(requestData);
            var command = new UserUpdateCommand(newData);
            var response = await _mediator.Send(command);

            return HandleApiResponse(response);

        }

        [HttpDelete]
        [Route(ApiRoutes.User.DeleteUser)]
        public async Task<IActionResult> DeleteUser(TodoByIdRequest request)
        {
            {
                var command = new UserDeleteCommand() { UserId = request.id };
                var response = await _mediator.Send(command);

                return HandleApiResponse(response);
            }
        }
    }
}
