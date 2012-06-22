using Moq;
using NUnit.Framework;

namespace Application.Tests.UnitTests
{
	[TestFixture]
	public class ToDoApplicationCreateListUnitTests
	{
		private const string USER_NAME = "user-name";
		private const string LIST_NAME = "list-name";
		
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
		public void When_creating_a_new_list_the_UserVerifier_is_called_correctly()
		{
			userVerifier.Setup(x => x.VerifyUserName(It.IsAny<string>())).Returns(true);

			toDoApplication.CreateList(USER_NAME, LIST_NAME);

			userVerifier.Verify(x => x.VerifyUserName(USER_NAME));
		}

		[Test]
		public void When_creating_a_new_list_with_a_invalid_userName()
		{
			userVerifier.Setup(x => x.VerifyUserName(It.IsAny<string>())).Returns(false);

			toDoApplication.CreateList(USER_NAME, LIST_NAME);

			toDoListCreator.Verify(x => x.CreateListForUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
		}

		[Test]
		public void When_creating_a_new_list_with_a_valid_userName()
		{
			userVerifier.Setup(x => x.VerifyUserName(It.IsAny<string>())).Returns(true);

			toDoApplication.CreateList(USER_NAME, LIST_NAME);

			toDoListCreator.Verify(x => x.CreateListForUser(USER_NAME, LIST_NAME));
		}
	}
}