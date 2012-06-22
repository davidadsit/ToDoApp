namespace Application
{
	public interface IToDoListRepository
	{
		ToDoList FindListByUserName(string userName, string listName);
		void Save(ToDoList toDoList);
	}
}