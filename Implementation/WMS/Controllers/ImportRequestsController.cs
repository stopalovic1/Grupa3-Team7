﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WMS.Models;

namespace WMS.Controllers
{
    [Authorize]
    public class ImportRequestsController : Controller
    {
        private readonly WMSContext _context;

        public ImportRequestsController(WMSContext context)
        {
            _context = context;
        }

        // GET: ImportRequests
        public async Task<IActionResult> Index()
        {
            return View(await _context.ImportRequests.ToListAsync());
        }

        // GET: ImportRequests/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importRequest = await _context.ImportRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importRequest == null)
            {
                return NotFound();
            }

            return View(importRequest);
        }

        // GET: ImportRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImportRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestDate,Processed")] ImportRequest importRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(importRequest);
        }

        // GET: ImportRequests/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importRequest = await _context.ImportRequests.FindAsync(id);
            if (importRequest == null)
            {
                return NotFound();
            }
            return View(importRequest);
        }

        // POST: ImportRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,RequestDate,Processed")] ImportRequest importRequest)
        {
            if (id != importRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportRequestExists(importRequest.Id))
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
            return View(importRequest);
        }

        // GET: ImportRequests/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importRequest = await _context.ImportRequests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (importRequest == null)
            {
                return NotFound();
            }

            return View(importRequest);
        }

        // POST: ImportRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var importRequest = await _context.ImportRequests.FindAsync(id);
            _context.ImportRequests.Remove(importRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportRequestExists(string id)
        {
            return _context.ImportRequests.Any(e => e.Id == id);
        }
    }
}