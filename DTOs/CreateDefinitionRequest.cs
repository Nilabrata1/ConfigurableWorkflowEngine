// File: DTOs/CreateDefinitionRequest.cs
using WorkflowEngine.Models;

namespace WorkflowEngine.DTOs
{
    public class CreateDefinitionRequest
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public List<State> States { get; set; } = new();
        public List<TransitionAction> Actions { get; set; } = new();
    }
}
