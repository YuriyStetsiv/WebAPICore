using System.Collections.Generic;

namespace Lab3.Infrastructure.Sql.Models
{
    public partial class Books
    {
        public Books()
        {
            BooksOrders = new HashSet<BooksOrders>();
            SagesBooks = new HashSet<SagesBooks>();
            UserCart = new HashSet<UserCart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public ICollection<BooksOrders> BooksOrders { get; set; }
        public ICollection<SagesBooks> SagesBooks { get; set; }
        public ICollection<UserCart> UserCart { get; set; }
    }
}
