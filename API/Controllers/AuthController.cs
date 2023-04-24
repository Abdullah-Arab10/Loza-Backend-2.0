using Microsoft.AspNetCore.Mvc;
using Application.Features.Auths.Commands;
using MediatR;
using AutoMapper;
using Loza.API.Contracts.Auths.Requests;
using Loza.Application.Features.Auths.Commands;
using Loza.Application.Models.AuthModels;

namespace Loza.API.Controllers
{

    [Route(ApiRoutes.AuthRoute)]
    [ApiController]
    public class AuthController : ResponsesController
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator,IMapper mapper) : base(mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(ApiRoutes.Auth.Login)]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var loginModel = _mapper.Map<LoginModel>(loginRequest);
            var command = new LoginCommand(loginModel);
            var response = await _mediator.Send(command);
            return HandleApiResponse(response);

        }

        [HttpPost]
        [Route(ApiRoutes.Auth.Register)]
        public async Task<ActionResult> Register(RegisterRequest user)
        {
            var userObj = _mapper.Map<RegisterModel>(user);
            var command = new RegisterCommand(userObj);
            var response = await _mediator.Send(command);
            return HandleApiResponse(response);
        }
    }
}
