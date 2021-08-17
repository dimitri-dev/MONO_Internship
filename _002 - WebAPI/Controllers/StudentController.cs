using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using _002___WebAPI.Models;

namespace _002___WebAPI.Controllers
{
    public class StudentController : ApiController
    {
        // For a better approach, see "Using the Web API Dependency Resolver".
        static readonly IStudentRepository repository = new StudentRepository();

        // GET: api/Student
        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return repository.GetAll();
        }

        // GET: api/Student/5
        [HttpGet]
        public Student GetStudent(Guid id)
        {
            Student item = repository.Get(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return item;
        }

        // POST: api/Student
        [HttpPost]
        public HttpResponseMessage PostStudent(Student student)
        {
            student = repository.Add(student);
            var response = Request.CreateResponse<Student>(HttpStatusCode.Created, student);
            
            string uri = Url.Link("DefaultApi", new { id = student.Id });
            response.Headers.Location = new Uri(uri);

            return response;
        }

        // PUT: api/Student/5
        [HttpPut]
        public void PutStudent(Guid id, Student student)
        {
            student.SetNewGuid(id);

            // Student doesn't exist
            if (!repository.Update(student))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/Student/5
        [HttpDelete]
        public void DeleteStudent(Guid id)
        {
            Student item = repository.Get(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            repository.Remove(id);
        }
    }
}
