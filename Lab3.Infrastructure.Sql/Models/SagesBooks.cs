namespace Lab3.Infrastructure.Sql.Models
{ 
    public partial class SagesBooks
    {
        public int BookId { get; set; }
        public int SageId { get; set; }

        public Books Book { get; set; }
        public Sages Sage { get; set; }
    }
}
