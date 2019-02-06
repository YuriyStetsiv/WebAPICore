namespace Lab3.Domain.Models.Entities
{
    public class SagesBooksModel
    {
        public int BookId { get; set; }
        public int SageId { get; set; }

        public BookModel Book { get; set; }
        public SageModel Sage { get; set; }
    }
}
