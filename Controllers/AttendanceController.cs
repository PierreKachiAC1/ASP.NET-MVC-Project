using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using FinalProject.Repositories;
using Microsoft.AspNetCore.Identity;
namespace FinalProject.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository, ISessionRepository sessionRepository, IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _attendanceRepository = attendanceRepository;
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

        }

        public async Task<IActionResult> Index()
        {
            var attendances = await _attendanceRepository.GetAllAsync();
            return View(attendances);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? sessionId)
        {
            var model = new AttendanceViewModel();
            if (sessionId.HasValue)
            {
                model.SessionId = sessionId.Value;
                
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingAttendance = await _attendanceRepository.FindBySessionAndUserId(model.SessionId, model.UserId);
                if (existingAttendance != null)
                {
                    ModelState.AddModelError("", "Attendance record already exists for this user and session.");
                    return View(model);
                }

                var attendance = new Attendance
                {
                    SessionId = model.SessionId,
                    UserId = model.UserId,
                    Present = model.Present,
                    AttendanceDateTime = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    Remarks = model.Remarks
                };
                await _attendanceRepository.AddAsync(attendance);
                return RedirectToAction(nameof(Index));
            }
            
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            var model = new AttendanceViewModel
            {
                SessionId = attendance.SessionId,
                UserId = attendance.UserId,
                Present = attendance.Present,
                Remarks = attendance.Remarks,
                AttendanceDateTime = attendance.AttendanceDateTime
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AttendanceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var attendanceToUpdate = await _attendanceRepository.GetByIdAsync(id);
            if (attendanceToUpdate == null)
            {
                return NotFound();
            }
            attendanceToUpdate.SessionId = model.SessionId;
            attendanceToUpdate.UserId = model.UserId;
            attendanceToUpdate.Present = model.Present;
            attendanceToUpdate.AttendanceDateTime = model.AttendanceDateTime; 
            attendanceToUpdate.Remarks = model.Remarks; 

            await _attendanceRepository.UpdateAsync(attendanceToUpdate);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var attendance = await _attendanceRepository.GetByIdAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            await _attendanceRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Role="User",
                    Email = model.Email,
                    Password=model.Password,
                    IsActive = true,
                    FullName = model.FullName
                    
                };
                user.Password = _passwordHasher.HashPassword(user, model.Password);


                try
                {
                    await _userRepository.AddUserAsync(user);  
                    TempData["SuccessMessage"] = "User registered successfully!";
                    return RedirectToAction("Index"); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }
    }
}
