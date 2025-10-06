using ToDoList.Domain;
using ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.Endpoints;

public static class TodoListEndpoints
{
  public static RouteGroupBuilder RouteTodoListEndpoint(this RouteGroupBuilder group)
  {
    group.MapGet("/", GetAllTodoLists);
    group.MapGet("/{todoListId}", GetTodoList).WithName(nameof(GetTodoList));
    group.MapPost("/", CreateTodoList);
    group.MapPut("/{todoListId}", UpdateTodoList);

    return group;
  }

  [EndpointSummary("Get all todo list models")]
  public static async Task<IEnumerable<TodoListDto>> GetAllTodoLists(ITodoService service)
  {
    return await service.GetAllTodoListsAsync();
  }

  [EndpointSummary("Get todo list model")]
  public static async Task<Results<Ok<TodoListDto>, NotFound>> GetTodoList(int todoListId, ITodoService service)
  {
    var returnedModel = await service.GetTodoListByIdAsync(todoListId);

    if (returnedModel is not null)
      return TypedResults.Ok(returnedModel);
    else
      return TypedResults.NotFound();
  }

  [EndpointSummary("Create todo list")]
  public static async Task<CreatedAtRoute> CreateTodoList(TodoListDto todoListDto, ITodoService service)
  {
    var createdModel = await service.CreateTodoListAsync(todoListDto);
    return TypedResults.CreatedAtRoute(nameof(GetTodoList), new { todoListId = createdModel.Id });
  }

  [EndpointSummary("Update todo list")]
  public static async Task<Results<NoContent, NotFound>> UpdateTodoList(int todoListId, TodoListDto todoListDto, ITodoService service)
  {
    var updatedModel = await service.UpdateTodoListAsync(todoListId, todoListDto);

    if (updatedModel is not null)
      return TypedResults.NoContent();
    else
      return TypedResults.NotFound();
  }
}
