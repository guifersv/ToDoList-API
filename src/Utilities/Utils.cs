using ToDoList.Domain;

namespace ToDoList.Utilities;

public static class Utils
{
  public static TodoListDto TodoList2Dto(TodoListModel todoListModel)
  {
    TodoListDto todoListDto = new()
    {
      Id = todoListModel.Id,
      Title = todoListModel.Title,
      Description = todoListModel.Description,
      Todos = todoListModel.Todos.Select(t => new TodoDto()
      {
        Id = t.Id,
        Title = t.Title,
        Description = t.Description,
        DateCreated = t.DateCreated,
        IsCompleted = t.IsCompleted,
      })
    };
    return todoListDto;
  }
}
