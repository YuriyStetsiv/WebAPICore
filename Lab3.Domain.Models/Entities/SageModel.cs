using System.Collections.Generic;

namespace Lab3.Domain.Models.Entities
{
    public class SageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public byte[] Photo { get; set; }

        public IEnumerable<SagesBooksModel> SagesBooks { get; set; }
    }
}
