using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http.Extensions;

namespace EmployeeWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            // Handle the uploaded image here, e.g., save it to a folder or process it.
            // Make sure to validate the image's content type and size before proceeding.

            // Example: Save the uploaded image to a folder
            if (image != null && image.Length > 0)
            {
                var filePath = Path.Combine("C:\\Users\\pavan\\OneDrive\\Desktop\\Interview_Prep", image.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Optionally, you can return a response to the client indicating successful upload.
                return Ok("Image uploaded successfully!");
            }

            return BadRequest("No image uploaded or invalid image format.");
        }

        //
        [HttpGet("image")]
        public IActionResult GetImage()
        {
            // Replace "your-image-path.jpg" with the actual path of the image on the server.
            var imagePath = "C:\\Users\\pavan\\OneDrive\\Desktop\\Interview_Prep\\bank PassBook.jpg";

            // Check if the image file exists
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Image not found.");
            }

            // Read the image file into a byte array
            byte[] imageData = System.IO.File.ReadAllBytes(imagePath);

            // Determine the content type based on the image file extension (e.g., jpg, png, etc.)
            string contentType = "image/jpeg"; // Replace with appropriate content type.

            // Return the image as a file result
            return File(imageData, contentType);
        }


    }
}