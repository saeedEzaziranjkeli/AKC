using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AK.Api.Extensions;
using AK.Application.Commands.Drug;
using AK.Application.Commands.User;
using AK.Application.DTOs;
using AK.Application.Queries;
using AK.Application.Queries.Drug;
using AK.Application.Queries.User;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace AK.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DrugController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<DrugController> _logger;

        public DrugController(IMediator mediator, IMapper mapper, ILogger<DrugController> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string searchString, [FromQuery] string sortOrder)
        {
            var model = new DrugGetAllQuery();
            var result = await _mediator.Send(model);
            if (!string.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.Label.Contains(searchString)
                                           || s.Code.Contains(searchString)).ToList();
            }

            result = sortOrder switch
            {
                "price_desc" => result.OrderByDescending(s => s.Price).ToList(),
                "code" => result.OrderBy(s => s.Code).ToList(),
                "price" => result.OrderBy(s => s.Price).ToList(),
                _ => result.OrderBy(s => s.Label).ToList()
            };
            int pageSize = 5;
            var pageNumber = 1;
            var response = await PaginatedList<DrugDto>.CreateAsync(result, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Id is null");
                return BadRequest("Id is null");
            }
            var modelToSend = new DrugDto()
            {
                Id = id
            };
            var model = new DrugGetByIdQuery(modelToSend);
            var result = await _mediator.Send(model);
            return Ok(result ??= null);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddDrug([FromBody] DrugDto drug)
        {
            if (drug is null)
            {
                return BadRequest("Drug model is null");
            }

            if (string.IsNullOrWhiteSpace(drug.Id))
            {
                drug.Id = Guid.NewGuid().ToString();
            }
            var modelToSend = _mapper.Map<DrugCreateCommand>(drug);
            var result = await _mediator.Send(modelToSend);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDrug([FromBody] DrugDto drug)
        {
            if (drug is null)
            {
                return BadRequest("Drug model is null");
            }
            var modelToSend = _mapper.Map<DrugUpdateCommand>(drug);
            var result = await _mediator.Send(modelToSend);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDrug([FromBody] DrugDto drug)
        {
            if (drug is null)
            {
                return BadRequest("Drug model is null");
            }
            var modelToSend = _mapper.Map<DrugDeleteCommand>(drug);
            var result = await _mediator.Send(modelToSend);
            return Ok(result);
        }
    }
}
