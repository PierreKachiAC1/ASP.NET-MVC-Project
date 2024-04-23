using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{

    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionRepository.GetAllAsync();

            var sessionViewModels = sessions.Select(session => new SessionViewModel
            {
                Id = session.Id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                Location = session.Location,
                Purpose = session.Purpose
            }).ToList();

            return View(sessionViewModels);
        }
        public async Task<IActionResult> History()
        {
            var sessions = await _sessionRepository.GetAllAsync();

            var sessionViewModels = sessions.Select(session => new SessionViewModel
            {
                Id = session.Id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                Location = session.Location,
                Purpose = session.Purpose,
                Active = session.Active
            }).ToList();

            return View(sessionViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionViewModel model)
        {
            if (ModelState.IsValid)
            {   
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError("", "User must be logged in to create a session.");
                    return View(model);
                }
                var temp = model.StartDate.AddHours(model.DurationHours);
                var session = new Session()
                {   
                    StartDate = model.StartDate,
                    DurationHours = model.DurationHours,
                    EndDate = temp,
                    Location = model.Location,
                    Purpose = model.Purpose,
                    CreatedByUserId = int.Parse(userId)
                };

                

                await _sessionRepository.AddAsync(session);
                TempData["SuccessMessage"] = "Session created successfully.";
                return View(new SessionViewModel()); 
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _sessionRepository.UpdateAsync(session);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _sessionRepository.GetByIdAsync(id)==null)
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
            return View(session);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]  // Ensure this is included for security against CSRF
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _sessionRepository.GetByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            // Set the session as inactive instead of deleting
            session.Active = false;
            try
            {
                await _sessionRepository.UpdateAsync(session);  // Assuming UpdateAsync handles setting the entity state as modified and saving changes
                TempData["SuccessMessage"] = "Session deactivated successfully.";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception details here, for debugging purposes or inform the user
                ModelState.AddModelError("", "Unable to deactivate the session. Please try again.");
                return View("Edit", session);  // Redirecting back to the edit page if there's a concurrency issue
            }

            return RedirectToAction(nameof(History));  // Redirect to the history view where presumably inactive sessions can be reviewed
        }
    }
}
