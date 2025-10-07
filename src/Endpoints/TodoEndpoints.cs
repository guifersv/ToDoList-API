using ToDoList.Domain;
using ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.Endpoints;

public static class TodoEndpoints
{
  public static RouteGroupBuilder RouteTodoEndpoint(this RouteGroupBuilder group)
  {
    group.MapGet("/{todoId}", GetTodo).WithName(nameof(GetTodo));

    return group;
  }

  public static async Task<Results<Ok<TodoDto>, NotFound>> GetTodo(int todoId, ITodoService service)
  {
    var todoModel = await service.GetTodoByIdAsync(todoId);

    if (todoModel is not null)
      return TypedResults.Ok(todoModel);
    else
      return TypedResults.NotFound();

  }
}
