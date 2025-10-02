using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.UnitTests;

public class HandlersTests
{
  [Fact]
  public async Task GetTodoList_ShouldReturnOk_WhenModelExists()
  {
    TodoListModel model = new() { Title = "string" };
    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetTodoListByIdAsync(It.IsAny<int>()).Result)
      .Returns(model)
      .Verifiable(Times.Once());

    var result = await
      TodoListEndpoints.GetTodoList(1, serviceMock.Object);

    Assert.IsType<Ok<TodoListModel>>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task GetTodoList_ShouldReturnNotFound_WhenModelDoesNotExist()
  {
    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetTodoListByIdAsync(It.IsAny<int>()).Result)
      .Returns((TodoListModel?)null)
      .Verifiable(Times.Once());

    var result = await
      TodoListEndpoints.GetTodoList(1, serviceMock.Object);

    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task GetAllTodoLists_ShoudCallReturnEnumarable_ShouldCallService()
  {
    IEnumerable<TodoListModel> models = [];
    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetAllTodoListsAsync().Result)
      .Returns(models)
      .Verifiable(Times.Once());

    var result = await
      TodoListEndpoints.GetAllTodoLists(serviceMock.Object);

    Assert.Equal(result, models);
    serviceMock.Verify();
  }

  [Fact]
  public async Task CreateTodoList_ShouldReturnCreatedAtRoute_ShouldCallService()
  {
    TodoListDto model = new() { Title = "string" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.CreateTodoListAsync(It.Is<TodoListModel>(m => m.Title == model.Title)))
      .Returns(Task.CompletedTask)
      .Verifiable(Times.Once());

    var result = await
      TodoListEndpoints.CreateTodoList(model, serviceMock.Object);

    Assert.IsType<CreatedAtRoute>(result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task DeleteTodoList_ShouldReturnNoContent_WhenModelExists()
  {
    TodoListModel model = new() { Id = 1, Title = "string" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.DeleteTodoListAsync(It.Is<int>(
              i => i == model.Id)).Result)
      .Returns(model)
      .Verifiable(Times.Once());

    var result =
      await TodoListEndpoints.DeleteTodoList(model.Id, serviceMock.Object);

    Assert.IsType<NoContent>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task DeleteTodoList_ShouldReturnNotFound_WhenModelDoesNotExist()
  {
    TodoListModel model = new() { Title = "string" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.DeleteTodoListAsync(It.Is<int>(
              i => i == model.Id)).Result)
      .Returns((TodoListModel?)null)
      .Verifiable(Times.Never());

    var result =
      await TodoListEndpoints.DeleteTodoList(1, serviceMock.Object);

    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task EditTodoList_ShouldReturnNoContent_WhenModelExists()
  {
    TodoListModel model = new() { Id = 1, Title = "string", Description = "" };
    TodoListModel updatedModel = new() { Id = 1, Title = "str", Description = "" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.UpdateTodoListAsync(
            It.Is<TodoListModel>(
              w => w.Title == updatedModel.Title && w.Id == updatedModel.Id &&
              w.Description == updatedModel.Description)).Result)
      .Returns(updatedModel)
      .Verifiable(Times.Once());

    var result =
      await TodoListEndpoints.EditTodoList(
          updatedModel.Id, new TodoListDto { Title = updatedModel.Title, Description = updatedModel.Description }, serviceMock.Object);

    Assert.IsType<NoContent>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task EditTodoList_ShouldReturnNotFound_WhenModelDoesNotExist()
  {
    TodoListModel model = new() { Id = 1, Title = "string", Description = "" };
    TodoListModel updatedModel = new() { Id = 1, Title = "str", Description = "" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.UpdateTodoListAsync(
            It.Is<TodoListModel>(
              w => w.Title == updatedModel.Title && w.Id == updatedModel.Id &&
              w.Description == updatedModel.Description)).Result)
      .Returns((TodoListModel?)null)
      .Verifiable(Times.Once());

    var result =
      await TodoListEndpoints.EditTodoList(
          updatedModel.Id, new TodoListDto { Title = updatedModel.Title, Description = updatedModel.Description }, serviceMock.Object);

    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task GetTodo_ShouldReturnOkTodos_WhenModelExists()
  {
    TodoListModel model = new() { Id = 1, Title = "string" };
    TodoModel todoModel = new() { Title = "todo", TodoListModelNavigation = model };
    ((List<TodoModel>)model.Todos).Add(todoModel);

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetAllTodoAsync(It.Is<int>(w => w == model.Id)).Result)
      .Returns(model.Todos)
      .Verifiable(Times.Once);

    var result = await TodoEndpoints.GetTodo(
        model.Id, serviceMock.Object);

    var resultModel = Assert.IsType<Ok<IEnumerable<TodoModel>>>(result.Result);
    Assert.Single(resultModel.Value!, todoModel);
    serviceMock.Verify();
  }

  [Fact]
  public async Task GetTodo_ShouldReturnNotFound_WhenModelDoesNotExist()
  {
    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetAllTodoAsync(It.IsAny<int>()).Result)
      .Returns((IEnumerable<TodoModel>?)null)
      .Verifiable(Times.Once);

    var result = await TodoEndpoints.GetTodo(
        1, serviceMock.Object);

    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }
}
