using AutoMapper;
using Loza.Application.Models.SharedModels;
using Microsoft.AspNetCore.Mvc;


namespace Loza.API.Controllers
{
    [Controller]
    public class ResponsesController : ControllerBase
    {
        private readonly IMapper _mapper;


        public ResponsesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        internal ActionResult HandleApiResponse<T>(OperationsResult<T> result)
        {

            if (result?.IsError == false)
            {
                result.StatusCode = 200;
                return Ok(result);
            }

            if (result?.StatusCode == 404)
            {

                return NotFound(result);
            }
            if (result?.StatusCode == 500)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
   
            return BadRequest(result);

        }
    }
}
