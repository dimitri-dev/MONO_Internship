using System;
using System.Threading.Tasks;
using System.Web.Http;
using TheBooks.Models;
using TheBooks.Service;
using TheBooks.Service.Common;

namespace TheBooks.Controllers
{
    [RoutePrefix("students")]
    public class StudentsController : ApiController
    {
        private static IStudentsService _privateService = new StudentsService();

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody()] CreateStudentDto dto)
        {
            if (dto == null) return BadRequest("Body empty.");

            var item = await _privateService.Create(dto);
            return Content(System.Net.HttpStatusCode.Created, item);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var items = await _privateService.GetAll();
            return Ok(items);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            var item = await _privateService.GetByID(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid id, [FromBody] UpdateStudentDto dto)
        {
            if (dto == null) return BadRequest("Body empty.");

            var item = await _privateService.Update(id, dto);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceResult = await _privateService.Delete(id);

            if (serviceResult == true)
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            else
                return NotFound();

        }
    }
}