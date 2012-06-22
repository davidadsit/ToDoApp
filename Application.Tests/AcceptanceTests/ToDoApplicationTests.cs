using NUnit.Framework;

namespace Application.Tests.AcceptanceTests
{
	[TestFixture]
	public class ToDoApplicationTests
	{
		private const string USER_NAME = "user-name";
		private const string LIST_NAME = "list-name";
		private const string ITEM_NAME = "pie";
		private ToDoApplication toDoApplication;

		[SetUp]
		public void SetUp()
		{
			toDoApplication = new ToDoApplication(null, null, null);
		}

		[Test]
		public void Add_an_item_to_a_list()
		{
			toDoApplication.CreateList(USER_NAME, LIST_NAME);

			toDoApplication.AddItemToList(USER_NAME, LIST_NAME, ITEM_NAME);

			ToDoList toDoListForUser = toDoApplication.GetToDoListForUser(USER_NAME);
			Assert.IsTrue(toDoListForUser.Items.Contains(ITEM_NAME));
		}

		[Test]
		public void Creates_a_todo_list_for_a_user()
		{
			toDoApplication.CreateList(USER_NAME, LIST_NAME);

			ToDoList toDoListForUser = toDoApplication.GetToDoListForUser(USER_NAME);
			Assert.AreEqual(LIST_NAME, toDoListForUser.ListName);
		}

		[Test]
		public void Delete_an_item_from_a_list()
		{
			toDoApplication.CreateList(USER_NAME, LIST_NAME);
			toDoApplication.AddItemToList(USER_NAME, LIST_NAME, ITEM_NAME);

			toDoApplication.RemoveItemFromList(LIST_NAME, ITEM_NAME);

			ToDoList toDoListForUser = toDoApplication.GetToDoListForUser(USER_NAME);
			Assert.IsFalse(toDoListForUser.Items.Contains(ITEM_NAME));
		}
	}

	//UI
	//Application <--
	//Domain
	//Database
}