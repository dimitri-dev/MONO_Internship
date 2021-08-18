using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using _003___Baze_podataka.Repositories;
using _003___Baze_podataka.Models.Student;
using System.Net;

namespace _003___Baze_podataka.Controllers
{
    [RoutePrefix("students")]
    public class StudentsController : ApiController
    {
        private static IStudentRepository _privateRepository = new StudentRepository();

        [HttpPost]
        public IHttpActionResult Create([FromBody()] CreateStudentDto dto)
        {
            if (dto == null) return BadRequest("Body empty.");

            var item = _privateRepository.Create(dto);
            return Content(System.Net.HttpStatusCode.Created, item);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var items = _privateRepository.GetAll();
            return Ok(items);
        }

        [HttpGet]
        public IHttpActionResult GetById(Guid id)
        {
            Student item = _privateRepository.Get(id);

            if (item == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Update(Guid id, [FromBody] UpdateStudentDto dto)
        {
            if (dto == null) return BadRequest("Body empty.");

            Student item = _privateRepository.Update(id, dto);

            if (item == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(item);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                _privateRepository.Delete(id);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}