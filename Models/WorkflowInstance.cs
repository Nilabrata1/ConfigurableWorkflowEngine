
using WorkflowEngine.Models;


namespace WorkflowEngine.Models;

public class WorkflowInstance
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string DefinitionId { get; init; }
    public string CurrentState { get; private set; }
    public List<HistoryEntry> History { get; } = new();

    public WorkflowInstance(string definitionId, string initialState)
    {
        DefinitionId = definitionId;
        CurrentState = initialState;
    }

    public void ApplyAction(string actionId, string newState)
    {
        CurrentState = newState;
        History.Add(new HistoryEntry(actionId, DateTime.UtcNow));
    }
}
