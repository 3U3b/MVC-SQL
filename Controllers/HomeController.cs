using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCwithSQLITE.Models;

namespace MVCwithSQLITE.Controllers
{
    public class HomeController : Controller
    {
        // 建立控制器
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context,ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Page1()
        {
            var status = HttpContext.Session.GetString("State");
            var username = HttpContext.Session.GetString("user");
            if (status == null)
            {
                return View();
            }
            else
            {
                ViewData["message"] = status;
                ViewData["username"] = username;
                return View();
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // login logout
        [HttpGet]
        public IActionResult Login()
        {
            Console.WriteLine("Login method called");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Leader model)
        {
            // ModelState => 驗證提交的表單資料的有效性

            if (ModelState.IsValid)
            {
                // 用LeaderId和Name尋找第一個符合資料
                var user = await _context.Leader
                    .FirstOrDefaultAsync(p => p.LeaderId == model.LeaderId && p.Name == model.Name);
                if (user != null)
                {
                    HttpContext.Session.SetString("State", "Logged in !!");
                    HttpContext.Session.SetString("user", model.Name);
                    ViewData["message"] = "Logged in!!";
                    // 重導向到頁面 (效果類似 View:<a asp-action="Page1"></a>)
                    HttpContext.Response.Redirect("/Home/Page1");
                    //return View(nameof(Page1)); // 只有畫面被渲染成Page1，網址還是在Login
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "帳號密碼錯誤");
                }
            }
            return View();
        }

        /*[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/

       // about leader

        // [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // POST: LeaderController/Create
        [HttpPost]
        //[VaLeaderIdateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("LeaderId,Name,Email,Phone")] Leader user)
        {
            // ModelState => 驗證提交的表單資料的有效性
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Leader.Add(user);
                    await _context.SaveChangesAsync();
                    await Login(user);
                    HttpContext.Response.Redirect("/Home/Page1");
                    //return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("constraint"))
                    {
                        var _ = await _context.Leader
                    .FirstOrDefaultAsync(p => p.LeaderId == user.LeaderId && p.Name == user.Name);
                        // 處理約束違反異常

                        if (_ != null)
                        {
                            ViewData["ShowConfirm"] = true;  // 這個可以控制是否顯示 confirm
                            ViewData["UserInfo"] = _;
                            
                            //HttpContext.Response.Redirect("/Home/Login");
                            return View();
                        }
                        else
                        {
                            ViewBag.AlarmMessage = "無法新增資料，可能已存在相同的記錄。";
                            return View(user);
                        }
                    }
                    else
                    {
                        // 其他類型的異常
                        throw;
                    }
                }
            }
            return View(user);
        }
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
