using System.Collections.Generic;

namespace Application
{
	public class ToDoList
	{
		public ToDoList()
		{
			Items = new List<string>();
		}

		public string ListName { get; set; }
		public List<string> Items { get; private set; }
	}
}