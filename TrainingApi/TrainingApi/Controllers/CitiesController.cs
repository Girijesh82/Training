using Microsoft.AspNetCore.Mvc;
using TrainingApi.Model;

namespace TrainingApi.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController: ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CitiesDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }


        [HttpGet("{id}")]
        public ActionResult<CitiesDto> GetCitiesById(int id)
        {

            var city=CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null)
                return NoContent();
            return Ok(city);
        }
    }
}
