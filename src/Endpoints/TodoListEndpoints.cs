using ToDoList.Domain;
using ToDoList.Services.Interfaces;

namespace ToDoList.Endpoints;

public static class TodoListEndpoints
{
  public static RouteGroupBuilder RouteTodoListEndpoint(this RouteGroupBuilder group)
  {
    group.MapGet("/", GetAllTodoLists);
    return group;
  }

  [EndpointSummary("Get all todo list models")]
  public static async Task<IEnumerable<TodoListDto>> GetAllTodoLists(ITodoService service)
  {
    return await service.GetAllTodoListsAsync();
  }
}
