using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuessBook.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("profile/{type}")]
        public IActionResult GetQuestionsWithQType(int type)
        {
            ViewBag.QuestionType = type;
            return View();
        }
    }
}
