using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers
{
    public class BankController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Content("<h1>Welcome to the Best Bank</h1>", "text/html");
        }

        [Route("account-details")]
        public IActionResult AccountDetails()
        {
            if (Request.Method != "GET") return BadRequest("Only GET Requests are allowed");
            var account = new { accountNumber = 1001, accountHolderName = "Example Name", currentBalance = 5000 };
            return Json(account);
        }

        [Route("account-statement")]
        public IActionResult AccountStatement()
        {
            if (Request.Method != "GET") return BadRequest("Only GET Requests are allowed");
            var pdfData = "[some dummy PDF file]";
            byte[] bytes = new byte[pdfData.Length];
            for(int i = 0; i < pdfData.Length; i++)
            {
                bytes[i] = (byte) pdfData[i];
            }
            return File(bytes, "text/plain");
        }

        [Route("get-current-balance/{accountNumber:int?}")]
        public IActionResult CurrentBalance()
        {
            if (Request.Method != "GET") return BadRequest("Only GET Requests are allowed");
            if (Request.RouteValues.ContainsKey("accountNumber"))
            {
                int accountNumber = Convert.ToInt32(Request.RouteValues["accountNumber"]);
                if(accountNumber == 1001)
                {
                    return Content("<p>Current Balance: 5000</p>", "text/html");
                }
                else
                {
                    return BadRequest("Account Number should be 1001");
                }
            }
            else
            {
                return NotFound("Account Number should be supplied");
            }
        }
    }
}
