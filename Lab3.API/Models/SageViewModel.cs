using System.Collections.Generic;

namespace Lab3.API.Models
{
    public class SageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public byte[] Photo { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }
    }
}
