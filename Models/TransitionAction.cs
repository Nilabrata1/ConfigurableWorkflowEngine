namespace WorkflowEngine.Models;

public record TransitionAction(
    string Id,
    string Name,
    bool Enabled,
    IEnumerable<string> FromStates,
    string ToState
);
