namespace SampleShopWebApi.DTO.Common
{
    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public string PrevPageLink { get; set; }

        public string NextPageLink { get; set; }
    }
}
