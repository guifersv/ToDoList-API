using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoList.UnitTests;

public class EndpointsTests
{
  [Fact]
  public async Task GetAllTodoLists_ShouldReturnModels_ShouldCallService()
  {
    TodoListDto model = new() { Title = "string" };
    List<TodoListDto> models = [model];
    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetAllTodoListsAsync().Result)
      .Returns(models)
      .Verifiable(Times.Once());

    var result = await TodoListEndpoints.GetAllTodoLists(serviceMock.Object);

    Assert.IsType<IEnumerable<TodoListDto>>(result, exactMatch: false);
    Assert.Single(result, model);
    serviceMock.Verify();
  }

  [Fact]
  public async Task GetTodoList_ShouldReturnOk_WhenItExists()
  {
    TodoListDto model = new() { Id = 1, Title = "string" };
    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetTodoListByIdAsync(It.Is<int>(id => id == model.Id)).Result)
      .Returns(model)
      .Verifiable(Times.Once());

    var result = await TodoListEndpoints.GetTodoList(model.Id, serviceMock.Object);

    var returnedModel = Assert.IsType<Ok<TodoListDto>>(result.Result);
    Assert.Equal(model, returnedModel.Value);
    serviceMock.Verify();
  }

  [Fact]
  public async Task GetTodoList_ShouldReturnNotFound_WhenItDoesNotExist()
  {
    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.GetTodoListByIdAsync(It.IsAny<int>()).Result)
      .Returns((TodoListDto?)null)
      .Verifiable(Times.Once());

    var result = await TodoListEndpoints.GetTodoList(1, serviceMock.Object);

    var returnedModel = Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }
}
