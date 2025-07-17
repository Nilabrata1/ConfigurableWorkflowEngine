using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WorkflowEngine.Repositories;
using WorkflowEngine.Services;
using WorkflowEngine.Validators;
using WorkflowEngine.Errors;
using WorkflowEngine.DTOs;
using WorkflowEngine.Models;


var builder = WebApplication.CreateBuilder(args);

// Dependency injection
builder.Services.AddSingleton<IWorkflowRepository, InMemoryWorkflowRepository>();
builder.Services.AddSingleton<DefinitionValidator>();
builder.Services.AddSingleton<ExecutionValidator>();
builder.Services.AddSingleton<IDefinitionService, DefinitionService>();
builder.Services.AddSingleton<IInstanceService, InstanceService>();

var app = builder.Build();

// Global error handling middleware
app.Use(async (ctx, next) => {
    try { await next(); }
    catch (ApiException ex) {
        ctx.Response.StatusCode = ex.StatusCode;
        await ctx.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
});

// Minimal‑API endpoints
app.MapPost("/api/workflows", async (CreateDefinitionRequest req, IDefinitionService svc) =>
    Results.Created($"/api/workflows/{(await svc.CreateAsync(req)).Id}", null));

app.MapGet("/api/workflows/{id}", async (string id, IDefinitionService svc) =>
    Results.Ok(await svc.GetDefinitionAsync(id)));

app.MapPost("/api/workflows/{id}/instances", async (string id, IInstanceService svc) =>
    Results.Created($"/api/instances/{(await svc.StartInstanceAsync(id)).Id}", null));

app.MapGet("/api/instances/{instanceId}", async (string instanceId, IInstanceService svc) =>
    Results.Ok(await svc.GetInstanceAsync(instanceId)));

app.MapPost("/api/instances/{instanceId}/actions/{actionId}", async (string instanceId, string actionId, IInstanceService svc) =>
    Results.Ok(await svc.ExecuteActionAsync(instanceId, actionId)));

app.Run();
