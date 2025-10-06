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

    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }

  [Fact]
  public async Task CreateTodoList_ShouldReturnCreatedAtRoute_WhenModelIsValid()
  {
    TodoListDto model = new() { Id = 1, Title = "string" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.CreateTodoListAsync(
            It.Is<TodoListDto>(d => d.Id == model.Id && d.Title == model.Title)).Result)
      .Returns(model)
      .Verifiable(Times.Once());

    var result = await TodoListEndpoints.CreateTodoList(model, serviceMock.Object);
    var returnedModel = Assert.IsType<CreatedAtRoute>(result);

    var values = Assert.Single(returnedModel.RouteValues);
    Assert.Equal(model.Id, values.Value);
    Assert.Equal(nameof(TodoListEndpoints.GetTodoList), returnedModel.RouteName);
    serviceMock.Verify();
  }

  [Fact]
  public async Task UpdateTodoList_ShouldReturnNoContent_WhenModelExists()
  {
    TodoListDto model = new() { Id = 1, Title = "string" };
    TodoListDto updatedModel = new() { Id = 1, Title = "str" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.UpdateTodoListAsync(
            It.Is<int>(id => id == updatedModel.Id),
            It.Is<TodoListDto>(m => m.Id == updatedModel.Id && m.Title == updatedModel.Title)).Result)
      .Returns(updatedModel)
      .Callback<int, TodoListDto>((_, n) => model = n)
      .Verifiable(Times.Once());

    var result = await TodoListEndpoints.UpdateTodoList(updatedModel.Id, updatedModel, serviceMock.Object);

    Assert.IsType<NoContent>(result.Result);
    Assert.Equal(updatedModel, model);
    serviceMock.Verify();
  }

  [Fact]
  public async Task UpdateTodoList_ShouldReturnNotFound_WhenModelDoesNotExist()
  {
    TodoListDto model = new() { Id = 1, Title = "string" };
    TodoListDto updatedModel = new() { Id = 1, Title = "str" };

    var serviceMock = new Mock<ITodoService>();
    serviceMock
      .Setup(s => s.UpdateTodoListAsync(
            It.Is<int>(id => id == updatedModel.Id),
            It.Is<TodoListDto>(m => m.Id == updatedModel.Id && m.Title == updatedModel.Title)).Result)
      .Returns((TodoListDto?)null)
      .Verifiable(Times.Once());

    var result = await TodoListEndpoints.UpdateTodoList(updatedModel.Id, updatedModel, serviceMock.Object);

    Assert.IsType<NotFound>(result.Result);
    serviceMock.Verify();
  }
}
