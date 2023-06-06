using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Udemy_NZWalks.API.Controllers
{

    //https://localhost:portnumber/api/students
    [Route("api/[controller]")] //the URI to the controller
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //GET: https://localhost:portnumber/api/students
        [HttpGet] //GET ATTRIBUTE

        //runs this method
        public IActionResult GetAllStudents()
        {
            string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };

            return Ok(studentNames);
            //passes the studentNames object back to the swagger caller
        }

    }
}
