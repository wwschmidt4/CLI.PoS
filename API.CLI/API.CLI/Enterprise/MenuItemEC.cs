using API.CLI.Database;
using CLI.PoS.Model;

namespace API.CLI.Enterprise
{
    public class MenuItemEC
    {
        public MenuItemEC() { }

        public IEnumerable<Item> Items => FakeDatabase.Current.MenuItems;

        public Item? Delete(int id)
        {
            var item = FakeDatabase.Current.MenuItems.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                FakeDatabase.Current.MenuItems.Remove(item);
            }
            return item;
        }

        public Item? AddOrUpdate(Item item)
        {

            if (item.Id == 0)
            {
                item.Id = NextKey;
                FakeDatabase.Current.MenuItems.Add(item);
            }

            return item;
        }

        public int NextKey
        {
            get
            {
                if (Items.Any())
                {
                    return Items.Select(i => i.Id).Max() + 1;
                }
                return 1;
            }
        }
    }
}
