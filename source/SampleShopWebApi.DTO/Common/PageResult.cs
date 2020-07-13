namespace SampleShopWebApi.DTO.Common
{
    /// <summary>
    /// Contains a page entries and a number of all entries.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> where T : class
    {
        public T Result { get; set; }

        public int TotalCount { get; set; }
    }
}
