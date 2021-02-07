using System;
using System.ComponentModel.DataAnnotations;

namespace GuessBook.Business.Models
{
    public class MemberAnswersDto
    {
        public string MemberId { get; set; }

        [Required]
        public int QuestionId { get; set; }
        public DateTime? CodeDate { get; set; }

        [Required]
        public string OptionIds { get; set; }
        public decimal? AnswerDigit { get; set; }
    }
}
