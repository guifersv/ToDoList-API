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

    Assert.Single(result, model);
    serviceMock.Verify();
  }
}
