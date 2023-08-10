using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TrainingApi.Model;

namespace TrainingApi.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Places>> GetPointsOfInterest(int cityId)
        {
            var cityOfIntrest=CitiesDataStore.Current.Cities.FirstOrDefault(city=> city.Id==cityId);
            if (cityOfIntrest == null) return NotFound();
                   

            return Ok(cityOfIntrest.PlacesToVisit);
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointsOfInterest")]
        public ActionResult<Places> GetPointsOfInterest(int cityId, int pointofinterestid)
        {
            var city= CitiesDataStore.Current.Cities.FirstOrDefault( city=> city.Id==cityId);

            if (city == null) return NotFound();

            var pointofinterest= city.PlacesToVisit.FirstOrDefault(place=> place.Id== pointofinterestid);
           if(pointofinterest==null) return NotFound();
           return Ok(pointofinterest);
        }

        [HttpPost]
        public ActionResult<Places> CreatePointOfInterest(int cityId,PointOfInterestForCreationDto pointOfInterest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            if(city == null) return NotFound();

            var maxPlacesId = CitiesDataStore.Current.Cities.SelectMany(c => c.PlacesToVisit).Max(Place => Place.Id);

            var finalPlace = new Places {
            Id=maxPlacesId +1,
            Name= pointOfInterest.Name,
            Description= pointOfInterest.Description
            };

            city.PlacesToVisit.Add(finalPlace);

            //return Ok(city);
            return CreatedAtRoute("GetPointsOfInterest",
                new
                {
                    cityId = cityId,
                    pointofinterestid = finalPlace.Id
                },
                finalPlace
                );


        }

        [HttpPut("{pointofinterestid}")]
        public ActionResult UpdatePointOfIntrest(int cityId, int pointofinterestid, PointOfInterestForUpdateDto pointOfIntrest )
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            if (city == null) return NotFound();

            // find point of intrest
            var pointOfIntrestFromStore = city.PlacesToVisit.FirstOrDefault(poi => poi.Id == pointofinterestid);
            if (pointOfIntrestFromStore == null) return NotFound();

            pointOfIntrestFromStore.Name = pointOfIntrest.Name;
            pointOfIntrestFromStore.Description = pointOfIntrest.Description;
            
            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        public ActionResult PartiallyUpdatePointOfIntrest(int cityId, int pointofinterestid, 
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == cityId);
            if (city == null) return NotFound();

            // find point of intrest
            var pointOfIntrestFromStore = city.PlacesToVisit.FirstOrDefault(poi => poi.Id == pointofinterestid);
            if (pointOfIntrestFromStore == null) return NotFound();

            var pointOfIntrestToPatch = new PointOfInterestForUpdateDto
            {
                Name = pointOfIntrestFromStore.Name,
                Description = pointOfIntrestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfIntrestToPatch);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!TryValidateModel(pointOfIntrestToPatch))
                return BadRequest(ModelState);

            pointOfIntrestFromStore.Name= pointOfIntrestToPatch.Name;
            pointOfIntrestFromStore.Description= pointOfIntrestToPatch.Description;
            return NoContent();


        }

    }
}
