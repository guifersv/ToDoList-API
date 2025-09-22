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
  public void GetAll_()
  {
    Assert.True(true);
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
