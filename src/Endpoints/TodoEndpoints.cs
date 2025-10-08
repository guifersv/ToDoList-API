using ToDoList.Domain;
using ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.Endpoints;

public static class TodoEndpoints
{
  public static RouteGroupBuilder RouteTodoEndpoint(this RouteGroupBuilder group)
  {
    group.MapPost("/{todoListId}", CreateTodo);
    group.MapDelete("/{todoId}", DeleteTodo);
    group.MapPatch("/{todoId}", ChangeTodoIsComplete);

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

  public static async Task<Results<NoContent, NotFound>> DeleteTodo(int todoId, ITodoService service)
  {
    var deletedModel = await service.DeleteTodoAsync(todoId);

    if (deletedModel is not null)
      return TypedResults.NoContent();
    else
      return TypedResults.NotFound();
  }

  public static async Task<Results<NoContent, NotFound>> ChangeTodoIsComplete(int todoId, ITodoService service)
  {
    var changedModel = await service.ChangeTodoIsCompleteAsync(todoId);

    if (changedModel is not null)
      return TypedResults.NoContent();
    else
      return TypedResults.NotFound();
  }
}
