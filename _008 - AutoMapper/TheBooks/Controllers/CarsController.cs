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
    [RoutePrefix("cars")]
    public class CarsController : ApiController
    {
        private ICarsService _privateService;
        private IMapper _mapper;

        public CarsController(ICarsService carsService, IMapper mapper)
        {
            Guard.ArgumentNotNull(() => carsService);
            _privateService = carsService;

            Guard.ArgumentNotNull(() => mapper);
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody()] REST_Car.CreateCarRest rest)
        {
            if (rest == null) return BadRequest("Body empty.");

            var item = await _privateService.Create(_mapper.Map<Car>(rest));
            return Content(System.Net.HttpStatusCode.Created, _mapper.Map<ICar, REST_Car.CarRest>(item));
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri] Sort sort, [FromUri] Pagination pagination, [FromUri] CarFilter filter)
        {
            var items = await _privateService.GetAll(sort, pagination, filter);
            return Ok(_mapper.Map<List<REST_Car.CarRest>>(items));
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            var item = await _privateService.GetByID(id);

            if (item == null) return NotFound();

            return Ok(_mapper.Map<REST_Car.CarRest>(item));
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(Guid id, [FromBody] REST_Car.UpdateCarRest rest)
        {
            if (rest == null) return BadRequest("Body empty.");

            var item = await _privateService.Update(id, _mapper.Map<Car>(rest));

            if (item == null) return NotFound();

            return Ok(_mapper.Map<REST_Car.CarRest>(item));
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

    public class REST_Car
    {
        public class CarRest
        {
            public Guid Id { get; set; }
            public string Registration { get; set; }
            public Guid StudentID { get; set; }
            public IStudent Student { get; set; }
        }

        public class CreateCarRest
        {
            public string Registration { get; set; }
            public Guid StudentId { get; set; }
        }
        public class UpdateCarRest
        {
#nullable enable
            public string? Registration { get; set; }
            public Guid? StudentId { get; set; }
#nullable disable
        }
    }
}