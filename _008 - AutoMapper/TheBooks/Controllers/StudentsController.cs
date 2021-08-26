using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TheBooks.Models;
using TheBooks.Models.Common;
using TheBooks.Service;
using TheBooks.Service.Common;
using TheBooks.Common.Sort;
using TheBooks.Common.Pagination;
using TheBooks.Common.Filters;

using AutoMapper;
using Guards;


namespace TheBooks.Controllers
{
    [RoutePrefix("students")]
    public class StudentsController : ApiController
    {
        private IStudentsService _privateService;
        private IMapper _mapper;

        public StudentsController(IStudentsService studentsService, IMapper mapper)
        {
            Guard.ArgumentNotNull(() => studentsService);
            _privateService = studentsService;

            Guard.ArgumentNotNull(() => mapper);
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody()] REST_Student.CreateStudentRest rest)
        {
            if (rest == null) return BadRequest("Body empty.");

            var item = await _privateService.Create(_mapper.Map<Student>(rest));
            return Content(System.Net.HttpStatusCode.Created, _mapper.Map<IStudent, REST_Student.StudentRest>(item));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] Sort sort, [FromUri] Pagination pagination, [FromUri] StudentFilter filter)
        {
            var items = await _privateService.GetAll(sort, pagination, filter);
            return Ok(_mapper.Map<List<REST_Student.StudentRest>>(items));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            var item = await _privateService.GetByID(id);

            if (item == null) return NotFound();

            return Ok(_mapper.Map<REST_Student.StudentRest>(item));
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid id, [FromBody] REST_Student.UpdateStudentRest rest)
        {
            if (rest == null) return BadRequest("Body empty.");

            var item = await _privateService.Update(id, _mapper.Map<Student>(rest));

            if (item == null) return NotFound();

            return Ok(_mapper.Map<REST_Student.StudentRest>(item));
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

    public class REST_Student
    {
        public class StudentRest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Gender { get; set; }
        }
        public class CreateStudentRest
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Gender { get; set; }
        }
        public class UpdateStudentRest
        {
            #nullable enable
            public string? Name { get; set; }
            public string? Surname { get; set; }
            #nullable disable
        }
    }
}