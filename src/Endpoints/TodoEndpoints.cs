using ToDoList.Domain;
using ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.Endpoints;

public static class TodoEndpoints
{
  public static RouteGroupBuilder RouteTodoEndpoint(this RouteGroupBuilder group)
  {
    group.MapPost("/{todoListId}", CreateTodo);

    return group;
  }

  public static async Task<Results<Created, NotFound>> CreateTodo(int todoListId, TodoDto todoDto, ITodoService service)
  {
    var createdModel = await service.CreateTodoAsync(todoListId, todoDto);

    if (createdModel is not null)
      return TypedResults.Created();
    else
      return TypedResults.NotFound();
  }
}
