using MVCwithSQLITE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Extensions;

namespace MVCwithSQLITE.Controllers
{
    public class LeaderController : Controller
    {
        // ActionResult<回傳值型態>
        // IActionResult 回傳各種可能類型
        
        // 建立控制器
        private readonly ApplicationDbContext _context;
        public LeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaderController
        public async Task<ActionResult> IndexAsync(int? page = 1)
        {
            const int pageSize = 4;
            ViewBag.Leader = GetPagedProcess(page, pageSize);
            return View(await _context.Leader.Skip<Leader>(pageSize * ((page ?? 1) - 1)).Take(pageSize).ToListAsync());

        }
        private IPagedList<Leader> GetPagedProcess(int? page, int pageSize)
        {
            if (page.HasValue && page < 1)
                return null;
            var listUnpageed = GetStuffFromDatabase();
            IPagedList<Leader> pagelist = listUnpageed.ToPagedList(page ?? 1, pageSize);
            if (pagelist.PageNumber != 1 && page.HasValue && page > pagelist.PageCount)
                return null;
            return pagelist;
            //throw new NotImplementedException();
        }
        protected IQueryable<Leader> GetStuffFromDatabase()
        {
            return _context.Leader;
        }

        // GET: LeaderController/Details/5
        public ActionResult Details(int id)
        {
            var leader = _context.Leader.Find(id);
            if (leader == null)
            {
                return NotFound(); // 如果找不到，回傳 404
            }

            return View(leader); // 將 leader 作為模型傳遞到視圖
        }


        // GET: LeaderController/Create => in HomeController
        

        // GET: LeaderController/Edit/5 => 預計放入Page1(使用者資料頁)
        /*public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: LeaderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

        // GET: LeaderController/Delete/5 => in HomeController
        
    }
}
