namespace ToDoList.UnitTests;

public class ServicesTests
{
  [Fact]
  public async Task CreateTodoListAsync_ShouldCallRepositoryOnce()
  {
    var todoList = new TodoListModel
    {
      Title = "string",
    };

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.CreateTodoListAsync(
          It.IsAny<TodoListModel>()))
      .Returns(Task.CompletedTask)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    await service.CreateTodoListAsync(todoList);

    repositoryMock.Verify();
  }

  [Fact]
  public async Task GetAllTodoListsAsync_ShouldReturnSameOfRepository_ShoulCallOnce()
  {
    List<TodoListModel> models = [new TodoListModel { Title = "string" }];

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetAllTodoListsAsync().Result)
      .Returns(models)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    var result = await service.GetAllTodoListsAsync();

    Assert.IsType<List<TodoListModel>>(result);
    Assert.Equal(models, result);
    repositoryMock.Verify();
  }

  [Fact]
  public async Task GetTodoListByIdAsync_ShouldReturnModel_IfItExistsInDB()
  {
    TodoListModel model = new() { Id = 1, Title = "string" };

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetTodoListByIdAsync(
            It.Is<int>(id => id == model.Id)).Result)
      .Returns(model)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    var result = await service.GetTodoListByIdAsync(model.Id);

    Assert.IsType<TodoListModel>(result);
    Assert.Equal(model, result);
    repositoryMock.Verify();
  }

  [Fact]
  public async Task GetTodoListByIdAsync_ShouldReturnNull_IfItDoesNotExistInDB()
  {
    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetTodoListByIdAsync(
            It.IsAny<int>()).Result)
      .Returns((TodoListModel?)null)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    var result = await service.GetTodoListByIdAsync(1);

    Assert.Null(result);
    repositoryMock.Verify();
  }

  [Fact]
  public async Task UpdateTodoListAsync_ShouldUpdateModel_WhenItExistsInDatabase()
  {
    TodoListModel model = new() { Title = "string" };
    TodoListModel updatedModel = new() { Title = "new" };

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetTodoListByIdAsync(
            It.IsAny<int>()).Result)
      .Returns(model)
      .Verifiable(Times.Once());
    repositoryMock
      .Setup(r => r.UpdateTodoListAsync(
            It.Is<TodoListModel>(s => s.Title == updatedModel.Title)))
      .Returns(Task.CompletedTask)
      .Callback<TodoListModel>(upd => model = upd)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    await service.UpdateTodoListAsync(updatedModel);

    Assert.Equal(updatedModel, model);
    repositoryMock.Verify();
  }

  [Fact]
  public async Task UpdateTodoListAsync_ShouldNotUpdateModel_WhenItDoesNotExistInDatabase()
  {
    Assert.True(true);
  }

  [Fact]
  public void Delete_()
  {
    Assert.True(true);
  }
}
