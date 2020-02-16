using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HwidProtectionServer.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HwidProtectionServer.Models;
using HwidProtectionServer.ViewModels;
using Microsoft.AspNetCore.Http;

namespace HwidProtectionServer.Controllers
{
    public class HwidController : Controller
    {
        private readonly HwidProtectionServerContext _context;

        public HwidController(HwidProtectionServerContext context)
        {
            _context = context;
        }

        // GET: Hwid
        public IActionResult Index()
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            return View(new HwidsViewModel
            {
                User = user,
                Hwids = _context.HwidInfo.ToList()
            });
        }

        // GET: Hwid/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            if (id == null)
            {
                return NotFound();
            }

            var hwidInfo = await _context.HwidInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hwidInfo == null)
            {
                return NotFound();
            }

            return View(new HwidViewModel
            {
                User = user,
                Hwid = hwidInfo
            });
        }

        // GET: Hwid/Create
        public IActionResult Create()
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            return View(new HwidViewModel
            {
                User = user
            });
        }

        // POST: Hwid/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HwidViewModel hwidViewModel)
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            if (ModelState.IsValid)
            {
                _context.Add(hwidViewModel.Hwid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(new HwidViewModel
            {
                User = user,
                Hwid = hwidViewModel.Hwid
            });
        }

        // GET: Hwid/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            if (id == null)
            {
                return NotFound();
            }

            var hwidInfo = await _context.HwidInfo.FindAsync(id);
            if (hwidInfo == null)
            {
                return NotFound();
            }
            return View(new HwidViewModel
            {
                User = user,
                Hwid = hwidInfo
            });
        }

        // POST: Hwid/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HwidViewModel hwidViewModel)
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            if (id != hwidViewModel.Hwid.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hwidViewModel.Hwid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HwidInfoExists(hwidViewModel.Hwid.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new HwidViewModel
            {
                User = user,
                Hwid = hwidViewModel.Hwid
            });
        }

        // GET: Hwid/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            if (id == null)
            {
                return NotFound();
            }

            var hwidInfo = await _context.HwidInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hwidInfo == null)
            {
                return NotFound();
            }

            return View(new HwidViewModel
            {
                User = user,
                Hwid = hwidInfo
            });
        }

        // POST: Hwid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            User user = HttpContext.Session.GetObject<User>("user");
            if (user == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            var hwidInfo = await _context.HwidInfo.FindAsync(id);
            _context.HwidInfo.Remove(hwidInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Verify(string hwid)
        {
            var hwidInfo = await _context.HwidInfo.FirstOrDefaultAsync(hwidInfo => hwidInfo.Value.ToLower() == hwid.ToLower());
            if (hwidInfo == null)
            {
                return Unauthorized();
            }
            return Ok();
        }

        private bool HwidInfoExists(int id)
        {
            return _context.HwidInfo.Any(e => e.Id == id);
        }
    }
}
