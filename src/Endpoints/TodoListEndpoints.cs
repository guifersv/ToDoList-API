using ToDoList.Services.Interfaces;
using ToDoList.Domain;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.Endpoints;

public static class TodoListEndpoints
{
  public static RouteGroupBuilder RouteTodoList(this RouteGroupBuilder group)
  {
    group.MapGet("/{todoListId}", GetTodoList).WithName(nameof(GetTodoList));
    group.MapGet("/", GetAllTodoLists);
    group.MapPost("/", CreateTodoList);
    group.MapDelete("/{todoListId}", DeleteTodoList);
    group.MapPut("/{todoListId}", EditTodoList);

    return group;
  }

  [EndpointSummary("Get Todo List")]
  public static async Task<Results<Ok<TodoListModel>, NotFound>> GetTodoList(int todoListId, ITodoService service)
  {
    var model = await service.GetTodoListByIdAsync(todoListId);

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

  [EndpointSummary("Create Todo List")]
  public static async Task<CreatedAtRoute> CreateTodoList(TodoListDto modelDto, ITodoService service)
  {
    var model = new TodoListModel
    {
      Title = modelDto.Title,
      Description = modelDto.Description
    };
    await service.CreateTodoListAsync(model);
    var createdModel = await service.GetTodoListByIdAsync(model.Id);

    return TypedResults.CreatedAtRoute(nameof(GetTodoList), createdModel);
  }

  [EndpointSummary("Delete Todo List")]
  public static async Task<Results<NotFound, NoContent>> DeleteTodoList(int todoListId, ITodoService service)
  {
    var deletedModel = await service.DeleteTodoListAsync(todoListId);

    if (deletedModel is null)
      return TypedResults.NotFound();
    else
      return TypedResults.NoContent();
  }

  [EndpointSummary("Edit Todo List")]
  public static async Task<Results<NotFound, NoContent>> EditTodoList(int todoListId, TodoListDto modelDto, ITodoService service)
  {
    var updatedModel = await service.UpdateTodoListAsync(new TodoListModel()
    {
      Id = todoListId,
      Title = modelDto.Title,
      Description = modelDto.Description
    });

    if (updatedModel is null)
      return TypedResults.NotFound();
    else
      return TypedResults.NoContent();
  }
}
