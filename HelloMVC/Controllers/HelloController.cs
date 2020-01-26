using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloMVC.Controllers
{
    public class HelloController : Controller
    {
        public static int timesGreeted;

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            string html = "<form method='post'>" +
                    "<input type='text' name='name' />" +
                    "<select name='language' />" +
                        "<option value='English'>English</option>" +
                        "<option value='Spanish'>Spanish</option>" +
                        "<option value='French'>French</option>" +
                        "<option value='Korean'>Korean</option>" +
                        "<option value='Japanese'>Japanese</option>" +
                    "</select>" +
                    "<input type='submit' value='Greet me!' />" +
                "</form>";

            return Content(html, "text/html");
        }

        public static string CreateMessage(string language, string name = "World")
        {
            Dictionary<string, string> greetings = new Dictionary<string, string>();
            greetings.Add("English", "Hello");
            greetings.Add("Spanish", "Hola");
            greetings.Add("French", "Bonjour");
            greetings.Add("Korean", "Annyeong");
            greetings.Add("Japanese", "Konbanwa");

            if (name == null)
            {
                name = "World";
            }
            return greetings[language] + " " + name + "!";
        }

        [Route("/Hello")]
        [HttpPost]
        public IActionResult Display(string language, string name)
        {
            if (Request.Cookies["VisitCount"] == null)
            {
                timesGreeted = 1;
                Response.Cookies.Append("VisitCount", "1");
            }
            else
            {
                timesGreeted += 1;
                Response.Cookies.Append("VisitCount", (timesGreeted).ToString());
            }

            string html = "<h1>" + CreateMessage(language, name) + "</h1>" + 
                "<p>Greeted " + timesGreeted + " times.</p>";

            return Content(html, "text/html");
        }

        // Handle requests to /Hello/NAME (URL segment - can use as parameters)
        [Route("/Hello/{name}")]
        public IActionResult Index2(string name)
        {
            return Content(string.Format("<h1>Hello {0}</h1>", name), "text/html");
        }

        public IActionResult Goodbye()
        {
            return Content("Goodbye");
        }
    }
}
