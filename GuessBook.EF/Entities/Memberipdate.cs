using System;

namespace GuessBook.EF.Entities
{
    public partial class Memberipdate
    {
        public int Id { get; set; }
        public int? Memberid { get; set; }
        public DateTime? CodeDate { get; set; }
        public string Ip { get; set; }
        public byte? Status { get; set; }
    }
}
