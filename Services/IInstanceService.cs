using WorkflowEngine.Models;

namespace WorkflowEngine.Services;

public interface IInstanceService
{
    Task<WorkflowInstance> StartInstanceAsync(string definitionId);
    Task<WorkflowInstance> GetInstanceAsync(string instanceId);
    Task<WorkflowInstance> ExecuteActionAsync(string instanceId, string actionId);
}
