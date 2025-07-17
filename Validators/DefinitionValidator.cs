using WorkflowEngine.Models;
using WorkflowEngine.Errors;

namespace WorkflowEngine.Validators;

public class DefinitionValidator
{
    public void Validate(WorkflowDefinition def)
    {
        if (def.States.Count(s => s.IsInitial) != 1)
            throw new ApiException(400, "Definition must have exactly one initial state.");

        var ids = def.States.Select(s => s.Id).ToList();
        if (ids.Distinct().Count() != ids.Count)
            throw new ApiException(400, "Duplicate state IDs found.");

        foreach (var act in def.Actions)
        {
            if (!def.States.Any(s => s.Id == act.ToState))
                throw new ApiException(400, $"Action '{act.Id}' targets unknown state '{act.ToState}'.");
            foreach (var src in act.FromStates)
                if (!def.States.Any(s => s.Id == src))
                    throw new ApiException(400, $"Action '{act.Id}' has unknown source state '{src}'.");
        }
    }
}
