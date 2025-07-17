using WorkflowEngine.Models;
using WorkflowEngine.Repositories;
using WorkflowEngine.Validators;
using WorkflowEngine.Errors;

namespace WorkflowEngine.Services;

public class InstanceService : IInstanceService
{
    private readonly IWorkflowRepository _repo;
    private readonly ExecutionValidator _validator;

    public InstanceService(IWorkflowRepository repo, ExecutionValidator validator)
    {
        _repo = repo;
        _validator = validator;
    }

    public async Task<WorkflowInstance> StartInstanceAsync(string definitionId)
    {
        var def = await _repo.GetDefinitionAsync(definitionId)
                  ?? throw new ApiException(404, "Definition not found.");
        var initial = def.States.Single(s => s.IsInitial);
        var inst = new WorkflowInstance(definitionId, initial.Id);
        await _repo.AddInstanceAsync(inst);
        return inst;
    }

    public async Task<WorkflowInstance> GetInstanceAsync(string instanceId)
    {
        return await _repo.GetInstanceAsync(instanceId)
               ?? throw new ApiException(404, "Instance not found.");
    }

    public async Task<WorkflowInstance> ExecuteActionAsync(string instanceId, string actionId)
    {
        var inst = await GetInstanceAsync(instanceId);
        var def = await _repo.GetDefinitionAsync(inst.DefinitionId)
                  ?? throw new ApiException(404, "Definition not found.");
        _validator.Validate(def, inst, actionId);

        var action = def.Actions.Single(a => a.Id == actionId);
        inst.ApplyAction(actionId, action.ToState);
        await _repo.UpdateInstanceAsync(inst);
        return inst;
    }
}
