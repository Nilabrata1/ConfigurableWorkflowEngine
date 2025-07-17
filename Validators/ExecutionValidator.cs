using WorkflowEngine.Models;
using WorkflowEngine.Errors;

namespace WorkflowEngine.Validators;

public class ExecutionValidator
{
    public void Validate(WorkflowDefinition def, WorkflowInstance inst, string actionId)
    {
        var curr = def.States.Single(s => s.Id == inst.CurrentState);
        if (curr.IsFinal)
            throw new ApiException(400, "Cannot execute actions on a final state.");

        var action = def.Actions.SingleOrDefault(a => a.Id == actionId)
                     ?? throw new ApiException(400, "Action not found in definition.");

        if (!action.Enabled)
            throw new ApiException(400, "Action is disabled.");

        if (!action.FromStates.Contains(inst.CurrentState))
            throw new ApiException(400, "Action cannot be executed from current state.");
    }
}
