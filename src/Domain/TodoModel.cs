namespace ToDoList.Domain;

public record TodoModel(string Title, string Description, bool IsCompleted, DateTime DateCreated)
{
}
