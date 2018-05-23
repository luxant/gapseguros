//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using DataAccess.Database;

//namespace SuperZapatosGAP.Controllers
//{
//    public class ArticlesController : Controller
//    {
//        private readonly GAPSuperZapatosContext _context;

//        public ArticlesController(GAPSuperZapatosContext context)
//        {
//            _context = context;
//        }

//        // GET: Articles
//        public async Task<IActionResult> Index()
//        {
//            var gAPSuperZapatosContext = _context.Articles.Include(a => a.Store);
//            return View(await gAPSuperZapatosContext.ToListAsync());
//        }

//        // GET: Articles/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var articles = await _context.Articles
//                .Include(a => a.Store)
//                .SingleOrDefaultAsync(m => m.Id == id);
//            if (articles == null)
//            {
//                return NotFound();
//            }

//            return View(articles);
//        }

//        // GET: Articles/Create
//        public IActionResult Create()
//        {
//            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Address");
//            return View();
//        }

//        // POST: Articles/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,TotalInShelf,TotalInVault,StoreId")] Articles articles)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(articles);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Address", articles.StoreId);
//            return View(articles);
//        }

//        // GET: Articles/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var articles = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);
//            if (articles == null)
//            {
//                return NotFound();
//            }
//            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Address", articles.StoreId);
//            return View(articles);
//        }

//        // POST: Articles/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,TotalInShelf,TotalInVault,StoreId")] Articles articles)
//        {
//            if (id != articles.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(articles);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ArticlesExists(articles.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Address", articles.StoreId);
//            return View(articles);
//        }

//        // GET: Articles/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var articles = await _context.Articles
//                .Include(a => a.Store)
//                .SingleOrDefaultAsync(m => m.Id == id);
//            if (articles == null)
//            {
//                return NotFound();
//            }

//            return View(articles);
//        }

//        // POST: Articles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var articles = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);
//            _context.Articles.Remove(articles);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool ArticlesExists(int id)
//        {
//            return _context.Articles.Any(e => e.Id == id);
//        }
//    }
//}
