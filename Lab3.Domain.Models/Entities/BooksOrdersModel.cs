namespace Lab3.Domain.Models.Entities
{
    public class BooksOrderModel
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }

        public BookModel Book { get; set; }
        public OrderModel Order { get; set; }
    }
}
