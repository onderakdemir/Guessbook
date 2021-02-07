using GuessBook.Business.Managers;
using GuessBook.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using GuessBook.EF.Models;
using GuessBook.Web.Models;
using Microsoft.Extensions.Caching.Memory;

namespace GuessBook.Web.ViewComponents
{
    [ViewComponent]
    public class QuestionsViewComponent : ViewComponent
    {
        private readonly IQuestionsService _questionsService;
        private readonly IOptionsService _optionsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemoryCache _cache;

        public QuestionsViewComponent(IQuestionsService questionsService, IOptionsService optionsService, UserManager<ApplicationUser> userManager, IMemoryCache cache)
        {
            _questionsService = questionsService;
            _optionsService = optionsService;
            _userManager = userManager;
            _cache = cache;
        }
        public IViewComponentResult Invoke()
        {
            try
            {
                int questionId = 0;
                var userId = _userManager.GetUserId(Request.HttpContext.User);

                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    if (_cache != null && _cache.TryGetValue(userId, out var qId))
                    {
                        questionId = Convert.ToInt32(qId);
                        _cache.Remove(userId);
                    }
                }


                //TODO : This is for test. must be deleted.
                //var questionid = 1133;
                //if (questionId == 0)
                //{
                //    questionId = 55;
                //}
                //var question = _questionsService.GetQuestionByIdAsync(questionId).Result.ValueResult;
                //if (question == null) throw new Exception("Something went wrong!");

                //question.QType = 2;
                //********************************************//



                var question = _questionsService.GetQuestionAsync(userId,questionId).Result.ValueResult;
                if (question == null) throw new Exception("Something went wrong!");
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
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong!");
                return View();
            }

        }

    }
}
