using System.Collections.Generic;

namespace Lab3.Domain.Models.Entities
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public IEnumerable<SagesBooksModel> SagesBooks{ get; set; }
    }
}
