using ToDoList.Services.Interfaces;
using ToDoList.Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.Endpoints;

public static class TodoEndpoints
{
  public static RouteGroupBuilder RouteTodo(this RouteGroupBuilder group)
  {
    group.MapGet("/{todoListId}", GetTodo);
    return group;
  }

  public static async Task<Results<NotFound, Ok<IEnumerable<TodoModel>>>> GetTodo(int todoListId, ITodoService service)
  {
    var returnedModel = await service.GetAllTodoAsync(todoListId);
    if (returnedModel is null)
      return TypedResults.NotFound();
    else
      return TypedResults.Ok(returnedModel);
  }
}
