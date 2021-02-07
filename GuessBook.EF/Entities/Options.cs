namespace GuessBook.EF.Entities
{
    public partial class Options
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string Text { get; set; }
        public string Photo { get; set; }
        public string PhotoAlt { get; set; }
        public string PhotoName { get; set; }
        public byte? Seq { get; set; }
    }
}
