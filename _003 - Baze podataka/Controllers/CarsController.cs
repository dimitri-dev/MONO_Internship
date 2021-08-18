using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;

using _003___Baze_podataka.Models.Car;
using _003___Baze_podataka.Repositories;

namespace _003___Baze_podataka.Controllers
{
    [RoutePrefix("cars")]
    public class CarsController : ApiController
    {
        private static ICarRepository _privateRepository = new CarRepository();

        [HttpPost]
        public IHttpActionResult Create([FromBody()] CreateCarDto dto)
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
            Car item = _privateRepository.Get(id);

            if (item == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(item);
        }

        [HttpPut]
        public IHttpActionResult Update(Guid id, [FromBody] UpdateCarDto dto)
        {
            if (dto == null) return BadRequest("Body empty.");

            Car item = _privateRepository.Update(id, dto);

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