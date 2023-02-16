using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Models;
using StudentRegWebApi.Services;

namespace StudentRegWebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentInfoController : ControllerBase
    {
        private readonly IStudentInfoService _studentInfoService;
        public StudentInfoController(IStudentInfoService studentInfoService)
        {
            _studentInfoService = studentInfoService;
        }
        // GET api/studentinfo
        [HttpGet]
        public IEnumerable<StudentInfo> GetStudentInfos() => _studentInfoService.GetAll();

        // GET api/studentinfo/id
        [HttpGet("{id}", Name = nameof(GetStudentInfoById))]
        public IActionResult GetStudentInfoById(int id)
        {
            StudentInfo studentInfo = _studentInfoService.Find(id);
            if (studentInfo == null) 
                return NotFound(); 
            else 
                return new ObjectResult(studentInfo);
        }

        // POST api/studentinfo
        [HttpPost]
        public IActionResult PostStudentInfo([FromBody]StudentInfo studentinfo)
        {
            if (studentinfo == null) return BadRequest();            
            int retVal= _studentInfoService.Add(studentinfo);            
            if (retVal > 0) return Ok(); else return NotFound();
        }
        // PUT api/studentinfo/guid
        [HttpPut("{id}")]
        public IActionResult PutStudentInfo(int id,[FromBody] StudentInfo studentinfo)
        {
            if (studentinfo == null || id != studentinfo.Id) return BadRequest();
            if (_studentInfoService.Find(id) == null) return NotFound();
            int retVal = _studentInfoService.Update(studentinfo);
            if (retVal > 0) return Ok(); else return NotFound();            
        }

        // DELETE api/studentinfo/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal= _studentInfoService.Remove(id);
            if (retVal > 0) return Ok(); else return NotFound();
        }
    }
}