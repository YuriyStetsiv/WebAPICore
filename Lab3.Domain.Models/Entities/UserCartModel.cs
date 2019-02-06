namespace Lab3.Domain.Models.Entities
{
    public class UserCartModel
    {
        public string UserId { get; set; }
        public int BookId { get; set; }

        public BookModel Book { get; set; }
    }
}
