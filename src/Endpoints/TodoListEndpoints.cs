using Microsoft.AspNetCore.Http.HttpResults;

using ToDoList.Domain;
using ToDoList.Services.Interfaces;

namespace ToDoList.Endpoints;

public static class TodoListEndpoints
{
    public static RouteGroupBuilder RouteTodoListEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllTodoLists);
        group.MapGet("/{todoListId}", GetTodoList).WithName(nameof(GetTodoList));
        group.MapPost("/", CreateTodoList);
        group.MapPut("/{todoListId}", UpdateTodoList);
        group.MapDelete("/{todoListId}", DeleteTodoList);

        return group;
    }

    [EndpointSummary("Get all TodoList models")]
    public static async Task<IEnumerable<TodoListDto>> GetAllTodoLists(ITodoService service)
    {
        return await service.GetAllTodoListsAsync();
    }

    [EndpointSummary("Get TodoList model")]
    public static async Task<Results<Ok<TodoListDto>, NotFound>> GetTodoList(int todoListId, ITodoService service)
    {
        var returnedModel = await service.GetTodoListByIdAsync(todoListId);

        if (returnedModel is not null)
            return TypedResults.Ok(returnedModel);
        else
            return TypedResults.NotFound();
    }

    [EndpointSummary("Create TodoList")]
    public static async Task<CreatedAtRoute> CreateTodoList(TodoListDto todoListDto, ITodoService service)
    {
        var createdModel = await service.CreateTodoListAsync(todoListDto);
        return TypedResults.CreatedAtRoute(nameof(GetTodoList), new { todoListId = createdModel.Id });
    }

    [EndpointSummary("Update TodoList")]
    public static async Task<Results<NoContent, NotFound>> UpdateTodoList(int todoListId, TodoListDto todoListDto, ITodoService service)
    {
        var updatedModel = await service.UpdateTodoListAsync(todoListId, todoListDto);

        if (updatedModel is not null)
            return TypedResults.NoContent();
        else
            return TypedResults.NotFound();
    }

    [EndpointSummary("Delete TodoList")]
    public static async Task<Results<NoContent, NotFound>> DeleteTodoList(int todoListId, ITodoService service)
    {
        var deletedModel = await service.DeleteTodoListAsync(todoListId);

        if (deletedModel is not null)
            return TypedResults.NoContent();
        else
            return TypedResults.NotFound();
    }
}