using AutoMapper;
using EmployeesBE.Models;
using EmployeesBE.Models.DTO;
using EmployeesBE.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeesBE.Controllers
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _db;
        protected APIResponse _response;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet(Name = "GetEmployees")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> GetEmployees()
        {
            try
            {
                _response.Result = await _db.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        [ProducesResponseType(200, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode(400, _response);
                }
                var employee = await _db.GetByIdAsync(id);
                if (employee == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<EmployeeDTO>(employee);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                if (employeeDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode(400, _response);
                }
                if (employeeDTO.Id > 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode(400, _response);
                }

                var employee = _mapper.Map<Employee>(employeeDTO);
                await _db.CreateAsync(employee);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = employee;
                return StatusCode(201, _response);
                //return CreatedAtRoute("GetEmployee", new { id = employee.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return StatusCode(500, _response);
            }
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialEmployee")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> UpdatePartialEmployee(int id, JsonPatchDocument<EmployeeDTO> patchDTO)
        {
            try
            {
                if (patchDTO == null | id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode(400, _response);
                }
                var employee = await _db.GetByIdAsync(id);

                var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                if (employee == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode(400, _response);
                }
                patchDTO.ApplyTo(employeeDTO, ModelState);

                var model = _mapper.Map<Employee>(employeeDTO);

                await _db.UpdateAsync(model);

                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return StatusCode(400, _response);
                }
                _response.StatusCode = HttpStatusCode.NoContent;
                return StatusCode(204, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return StatusCode(500, _response);
            }
        }

        [HttpDelete("{id}", Name = "DeleteEmployee")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<APIResponse>> DeleteEmployee(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var employee = await _db.GetByIdAsync(id);
                if (employee == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _db.DeleteAsync(employee);

                _response.StatusCode = HttpStatusCode.NoContent;
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages = new List<string>() { ex.Message };
                return StatusCode(500, _response);
            }
        }
    }
}
