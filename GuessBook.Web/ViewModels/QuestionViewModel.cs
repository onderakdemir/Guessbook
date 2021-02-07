using GuessBook.Business.Models;
using System.Collections.Generic;

namespace GuessBook.Web.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionsDto Questions { get; set; }
        public IEnumerable<OptionsDto> Options { get; set; }

        public IEnumerable<int> UserOptions { get; set; }
    }
}
