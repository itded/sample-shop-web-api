namespace SampleShopWebApi.DTO.Common
{
    public class UpdateResult<T> where T:class
    {
        public T Result { get; set; }

        public bool Success { get; set; }

        public string ExceptionMessage { get; set; }
    }
}
