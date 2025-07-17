using WorkflowEngine.Models;
using WorkflowEngine.Errors;
using System.Collections.Concurrent;

namespace WorkflowEngine.Repositories;

public class InMemoryWorkflowRepository : IWorkflowRepository
{
    private readonly ConcurrentDictionary<string, WorkflowDefinition> _defs = new();
    private readonly ConcurrentDictionary<string, WorkflowInstance> _insts = new();

    public Task AddDefinitionAsync(WorkflowDefinition def)
    {
        if (!_defs.TryAdd(def.Id, def))
            throw new ApiException(400, "Definition with this ID already exists.");
        return Task.CompletedTask;
    }

    public Task<WorkflowDefinition?> GetDefinitionAsync(string id)
        => Task.FromResult(_defs.GetValueOrDefault(id));

    public Task AddInstanceAsync(WorkflowInstance inst)
    {
        if (!_insts.TryAdd(inst.Id, inst))
            throw new ApiException(400, "Instance with this ID already exists.");
        return Task.CompletedTask;
    }

    public Task<WorkflowInstance?> GetInstanceAsync(string id)
        => Task.FromResult(_insts.GetValueOrDefault(id));

    public Task UpdateInstanceAsync(WorkflowInstance inst)
    {
        _insts[inst.Id] = inst;
        return Task.CompletedTask;
    }
}
