namespace Lab3.Infrastructure.Sql.Models
{
    public partial class BooksOrders
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }

        public Books Book { get; set; }
        public Orders Order { get; set; }
    }
}
