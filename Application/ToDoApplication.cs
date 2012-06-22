namespace Application
{
	public class ToDoApplication
	{
		private readonly IToDoListCreator toDoListCreator;
		private readonly IToDoListRepository toDoListRepository;
		private readonly IUserVerifier userVerifier;

		public ToDoApplication(IUserVerifier userVerifier, IToDoListCreator toDoListCreator, IToDoListRepository toDoListRepository)
		{
			this.userVerifier = userVerifier;
			this.toDoListCreator = toDoListCreator;
			this.toDoListRepository = toDoListRepository;
		}

		public void AddItemToList(string userName, string listName, string item)
		{
			ToDoList toDoList = toDoListRepository.FindListByUserName(userName, listName);
			if (toDoList != null)
			{
				toDoList.Items.Add(item);
				toDoListRepository.Save(toDoList);
			}
		}

		public void CreateList(string userName, string listName)
		{
			if (userVerifier.VerifyUserName(userName))
			{
				toDoListCreator.CreateListForUser(userName, listName);
			}
		}

		public ToDoList GetToDoListForUser(string userName)
		{
			return null;
		}

		public void RemoveItemFromList(string listName, string itemName)
		{
		}
	}
}