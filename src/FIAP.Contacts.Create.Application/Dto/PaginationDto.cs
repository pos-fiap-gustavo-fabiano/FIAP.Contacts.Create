namespace FIAP.Contacts.Create.Application.Dto
{
    public class PaginationDto<T>
    {
        public PaginationDto(IEnumerable<T> items, int total, int page, int limit)
        {
            Items = items;
            Total = total;
            Page = page;
            Limit = limit;
        }

        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
