using GuessBook.Business.Models;
using GuessBook.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessBook.Web.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionsDto Questions { get; set; }
        public IEnumerable<OptionsDto> Options { get; set; }

        public IEnumerable<int> UserOptions { get; set; }
    }
}
