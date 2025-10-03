
using GiftOfTheGiversII.Models;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGiversII.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly ApplicationDbContext _context;

        public VolunteerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Volunteer> RegisterVolunteerAsync(int userId, string phone, string address, string city, string zipCode, string? skills, string? availability, bool hasTransportation, string? emergencyContact)
        {
            var volunteer = new Volunteer
            {
                UserID = userId,
                Phone = phone,
                Address = address,
                City = city,
                ZipCode = zipCode,
                Skills = skills,
                Availability = availability,
                HasTransportation = hasTransportation,
                EmergencyContact = emergencyContact,
                Status = "Pending",
                RegistrationDate = DateTime.UtcNow
            };

            _context.Volunteers.Add(volunteer);
            await _context.SaveChangesAsync();
            return volunteer;
        }

        public async Task<Volunteer?> GetVolunteerByUserIdAsync(int userId)
        {
            return await _context.Volunteers
                .Include(v => v.User)
                .Include(v => v.VolunteerTasks)
                .FirstOrDefaultAsync(v => v.UserID == userId);
        }

        public async Task<List<Volunteer>> GetAllVolunteersAsync()
        {
            return await _context.Volunteers
                .Include(v => v.User)
                .Include(v => v.VolunteerTasks)
                .OrderByDescending(v => v.RegistrationDate)
                .ToListAsync();
        }

        public async Task UpdateVolunteerStatusAsync(int volunteerId, string status)
        {
            var volunteer = await _context.Volunteers.FindAsync(volunteerId);
            if (volunteer != null)
            {
                volunteer.Status = status;
                if (status == "Approved")
                {
                    volunteer.ApprovalDate = DateTime.UtcNow;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<VolunteerTask> CreateTaskAsync(VolunteerTask task)
        {
            task.CreatedDate = DateTime.UtcNow;
            _context.VolunteerTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<List<VolunteerTask>> GetAvailableTasksAsync()
        {
            return await _context.VolunteerTasks
                .Include(t => t.Project)
                .Where(t => t.Status == "Open" && t.StartDate > DateTime.UtcNow && t.VolunteersAssigned < t.VolunteersNeeded)
                .OrderBy(t => t.StartDate)
                .ToListAsync();
        }

        public async Task<List<VolunteerTask>> GetProjectTasksAsync(int projectId)
        {
            return await _context.VolunteerTasks
                .Include(t => t.Project)
                .Include(t => t.VolunteerAssignments)
                .Where(t => t.ProjectID == projectId)
                .OrderByDescending(t => t.StartDate)
                .ToListAsync();
        }

        public async Task<List<VolunteerTask>> GetAllTasksAsync()
        {
            return await _context.VolunteerTasks
                .Include(t => t.Project)
                .Include(t => t.VolunteerAssignments)
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();
        }

        public async Task<VolunteerTask?> GetTaskByIdAsync(int taskId)
        {
            return await _context.VolunteerTasks
                .Include(t => t.Project)
                .Include(t => t.VolunteerAssignments)
                .ThenInclude(va => va.Volunteer)
                .ThenInclude(v => v.User)
                .FirstOrDefaultAsync(t => t.TaskID == taskId);
        }

        public async Task UpdateTaskStatusAsync(int taskId, string status)
        {
            var task = await _context.VolunteerTasks.FindAsync(taskId);
            if (task != null)
            {
                task.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<VolunteerAssignment> AssignVolunteerToTaskAsync(int volunteerId, int taskId)
        {
            var assignment = new VolunteerAssignment
            {
                VolunteerID = volunteerId,
                TaskID = taskId,
                AssignmentDate = DateTime.UtcNow,
                Status = "Assigned"
            };

            _context.VolunteerAssignments.Add(assignment);

            // Update task assignment count
            var task = await _context.VolunteerTasks.FindAsync(taskId);
            if (task != null)
            {
                task.VolunteersAssigned++;
                if (task.VolunteersAssigned >= task.VolunteersNeeded)
                {
                    task.Status = "InProgress";
                }
            }

            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<List<VolunteerAssignment>> GetVolunteerAssignmentsAsync(int volunteerId)
        {
            return await _context.VolunteerAssignments
                .Include(va => va.Task)
                .ThenInclude(t => t.Project)
                .Where(va => va.VolunteerID == volunteerId)
                .OrderByDescending(va => va.AssignmentDate)
                .ToListAsync();
        }

        public async Task<List<VolunteerAssignment>> GetTaskAssignmentsAsync(int taskId)
        {
            return await _context.VolunteerAssignments
                .Include(va => va.Volunteer)
                .ThenInclude(v => v.User)
                .Where(va => va.TaskID == taskId)
                .ToListAsync();
        }

        public async Task UpdateAssignmentStatusAsync(int assignmentId, string status, decimal? hoursCompleted, string? feedback, int? rating)
        {
            var assignment = await _context.VolunteerAssignments.FindAsync(assignmentId);
            if (assignment != null)
            {
                assignment.Status = status;
                assignment.HoursCompleted = hoursCompleted;
                assignment.Feedback = feedback;
                assignment.Rating = rating;

                if (status == "Completed")
                {
                    assignment.CompletedDate = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsVolunteerAssignedToTaskAsync(int volunteerId, int taskId)
        {
            return await _context.VolunteerAssignments
                .AnyAsync(va => va.VolunteerID == volunteerId && va.TaskID == taskId && va.Status != "Cancelled");
        }

        public async Task<int> GetTotalVolunteersAsync()
        {
            return await _context.Volunteers.CountAsync();
        }

        public async Task<int> GetActiveVolunteersCountAsync()
        {
            return await _context.Volunteers.CountAsync(v => v.Status == "Approved");
        }

        public async Task<int> GetCompletedTasksCountAsync()
        {
            return await _context.VolunteerAssignments.CountAsync(va => va.Status == "Completed");
        }

        public async Task<decimal> GetTotalVolunteerHoursAsync()
        {
            return await _context.VolunteerAssignments
                .Where(va => va.Status == "Completed" && va.HoursCompleted.HasValue)
                .SumAsync(va => va.HoursCompleted.Value);
        }
    }
}
