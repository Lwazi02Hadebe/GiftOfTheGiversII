
using GiftOfTheGiversII.Models;

namespace GiftOfTheGiversII.Services
{
    public interface IVolunteerService
    {
        // Volunteer Management
        Task<Volunteer> RegisterVolunteerAsync(int userId, string phone, string address, string city, string zipCode, string? skills, string? availability, bool hasTransportation, string? emergencyContact);
        Task<Volunteer?> GetVolunteerByUserIdAsync(int userId);
        Task<List<Volunteer>> GetAllVolunteersAsync();
        Task UpdateVolunteerStatusAsync(int volunteerId, string status);

        // Task Management
        Task<VolunteerTask> CreateTaskAsync(VolunteerTask task);
        Task<List<VolunteerTask>> GetAvailableTasksAsync();
        Task<List<VolunteerTask>> GetProjectTasksAsync(int projectId);
        Task<List<VolunteerTask>> GetAllTasksAsync();
        Task<VolunteerTask?> GetTaskByIdAsync(int taskId);
        Task UpdateTaskStatusAsync(int taskId, string status);

        // Assignment Management
        Task<VolunteerAssignment> AssignVolunteerToTaskAsync(int volunteerId, int taskId);
        Task<List<VolunteerAssignment>> GetVolunteerAssignmentsAsync(int volunteerId);
        Task<List<VolunteerAssignment>> GetTaskAssignmentsAsync(int taskId);
        Task UpdateAssignmentStatusAsync(int assignmentId, string status, decimal? hoursCompleted, string? feedback, int? rating);
        Task<bool> IsVolunteerAssignedToTaskAsync(int volunteerId, int taskId);

        // Dashboard Stats
        Task<int> GetTotalVolunteersAsync();
        Task<int> GetActiveVolunteersCountAsync();
        Task<int> GetCompletedTasksCountAsync();
        Task<decimal> GetTotalVolunteerHoursAsync();
    }
}
