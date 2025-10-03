
using GiftOfTheGiversII.Models;
using GiftOfTheGiversII.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftOfTheGiversII.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminVolunteerController : Controller
    {
        private readonly IVolunteerService _volunteerService;

        public AdminVolunteerController(IVolunteerService volunteerService)
        {
            _volunteerService = volunteerService;
        }

        [HttpGet]
        public async Task<IActionResult> Volunteers()
        {
            var volunteers = await _volunteerService.GetAllVolunteersAsync();
            return View(volunteers);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVolunteerStatus(int volunteerId, string status)
        {
            await _volunteerService.UpdateVolunteerStatusAsync(volunteerId, status);
            TempData["Success"] = "Volunteer status updated successfully!";
            return RedirectToAction("Volunteers");
        }

        [HttpGet]
        public async Task<IActionResult> Tasks()
        {
            var tasks = await _volunteerService.GetAllTasksAsync();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(VolunteerTask task)
        {
            if (ModelState.IsValid)
            {
                await _volunteerService.CreateTaskAsync(task);
                TempData["Success"] = "Task created successfully!";
                return RedirectToAction("Tasks");
            }
            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> TaskDetails(int id)
        {
            var task = await _volunteerService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Assignments = await _volunteerService.GetTaskAssignmentsAsync(id);
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTaskStatus(int taskId, string status)
        {
            await _volunteerService.UpdateTaskStatusAsync(taskId, status);
            TempData["Success"] = "Task status updated successfully!";
            return RedirectToAction("Tasks");
        }

        [HttpGet]
        public async Task<IActionResult> VolunteerStats()
        {
            ViewBag.TotalVolunteers = await _volunteerService.GetTotalVolunteersAsync();
            ViewBag.ActiveVolunteers = await _volunteerService.GetActiveVolunteersCountAsync();
            ViewBag.CompletedTasks = await _volunteerService.GetCompletedTasksCountAsync();
            ViewBag.TotalHours = await _volunteerService.GetTotalVolunteerHoursAsync();

            return View();
        }
    }
}
