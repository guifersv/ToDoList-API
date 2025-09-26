using ToDoList.Services.Interfaces;
using ToDoList.Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.Endpoints;

public static class ApiEndpoints
{
  public static RouteGroupBuilder RouteApi(this RouteGroupBuilder group)
  {
    group.MapGet("/{id}", GetTodoList);
    group.MapGet("/", GetAllTodoLists);
    return group;
  }

  [EndpointSummary("Get Todo List")]
  public static async Task<Results<Ok<TodoListModel>, NotFound>> GetTodoList(int id, ITodoService service)
  {
    var model = await service.GetTodoListByIdAsync(id);

    if (model is null)
      return TypedResults.NotFound();
    else
      return TypedResults.Ok(model);
  }

  [EndpointSummary("Get All Todo Lists")]
  public static async Task<IEnumerable<TodoListModel>> GetAllTodoLists(ITodoService service)
  {
    return await service.GetAllTodoListsAsync();
  }

}
