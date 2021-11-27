using System;

namespace CrudTest.Models
{
	public class ModelResponse
	{
		public string Result { get; set; }
		
		public ModelResponse(string result)
		{
			Result = result ?? throw new NullReferenceException("result was not set");
		}
	}
}
