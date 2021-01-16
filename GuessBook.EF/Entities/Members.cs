using System;
using System.Collections.Generic;

namespace GuessBook.EF.Entities
{
    public partial class Members
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public string ActivateCode { get; set; }
        public DateTime? CodeDate { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Username { get; set; }
    }
}
