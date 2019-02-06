using System.Collections.Generic;

namespace Lab3.API.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public IEnumerable<SageViewModel> Sages { get; set; }
    }
}
