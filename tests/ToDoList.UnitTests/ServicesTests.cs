namespace ToDoList.UnitTests;

public class ServicesTests
{
  [Fact]
  public async Task CreateTodoListAsync_ShouldCallRepositoryOnce()
  {
    TodoListModel model = new() { Title = "string" };

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.CreateTodoListAsync(
          It.Is<TodoListModel>(m => m.Title == model.Title)))
      .Returns(Task.CompletedTask)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    await service.CreateTodoListAsync(model);

    repositoryMock.Verify();
  }

  [Fact]
  public async Task GetAllTodoListsAsync_ShouldReturnListOfModels_WhenTheyExist()
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
  public async Task GetAllTodoListsAsync_ShouldReturnEmptyList_WhenTheyDoNotExist()
  {
    List<TodoListModel> models = [];

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetAllTodoListsAsync().Result)
      .Returns(models)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    var result = await service.GetAllTodoListsAsync();

    Assert.IsType<List<TodoListModel>>(result);
    Assert.Empty(result);
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
    TodoListModel model = new() { Id = 1, Title = "string" };
    TodoListModel updatedModel = new() { Id = 1, Title = "new" };

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetTodoListByIdAsync(
            It.Is<int>(id => id == updatedModel.Id)).Result)
      .Returns(model)
      .Verifiable(Times.Once());
    repositoryMock
      .Setup(r => r.UpdateTodoListAsync(
            It.Is<TodoListModel>(
              s => s.Id == updatedModel.Id && s.Title == updatedModel.Title)))
      .Returns(Task.CompletedTask)
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    var returnedModel = await service.UpdateTodoListAsync(updatedModel);

    Assert.NotNull(returnedModel);
    Assert.Equal(updatedModel.Id, returnedModel.Id);
    Assert.Equal(updatedModel.Title, returnedModel.Title);
    repositoryMock.Verify();
  }

  [Fact]
  public async Task UpdateTodoListAsync_ShouldNotUpdateModel_WhenItDoesNotExistInDatabase()
  {
    TodoListModel model = new() { Id = 1, Title = "string" };
    TodoListModel updatedModel = new() { Id = 1, Title = "new" };

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetTodoListByIdAsync(
            It.Is<int>(id => id == updatedModel.Id)).Result)
      .Returns((TodoListModel?)null)
      .Verifiable(Times.Once());
    repositoryMock
      .Setup(r => r.UpdateTodoListAsync(
            It.Is<TodoListModel>(
              s => s.Id == updatedModel.Id && s.Title == updatedModel.Title)))
      .Returns(Task.CompletedTask)
      .Callback<TodoListModel>(upd => model = upd)
      .Verifiable(Times.Never());

    var service = new TodoService(repositoryMock.Object, logger);
    var returnedModel = await service.UpdateTodoListAsync(updatedModel);

    Assert.NotEqual(updatedModel, model);
    Assert.Null(returnedModel);
    repositoryMock.Verify();
  }

  [Fact]
  public async Task DeleteTodoListAsync_DeleteModel_WhenItExistsInDatabase()
  {
    TodoListModel model = new() { Id = 1, Title = "string" };
    List<TodoListModel> models = [model];

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetTodoListByIdAsync(
            It.Is<int>(id => id == model.Id)).Result)
      .Returns(model)
      .Verifiable(Times.Once());
    repositoryMock
      .Setup(r => r.DeleteTodoListAsync(
            It.Is<TodoListModel>(
              s => s.Id == model.Id && s.Title == model.Title)))
      .Returns(Task.CompletedTask)
      .Callback<TodoListModel>(upd => models.Remove(upd))
      .Verifiable(Times.Once());

    var service = new TodoService(repositoryMock.Object, logger);
    var returnedModel = await service.DeleteTodoListAsync(model.Id);

    Assert.Empty(models);
    Assert.Equal(model, returnedModel);
    repositoryMock.Verify();
  }

  [Fact]
  public async Task DeleteTodoListAsync_ShouldNotDeleteModel_WhenItDoesNotExistInDatabase()
  {
    TodoListModel model = new() { Id = 1, Title = "string" };
    List<TodoListModel> models = [model];

    var logger = Mock.Of<ILogger<TodoService>>();

    var repositoryMock = new Mock<ITodoRepository>();
    repositoryMock
      .Setup(r => r.GetTodoListByIdAsync(
            It.Is<int>(id => id == model.Id)).Result)
      .Returns((TodoListModel?)null)
      .Verifiable(Times.Once());
    repositoryMock
      .Setup(r => r.DeleteTodoListAsync(
            It.Is<TodoListModel>(
              s => s.Id == model.Id && s.Title == model.Title)))
      .Returns(Task.CompletedTask)
      .Callback<TodoListModel>(upd => models.Remove(upd))
      .Verifiable(Times.Never());

    var service = new TodoService(repositoryMock.Object, logger);
    var returnedModel = await service.DeleteTodoListAsync(model.Id);

    Assert.NotEmpty(models);
    Assert.Null(returnedModel);
    repositoryMock.Verify();
  }
}
