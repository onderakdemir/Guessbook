using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessBook.Business.Managers;
using GuessBook.EF.Entities;
using GuessBook.EF.Models;
using GuessBook.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GuessBook.Web.ViewComponents
{
    [ViewComponent]
    public class ProfilePageViewComponent : ViewComponent
    {
        private readonly IQuestionsService _questionsService;
        private readonly IOptionsService _optionsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfilePageViewComponent(IQuestionsService questionsService, IOptionsService optionsService, UserManager<ApplicationUser> userManager)
        {
            _questionsService = questionsService;
            _optionsService = optionsService;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke(int type)
        {
            var userId = _userManager.GetUserId(Request.HttpContext.User);
            var questions = _questionsService.GetAllQuestionByUserIdAsync(userId,type).Result.ValueResult;

            //TODO: This shoud be removed
            questions.ForEach(c =>
            {
                c.PhotoUrl = "https://picsum.photos/300/200?random=" + new Random().Next(20);
            });

            var model = new List<QuestionViewModel>();

            foreach (var question in questions)
            {
                var options = _optionsService.GetByQuestionIdAsync(question.Id).Result.ValueResult.ToList();
                var userOptions = _optionsService.GetUserAnswersAsync(userId, question.Id).Result.ValueResult;

                var optionIds = new List<int>();
                if (userOptions!=null && !string.IsNullOrEmpty(userOptions.OptionIds))
                {
                    optionIds=JsonConvert.DeserializeObject<IEnumerable<int>>(userOptions.OptionIds).ToList();
                }

                Console.WriteLine(userOptions);
                foreach (var opt in options)
                {
                    opt.Photo = "https://picsum.photos/300/200?random=" + new Random().Next(20);
                }
                model.Add(new QuestionViewModel
                {
                    Questions = question,
                    Options = options,
                    UserOptions = optionIds,
                });

            }
            return View(model);
        }
    }
}
