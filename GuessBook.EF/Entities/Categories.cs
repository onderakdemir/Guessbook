namespace GuessBook.EF.Entities
{
    public partial class Categories
    {
        public int Id { get; set; }
        public byte? Inx { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string Icon { get; set; }
    }
}
