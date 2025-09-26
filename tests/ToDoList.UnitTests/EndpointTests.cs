using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.UnitTests;

public class EndpointTests
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
    TodoListModel model = new() { Title = "string" };

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
}
