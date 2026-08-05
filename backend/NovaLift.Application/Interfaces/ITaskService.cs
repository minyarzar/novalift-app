using NovaLift.Application.DTOs;

namespace NovaLift.Application.Interfaces;

public interface ITaskService
{
    // User: Get current user's tasks
    Task<IEnumerable<TaskDto>> GetMyTasksAsync(int userId);


    // User: Generate daily tasks based on VIP level
    Task<bool> GenerateDailyTasksAsync(int userId);


    // User: Complete task
    Task<bool> CompleteTaskAsync(
        int userId,
        int taskId,
        string? proofUrl,
        string? proofDataJson
    );


    // Admin: Task Template Management

    Task<IEnumerable<TaskTemplateDto>> GetTaskTemplatesAsync();


    Task<TaskTemplateDto?> GetTaskTemplateByIdAsync(int id);


    Task<TaskTemplateDto> CreateTaskTemplateAsync(
        CreateTaskTemplateRequest request
    );


    Task<bool> UpdateTaskTemplateAsync(
        int id,
        UpdateTaskTemplateRequest request
    );


    Task<bool> DeleteTaskTemplateAsync(int id);


    // Admin: Generate tasks for all users
    Task<int> GenerateTasksForAllUsersAsync();
}