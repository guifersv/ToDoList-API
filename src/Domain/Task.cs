namespace ToDoList.Domain;

public record Task(string Title, string Description, bool IsCompleted, DateTime DateCreated)
{
}
