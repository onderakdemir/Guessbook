using System;
using System.Linq;
using GuessBook.EF.Context;
using GuessBook.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GuessBook.Business.Managers;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessBook.Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionsService _questionsService;
        private readonly IOptionsService _optionsService;

        public QuestionsController(IQuestionsService questionsService, IOptionsService optionsService)
        {
            _questionsService = questionsService;
            _optionsService = optionsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveOptionsAsync([FromBody] MemberAnswersDto model)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Model state is not valid!");

                var question = (await _questionsService.GetQuestionByIdAsync(model.QuestionId)).ValueResult;
                if (question == null) throw new Exception("Something went wrong!");

                var optionIds = JsonConvert.DeserializeObject<List<string>>(model.OptionIds);
                var count = optionIds.Count;

                if (question != null)
                    switch (question.QType)
                    {
                        case 1:
                        case 3:
                            if (count < question.MinSelect || count > question.MaxSelect)
                            {
                                return BadRequest(
                                    $"Min {question.MinSelect} - Max {question.MaxSelect} items must be selected!");
                            }

                            break;

                        case 2:
                        case 4:
                            if (count < question.QTypeMin || count > question.QTypeMax)
                            {
                                return BadRequest(
                                    $"Min {question.QTypeMin} - Max {question.QTypeMax} items must be selected!");
                            }

                            break;
                        case 5:
                            var value = Convert.ToInt32(optionIds.FirstOrDefault());
                            model.AnswerDigit = Convert.ToDecimal(value);

                            if (question.IncrementSelect != null &&
                                ((value < question.MinSelect) || (value > question.MaxSelect) ||
                                 (value % question.IncrementSelect) != 0))
                            {
                                return BadRequest(
                                    $"Your choice must be between {question.MinSelect} - {question.MaxSelect} and multiple of {question.IncrementSelect}!");
                            }

                            break;
                    }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                model.MemberId = userId;

                var result = _optionsService.SaveUserAnswers(model).Result;
                if (!result.Succeeded) throw new Exception(result.ErrorMessage);

                return Ok(model);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
