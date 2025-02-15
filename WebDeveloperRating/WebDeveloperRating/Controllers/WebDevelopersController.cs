using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebDeveloperRating.Data;
using WebDeveloperRating.Data.Models;
using WebDeveloperRating.ViewModels.WebDevelopers;
using WebDeveloperRating.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace WebDeveloperRating.Controllers
{
    public class WebDevelopersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebDevelopersService WebDevelopersService;

        public WebDevelopersController(ApplicationDbContext context, IWebDevelopersService WebDevelopersService)
        {
            _context = context;
            this.WebDevelopersService = WebDevelopersService;
        }

        // GET: WebDevelopers
        public async Task<IActionResult> Index(IndexWebDevelopersViewModel WebDevelopers)
        {
            WebDevelopers = await WebDevelopersService.GetWebDevelopersAsync(WebDevelopers);
            return View(WebDevelopers);
        }

        // GET: WebDevelopers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var WebDeveloper = await _context.WebDevelopers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (WebDeveloper == null)
            {
                return NotFound();
            }

            return View(WebDeveloper);
        }

        // GET: WebDevelopers/Create
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebDevelopers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWebDeveloperViewModel WebDeveloper)
        {
            if (ModelState.IsValid)
            {
                await WebDevelopersService.CreateWebDeveloperAsync(WebDeveloper);
                return RedirectToAction(nameof(Index));
            }
            return View(WebDeveloper);
        }

        // GET: WebDevelopers/Edit/5
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var WebDeveloper = await WebDevelopersService.GetWebDeveloperToEditAsync(id);
            if (WebDeveloper == null)
            {
                return NotFound();
            }
            return View(WebDeveloper);
        }

        // POST: WebDevelopers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditWebDeveloperViewModel editWebDeveloper)
        {

            if (ModelState.IsValid)
            {
                await WebDevelopersService.UpdateWebDeveloperAsync(editWebDeveloper);
                return RedirectToAction(nameof(Index));
            }
            return View(editWebDeveloper);
        }

        // GET: WebDevelopers/Delete/5
        [Authorize(Roles = GlobalConstants.AdminRole)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var WebDeveloper = await _context.WebDevelopers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (WebDeveloper == null)
            {
                return NotFound();
            }

            return View(WebDeveloper);
        }

        // POST: WebDevelopers/Delete/5
        [Authorize(Roles = GlobalConstants.AdminRole)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var WebDeveloper = await _context.WebDevelopers.FindAsync(id);
            if (WebDeveloper != null)
            {
                _context.WebDevelopers.Remove(WebDeveloper);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebDeveloperExists(string id)
        {
            return _context.WebDevelopers.Any(e => e.Id == id);
        }
    }
}
