namespace Lab3.Infrastructure.Sql.Models
{
    public partial class UserCart
    {
        public string UserId { get; set; }
        public int BookId { get; set; }

        public Books Book { get; set; }
    }
}
