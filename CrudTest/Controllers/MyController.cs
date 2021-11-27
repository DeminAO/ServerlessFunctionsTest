using Microsoft.AspNetCore.Mvc;
using Web.Core;

namespace CrudTest
{
	[ApiController]
	[Route("api/[controller]")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Пометьте члены как статические")]
	public class MyController : ControllerBase
	{
		public ResourceResponse Get([FromServices] ExampleService exampleService)
			=> exampleService.Get("some");

		[HttpGet("throw")]
		public ResourceResponse ThrowedGet([FromServices] ExampleService exampleService)
			=> exampleService.Get(null);
	}
}
