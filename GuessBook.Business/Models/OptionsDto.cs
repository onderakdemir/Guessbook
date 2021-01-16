using System;
using System.Collections.Generic;
using System.Text;

namespace GuessBook.Business.Models
{
    public class OptionsDto
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
