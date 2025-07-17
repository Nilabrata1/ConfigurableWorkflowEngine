using WorkflowEngine.Models;

namespace WorkflowEngine.Services;

public interface IDefinitionService
{
    Task<WorkflowDefinition> CreateAsync(CreateDefinitionRequest req);
    Task<WorkflowDefinition> GetDefinitionAsync(string id);
}
