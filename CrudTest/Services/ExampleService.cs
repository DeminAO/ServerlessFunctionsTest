using CrudTest.Models;
using Web.Core;

namespace CrudTest
{
	public class ExampleService
	{
		public ResourceResponse Get(string result)
		{
			return new ResourceResponse()
			{
				Result = new ModelResponse(result), 
				IsSucceed = true 
			};
		}
	}
}
