using CLI.PoS.Model;

namespace API.CLI.Database
{
    public class FakeDatabase
    {
        private List<Item> menuItems;
        private FakeDatabase() { 
            menuItems = new List<Item> {
                new Item{ Id = 1, Name = "Something 1", Description="This is a test description", Price = 1 }
                , new Item{ Id = 2, Name = "Something 2", Price = 2 }
                , new Item{ Id = 3, Name = "Something 3", Price = 3 } };
        }

        private static FakeDatabase? instance;
        public static FakeDatabase Current
        {
            get
            {
                if(instance == null)
                {
                    instance = new FakeDatabase();
                }
                return instance;
            }
        }

        public List<Item> MenuItems => menuItems;

    }
}
