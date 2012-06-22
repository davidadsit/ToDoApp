using Moq;
using NUnit.Framework;

namespace Application.Tests.UnitTests
{
	[TestFixture]
	public class ToDoApplicationModifyListUnitTests
	{
		private const string USER_NAME = "user-name";
		private const string LIST_NAME = "list-name";
		private const string ITEM_NAME = "pie";

		private Mock<IToDoListCreator> toDoListCreator;
		private Mock<IUserVerifier> userVerifier;
		private ToDoApplication toDoApplication;
		private Mock<IToDoListRepository> toDoListRepository;

		[SetUp]
		public void SetUp()
		{
			userVerifier = new Mock<IUserVerifier>();
			toDoListCreator = new Mock<IToDoListCreator>();
			toDoListRepository = new Mock<IToDoListRepository>();
			toDoApplication = new ToDoApplication(userVerifier.Object, toDoListCreator.Object, toDoListRepository.Object);
		}

		[Test]
		public void When_adding_an_item_to_a_list_that_exists()
		{
			ToDoList toDoList = new ToDoList();
			toDoListRepository.Setup(x => x.FindListByUserName(It.IsAny<string>(), It.IsAny<string>())).Returns(toDoList);

			toDoApplication.AddItemToList(USER_NAME, LIST_NAME, ITEM_NAME);

			Assert.IsTrue(toDoList.Items.Contains(ITEM_NAME));
			toDoListRepository.Verify(x=>x.Save(toDoList));
		}

		[Test]
		public void When_adding_an_item_to_a_list_that_does_not_exist()
		{
			toDoListRepository.Setup(x => x.FindListByUserName(It.IsAny<string>(), It.IsAny<string>())).Returns((ToDoList) null);

			toDoApplication.AddItemToList(USER_NAME, LIST_NAME, ITEM_NAME);

			toDoListRepository.Verify(x=>x.Save(It.IsAny<ToDoList>()), Times.Never());
		}
	}
}