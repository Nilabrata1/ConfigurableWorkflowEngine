using WorkflowEngine.Models;

namespace WorkflowEngine.Models;

public record WorkflowDefinition(
    string Id,
    string Name,
    IReadOnlyCollection<State> States,
    IReadOnlyCollection<TransitionAction> Actions
);
