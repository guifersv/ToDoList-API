namespace ToDoList.UnitTests;

public class ServicesTests
{
  [Fact]
  public async Task CreateAsync_ShouldCallRepositoryOnce()
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
  public async Task GetAllAsync_ShouldReturnSameOfRepository_ShoulCallOnce()
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
  public void GetTodo_()
  {
    Assert.True(true);
  }

  [Fact]
  public void Update_()
  {
    Assert.True(true);
  }

  [Fact]
  public void Delete_()
  {
    Assert.True(true);
  }
}
