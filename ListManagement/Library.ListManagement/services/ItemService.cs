using Library.ListManagement.helpers;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = ListManagement.models.Task;

namespace ListManagement.services
{
    public class ItemService
    {
        private List<Item> items;
        private ListNavigator<Item> itemNav;
        private static ItemService? instance;
        private string persistence_path;
        private JsonSerializerSettings serializer_settings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All};

        public List<Item> Items { get { return items; } }

        public static ItemService Current { 
            get { 
                if(instance == null)
                    instance = new ItemService();
                return instance; 
            } 
        }
        public Dictionary<object, Item> GetPage(string type)
        {
            //ListNavigator<Item> itemNav = new ListNavigator<Item>(Items, 2);
            //if (type == "all")
            //    itemNav = new ListNavigator<Item>(Items, 2);
            //if (type == "incomplete")
            //    itemNav = new ListNavigator<Item>(IncompleteItems, 2);
            var page = itemNav.GetCurrentPage();
            if (itemNav.HasNextPage)
            {
                page.Add("N", new Item { Name = "Next" });
            }
            if (itemNav.HasPreviousPage)
            {
                page.Add("P", new Item { Name = "Previous" });
            }
            page.Add("E", new Item { Name = "Exit" });
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
            persistence_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\SaveData.json";
            if (File.Exists(persistence_path))
            {
                try
                {
                    items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(persistence_path), serializer_settings) ?? new List<Item>();
                }
                catch(Exception ex)
                {
                    File.Delete(persistence_path);
                    items = new List<Item>();
                }
            }
            itemNav = new ListNavigator<Item>(Items, 2);
        }
        public void Save()
        {
            
            var list_json = JsonConvert.SerializeObject(Items, serializer_settings);
            if (File.Exists(persistence_path))
                File.Delete(persistence_path);
            File.WriteAllText(persistence_path, list_json);
        }
        public List<Item> IncompleteItems
        {
            get
            {
                var incomplete_items = new List<Item>();
                foreach (var item in Items.Where(i => !((i as Task)?.isCompleted) ?? false)) 
                    incomplete_items.Add(item);
                return incomplete_items;
            }
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
