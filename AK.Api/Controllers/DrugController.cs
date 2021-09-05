using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using AK.Api.Extensions;
using AK.Application.Commands.Drug;
using AK.Application.DTOs;
using AK.Application.Queries.Drug;
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

        /// <summary>
        /// Get All Drugs , if you want to filter : http://localhost:5013/api/drug?searchString=uuj&sortOrder=price
        /// </summary>
        /// <param name="searchString">search into code or label</param>
        /// <param name="sortOrder">order items</param>
        /// <returns> List of Drugs</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string searchString, [FromQuery] string sortOrder)
        {
            try
            {
                _logger.LogDebug($"Enter method {nameof(GetAll)}");
                var model = new DrugGetAllQuery();
                var result = await _mediator.Send(model);
                if (!string.IsNullOrEmpty(searchString))
                {
                    result = result.Where(s => s.Label.Contains(searchString)
                                               || s.Code.Contains(searchString)).ToList();
                }

                result = sortOrder switch
                {
                    ApiConstant.SearchPriceDesc => result.OrderByDescending(s => s.Price).ToList(),
                    ApiConstant.SearchPrice => result.OrderBy(s => s.Code).ToList(),
                    ApiConstant.SearchCode => result.OrderBy(s => s.Price).ToList(),
                    _ => result.OrderBy(s => s.Label).ToList()
                };
                var pageSize = 5;
                var pageNumber = 1;
                var response = await PaginatedList<DrugDto>.CreateAsync(result, pageNumber, pageSize);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError("Cannot get drug with exception {0}", e.Message);
                throw new ArgumentException("cannot get drugs");
            }
            
        }

        /// <summary>
        /// Get Drug with Id , request example : http://localhost:5013/api/drug/GUID
        /// </summary>
        /// <returns> Drug </returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                _logger.LogDebug($"Enter method {nameof(Get)}");

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
            catch (Exception e)
            {
                _logger.LogError("Cannot get drug with exception {0}" , e.Message);
                throw new ArgumentException("cannot get drug");
            }
           
        }
        /// <summary>
        /// Add new Drug
        /// </summary>
        /// <param name="drug"></param>
        /// <returns>Created drug</returns>
        [HttpPost]
        public async Task<IActionResult> AddDrug([FromBody] DrugDto drug)
        {
            try
            {
                _logger.LogDebug($"Enter method {nameof(AddDrug)}");

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
            catch (Exception e)
            {
                _logger.LogError("Cannot get drug with exception {0}", e.Message);
                throw new ArgumentException("cannot add drug");
            }
           
        }

        /// <summary>
        /// Update the drug with Id
        /// </summary>
        /// <param name="drug"></param>
        /// <returns>Updated drug</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateDrug([FromBody] DrugDto drug)
        {
            try
            {
                _logger.LogDebug($"Enter method {nameof(UpdateDrug)}");

                if (drug is null)
                {
                    return BadRequest("Drug model is null");
                }
                var modelToSend = _mapper.Map<DrugUpdateCommand>(drug);
                var result = await _mediator.Send(modelToSend);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Cannot get drug with exception {0}", e.Message);
                throw new ArgumentException("cannot update drug");
            }
           
        }
        /// <summary>
        /// Delete drug
        /// </summary>
        /// <param name="drug"></param>
        /// <returns>bool</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteDrug([FromBody] DrugDto drug)
        {
            try
            {
                _logger.LogDebug($"Enter method {nameof(DeleteDrug)}");

                if (drug is null)
                {
                    return BadRequest("Drug model is null");
                }
                var modelToSend = _mapper.Map<DrugDeleteCommand>(drug);
                var result = await _mediator.Send(modelToSend);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError("Cannot get drug with exception {0}", e.Message);
                throw new ArgumentException("cannot delete drug");
            }
           
        }
    }
}
