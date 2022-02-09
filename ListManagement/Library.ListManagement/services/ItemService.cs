using Library.ListManagement.helpers;
using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.services
{
    public class ItemService
    {
        private List<Item> items;
        private ListNavigator<Item> itemNav;
        private static ItemService? instance;

        public List<Item> Items { get { return items; } }

        public static ItemService Current { 
            get { 
                if(instance == null)
                    instance = new ItemService();
                return instance; 
            } 
        }
        public Dictionary<object, Item> GetPage()
        {
            var page = itemNav.GetCurrentPage();
            if (itemNav.HasNextPage)
            {
                page.Add("N", new Item { Name = "Next" });
            }
            if (itemNav.HasPreviousPage)
            {
                page.Add("P", new Item { Name = "Previous" });
            }
            return page;
        }
        public Dictionary<object, Item> NextPage()
        {
            return itemNav.GoForward();
        }

        public Dictionary<object, Item> PreviousPage()
        {
            return itemNav.GoBackward();
        }

        private ItemService()
        {
            items = new List<Item>();
            itemNav = new ListNavigator<Item>(Items, 2);
        }

        public void Add(Item item_added)
        {
            items.Add(item_added);
        }

        public void Remove(Item item_removed)
        {
            items.Remove(item_removed);

        }
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        public void Replace(int index, Item item_replaced)
        {
            items[index] = item_replaced;
        }
    }
}
