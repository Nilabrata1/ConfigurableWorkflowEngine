using WorkflowEngine.Models;
using WorkflowEngine.Repositories;
using WorkflowEngine.Validators;
using WorkflowEngine.Errors;

namespace WorkflowEngine.Services;

public class DefinitionService : IDefinitionService
{
    private readonly IWorkflowRepository _repo;
    private readonly DefinitionValidator _validator;

    public DefinitionService(IWorkflowRepository repo, DefinitionValidator validator)
    {
        _repo = repo;
        _validator = validator;
    }

    public async Task<WorkflowDefinition> CreateAsync(CreateDefinitionRequest req)
    {
        var def = new WorkflowDefinition(req.Id, req.Name, req.States, req.Actions);
        _validator.Validate(def);
        await _repo.AddDefinitionAsync(def);
        return def;
    }

    public async Task<WorkflowDefinition> GetDefinitionAsync(string id)
    {
        var def = await _repo.GetDefinitionAsync(id)
                  ?? throw new ApiException(404, "Definition not found.");
        return def;
    }
}
