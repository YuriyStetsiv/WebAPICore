using System.Collections.Generic;

namespace Lab3.Infrastructure.Sql.Models
{
    public partial class Sages
    {
        public Sages()
        {
            SagesBooks = new HashSet<SagesBooks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public byte[] Photo { get; set; }

        public ICollection<SagesBooks> SagesBooks { get; set; }
    }
}
