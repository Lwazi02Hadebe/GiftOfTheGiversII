
using GiftOfTheGiversII.Models;
using GiftOfTheGiversII.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GiftOfTheGiversII.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService _volunteerService;
        private readonly ApplicationDbContext _context;

        public VolunteerController(IVolunteerService volunteerService, ApplicationDbContext context)
        {
            _volunteerService = volunteerService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var existingVolunteer = await _volunteerService.GetVolunteerByUserIdAsync(userId);

            if (existingVolunteer != null)
            {
                return RedirectToAction("Dashboard");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string phone, string address, string city, string zipCode, string? skills, string? availability, bool hasTransportation = false, string? emergencyContact = null)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var volunteer = await _volunteerService.RegisterVolunteerAsync(
                userId, phone, address, city, zipCode, skills, availability, hasTransportation, emergencyContact);

            TempData["Success"] = "Thank you for registering as a volunteer! Your application is under review.";
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var volunteer = await _volunteerService.GetVolunteerByUserIdAsync(userId);

            if (volunteer == null)
            {
                return RedirectToAction("Register");
            }

            ViewBag.AvailableTasks = await _volunteerService.GetAvailableTasksAsync();
            ViewBag.MyAssignments = await _volunteerService.GetVolunteerAssignmentsAsync(volunteer.VolunteerID);

            return View(volunteer);
        }

        [HttpGet]
        public async Task<IActionResult> AvailableTasks()
        {
            var tasks = await _volunteerService.GetAvailableTasksAsync();
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> SignUpForTask(int taskId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var volunteer = await _volunteerService.GetVolunteerByUserIdAsync(userId);

            if (volunteer == null || volunteer.Status != "Approved")
            {
                TempData["Error"] = "You must be an approved volunteer to sign up for tasks.";
                return RedirectToAction("AvailableTasks");
            }

            // Check if already assigned
            if (await _volunteerService.IsVolunteerAssignedToTaskAsync(volunteer.VolunteerID, taskId))
            {
                TempData["Error"] = "You are already assigned to this task.";
                return RedirectToAction("AvailableTasks");
            }

            await _volunteerService.AssignVolunteerToTaskAsync(volunteer.VolunteerID, taskId);
            TempData["Success"] = "Successfully signed up for the task!";
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> MyAssignments()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var volunteer = await _volunteerService.GetVolunteerByUserIdAsync(userId);

            if (volunteer == null)
            {
                return RedirectToAction("Register");
            }

            var assignments = await _volunteerService.GetVolunteerAssignmentsAsync(volunteer.VolunteerID);
            return View(assignments);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAssignmentStatus(int assignmentId, string status, decimal? hoursCompleted, string? feedback, int? rating)
        {
            await _volunteerService.UpdateAssignmentStatusAsync(assignmentId, status, hoursCompleted, feedback, rating);
            TempData["Success"] = "Assignment status updated successfully!";
            return RedirectToAction("MyAssignments");
        }
    }
}