using WorkflowEngine.Models;

namespace WorkflowEngine;

public class CreateDefinitionRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<State> States { get; set; } = new();
    public List<TransitionAction> Actions { get; set; } = new();
}
