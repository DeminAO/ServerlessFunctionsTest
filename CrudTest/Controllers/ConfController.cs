using CrudTest.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Core;

namespace CrudTest
{
	[ApiController]
	[Route("api/[controller]")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Пометьте члены как статические")]
	public class ConfController : ControllerBase
	{
		[HttpGet]
		public ResourceResponse Get([FromServices] TestConfig testConfig)
			=> new() { IsSucceed = true, Result = testConfig?.TestString };
	}
}
