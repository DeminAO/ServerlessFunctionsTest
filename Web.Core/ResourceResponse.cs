namespace Web.Core
{
    public class ResourceResponseBase
    {
		public bool IsSucceed { get; set; }
		public string Error { get; set; }
    }

    public class ResourceResponse : ResourceResponseBase
    {
        public object Result { get; set; }
    }
}
