using System.Collections.Generic;

namespace Lab3.API.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public int Status { get; set; }
        public bool IsCompleted { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
