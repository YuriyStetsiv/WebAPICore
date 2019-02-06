using Lab3.Domain.Models.Enums;
using System.Collections.Generic;

namespace Lab3.Domain.Models.Entities
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public OrderStatus Status { get; set; }
        public bool IsCompleted { get; set; }

        public IEnumerable<BooksOrderModel> BooksOrders { get; set; }
    }
}
