namespace Domain.DTO
{
    /// <summary>
    /// Indicates the parameters applied in a pagination response to a request
    /// </summary>
    public class PaginationQueryParameters
    {

        /// <summary>
        /// The page number 
        /// </summary>
        public int PageNo { get; set; } = 1;
        /// <summary>
        /// The size of the records/items in the response
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
