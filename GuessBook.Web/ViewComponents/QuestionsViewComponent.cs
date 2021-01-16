using GuessBook.Business.Managers;
using GuessBook.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using GuessBook.EF.Models;

namespace GuessBook.Web.ViewComponents
{
    [ViewComponent]
    public class QuestionsViewComponent : ViewComponent
    {
        private readonly IQuestionsService _questionsService;
        private readonly IOptionsService _optionsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuestionsViewComponent(IQuestionsService questionsService, IOptionsService optionsService, UserManager<ApplicationUser> userManager)
        {
            _questionsService = questionsService;
            _optionsService = optionsService;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {

            //TODO : This is for test. must be deleted.

            //var questionid = 1133;
            var questionid =2;
            var question = _questionsService.GetQuestionByIdAsync(questionid).Result.ValueResult;
            question.QType = 2;
            //********************************************//



            //var userId = _userManager.GetUserId(Request.HttpContext.User);
            //var question = _questionsService.GetQuestionAsync(userId).Result.ValueResult;
            question.PhotoUrl = "https://picsum.photos/300/200?random=" + new Random().Next(20);
            var options = _optionsService.GetByQuestionIdAsync(question.Id).Result.ValueResult;


            var optionsDtos = options.ToList();
            foreach (var opt in optionsDtos)
            {
                opt.Photo = "https://picsum.photos/300/200?random=" + new Random().Next(20);
            }

            var model = new QuestionViewModel
            {
                Questions = question,
                Options = optionsDtos
            };

            return View(model);
        }

    }
}
