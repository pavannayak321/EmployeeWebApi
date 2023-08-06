using AutoMapper;
using EmployeeWebApi.Data;
using EmployeeWebApi.Model;
using EmployeeWebApi.Model.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeDbContext _emp;
        private readonly IMapper _autoMapper;
        private readonly ResponseDTO _responseDTO;
        public EmployeesController(EmployeeDbContext dbContext,IMapper mapper)
        {
            _emp = dbContext;
            _autoMapper = mapper;
            _responseDTO= new ResponseDTO();
        }
        // GET: api/<EmployeesController>
        [HttpGet]
        public ResponseDTO Get()
        {
            try
            {
                IEnumerable<Employee> emp = _emp.Employees.ToList();
                IEnumerable<EmployeeDTO> emp_DTO = _autoMapper.Map<IEnumerable<EmployeeDTO>>(emp);
                _responseDTO.Result = emp_DTO;
            }
            catch (Exception ex) 
            {
                _responseDTO.ISSuccess = false;
                _responseDTO.ErrorMessage = ex.Message;
            }

            return _responseDTO;
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public ResponseDTO Get(string id)
        {
          var  x =  _emp.Employees.FirstOrDefault(x=>x.EmployeeId.Equals(id));
            if (x != null)
            {
                try
                {
                    var x1 = _autoMapper.Map<EmployeeDTO>(x);
                    _responseDTO.Result = x1;
                    return _responseDTO;
                }
                catch(Exception e)
                {
                    _responseDTO.ISSuccess = false;
                    _responseDTO.ErrorMessage = e.Message;
                }
                
            }
            return _responseDTO;
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public void Post([FromBody] Employee empdata)
        {
            if (empdata.EmployeeId != null)
            {
                _emp.Employees.Add(empdata);
                _emp.SaveChanges();
            }
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
