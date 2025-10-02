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
      ApiEndpoints.GetTodoList(1, serviceMock.Object);

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
      ApiEndpoints.GetTodoList(1, serviceMock.Object);

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
      ApiEndpoints.GetAllTodoLists(serviceMock.Object);

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
      ApiEndpoints.CreateTodoList(model, serviceMock.Object);

    Assert.IsType<CreatedAtRoute>(result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task DeleteTodoList_ShouldReturnNoContent_WhenModelExists()
  {
    TodoListModel model = new() { Id = 1, Title = "string" };
    List<TodoListModel> models = [model];

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.DeleteTodoListAsync(It.Is<int>(
              i => i == model.Id)).Result)
      .Returns(model)
      .Callback<TodoListModel>(c => models.Remove(c))
      .Verifiable(Times.Once());

    var result =
      await ApiEndpoints.DeleteTodoList(model.Id, serviceMock.Object);

    Assert.IsType<NoContent>(result.Result);
    Assert.Empty(models);
    serviceMock.Verify();
  }

  [Fact]
  public async Task DeleteTodoList_ShouldReturnNotFound_WhenModelDoesNotExist()
  {
    TodoListModel model = new() { Title = "string" };
    List<TodoListModel> models = [model];

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.DeleteTodoListAsync(It.Is<int>(
              i => i == model.Id)).Result)
      .Returns((TodoListModel?)null)
      .Callback<TodoListModel>(c => models.Remove(c))
      .Verifiable(Times.Never());

    var result =
      await ApiEndpoints.DeleteTodoList(1, serviceMock.Object);

    Assert.NotEmpty(models);
    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task EditTodoList_ShouldReturnNoContent_WhenModelExists()
  {
    TodoListModel model = new() { Title = "string" };
    TodoListModel updatedModel = new() { Title = "str" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetTodoListByIdAsync(It.IsAny<int>()).Result)
      .Returns(model)
      .Verifiable(Times.Once());
    serviceMock
      .Setup(s => s.UpdateTodoListAsync(
            It.Is<TodoListModel>(w => w.Title == model.Title)))
      .Returns(Task.CompletedTask)
      .Callback<TodoListModel>(m => model = m)
      .Verifiable(Times.Once());

    var result =
      await ApiEndpoints.EditTodoList(1, new TodoListDto { Title = "str" }, serviceMock.Object);

    Assert.Equal(updatedModel.Title, model.Title);
    Assert.IsType<NoContent>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task EditTodoList_ShouldReturnNotFound_WhenModelDoesNotExist()
  {
    TodoListModel model = new() { Title = "string" };
    TodoListModel updatedModel = new() { Title = "str" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetTodoListByIdAsync(It.IsAny<int>()).Result)
      .Returns((TodoListModel?)null)
      .Verifiable(Times.Once());
    serviceMock
      .Setup(s => s.UpdateTodoListAsync(
            It.Is<TodoListModel>(w => w.Title == model.Title)))
      .Returns(Task.CompletedTask)
      .Callback<TodoListModel>(m => model = m)
      .Verifiable(Times.Never());

    var result =
      await ApiEndpoints.EditTodoList(1, new TodoListDto { Title = "str" }, serviceMock.Object);

    Assert.NotEqual(updatedModel.Title, model.Title);
    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }
}
