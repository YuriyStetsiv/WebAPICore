using System.Collections.Generic;

namespace Lab3.Infrastructure.Sql.Models
{
    public partial class Orders
    {
        public Orders()
        {
            BooksOrders = new HashSet<BooksOrders>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public bool IsCompleted { get; set; }

        public ICollection<BooksOrders> BooksOrders { get; set; }
    }
}
