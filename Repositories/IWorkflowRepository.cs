using WorkflowEngine.Models;

namespace WorkflowEngine.Repositories;

public interface IWorkflowRepository
{
    Task AddDefinitionAsync(WorkflowDefinition def);
    Task<WorkflowDefinition?> GetDefinitionAsync(string id);
    Task AddInstanceAsync(WorkflowInstance inst);
    Task<WorkflowInstance?> GetInstanceAsync(string id);
    Task UpdateInstanceAsync(WorkflowInstance inst);
}
