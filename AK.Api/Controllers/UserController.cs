using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using AK.Application.Commands.User;
using AK.Application.DTOs;
using AK.Application.Queries.User;
using Microsoft.Extensions.Logging;

namespace AK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, IMapper mapper,ILogger<UserController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet("{id}",Name = "GetUser")]
        public async Task<IActionResult> Get(Guid id)
        {
            var model = new GetUserByIdQuery(id);
            var result = await _mediator.Send(model);
            return Ok(result ??= null);
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUser([FromBody] UserDto user)
        {
            if (user is null)
            {
                return BadRequest("User is null");
            }
            var modelToSend = _mapper.Map<UserAddCommand>(user);
            var result = await _mediator.Send(modelToSend);
            return Ok(result);
        }
    }
}
