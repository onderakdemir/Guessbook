using System;
using System.Collections.Generic;

namespace GuessBook.EF.Entities
{
    public partial class MemberAnswer
    {
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int? QuestionId { get; set; }
        public DateTime? CodeDate { get; set; }
        public string OptionIds { get; set; }
        public decimal? AnswerDigit { get; set; }
    }
}
