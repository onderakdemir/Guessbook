using System;
using System.Collections.Generic;

namespace GuessBook.EF.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int? PermGroupId { get; set; }
    }
}
