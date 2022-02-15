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
        private List<Item> incomplete_items;
        private List<Item> filtered_items;
        private ListNavigator<Item> item_nav;
        private ListNavigator<Item> incomplete_item_nav;
        private ListNavigator<Item> filtered_item_nav;
        private static ItemService? instance;
        private string persistence_path;
        private JsonSerializerSettings serializer_settings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All};

        public List<Item> Items { get { return items; } }

        public bool ShowComplete { get; set; }
        public IEnumerable<Item> FilteredItems { 
            get {
                return items.Where(i => ((!ShowComplete && !(((i as Task)?.isCompleted) ?? true)) || ShowComplete));
            } 
        }

        public static ItemService Current { 
            get { 
                if(instance == null)
                    instance = new ItemService();
                return instance; 
            } 
        }
        public Dictionary<object, Item> GetPage()
        {
            //ListNavigator<Item> nav = new ListNavigator<Item>(Items, 2);
            //if (type == "all")
            //    nav = item_nav;
            //if (type == "incomplete")
            //    nav = incomplete_item_nav;
            var page = item_nav.GetCurrentPage();
            if (item_nav.HasNextPage)
            {
                page.Add("N", new Item { Name = "Next" });
            }
            if (item_nav.HasPreviousPage)
            {
                page.Add("P", new Item { Name = "Previous" });
            }
            page.Add("E", new Item { Name = "Exit" });
            return page;
        }
        public Dictionary<object, Item> NextPage()
        {
            //ListNavigator<Item> nav = new ListNavigator<Item>(Items, 2);
            //if (type == "all")
            //    nav = item_nav;
            //if (type == "incomplete")
            //    nav = incomplete_item_nav;
            return item_nav.GoForward();
        }

        public Dictionary<object, Item> PreviousPage()
        {
            //ListNavigator<Item> nav = new ListNavigator<Item>(Items, 2);
            //if (type == "all")
            //    nav = item_nav;
            //if (type == "incomplete")
            //    nav = incomplete_item_nav;
            return item_nav.GoBackward();
        }

        private ItemService()
        {
            items = new List<Item>();
            incomplete_items = new List<Item>();
            filtered_items = new List<Item>();
            ShowComplete = true;
            
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
            item_nav = new ListNavigator<Item>(FilteredItems, 2);
            //incomplete_item_nav = new ListNavigator<Item>(incomplete_items, 2);
            //filtered_item_nav = new ListNavigator<Item>(filtered_items, 2);
        }
        public void Save()
        {
            
            var list_json = JsonConvert.SerializeObject(Items, serializer_settings);
            if (File.Exists(persistence_path))
                File.Delete(persistence_path);
            File.WriteAllText(persistence_path, list_json);
        }
        public void UpdateIncompleteItemsList()  // have to make this function run right before print all incomplete tasks
        {
            var incomplete_ienumerable = Items.Where(i => !(((i as Task)?.isCompleted) ?? true));
            //foreach(var item in incomplete_ienumerable)
                //Console.WriteLine(item.Name);
            incomplete_items.Clear();
            //for (int i = 0; i < incomplete_items.Count; i++)
              //  incomplete_items.RemoveAt(i);
            //Console.WriteLine(incomplete_items.Count);
            foreach (var item in incomplete_ienumerable) 
                incomplete_items.Add(item);
            //foreach(var item in incomplete_items)
            //    Console.WriteLine(item.Name);
        }
        public List<Item> Search(string search_string)
        {
            filtered_items.Clear();
            var filtered_items_ienumerable = Items.Where((i) =>
            {
                if (i.Name == search_string)
                    return true;
                else if (i.Description == search_string)
                    return true;
                else if ((i as Appointment)?.Attendees?.Contains(search_string) ?? false)
                    return true;
                else
                    return false;
            });
            foreach(var item in filtered_items_ienumerable)
                filtered_items.Add(item);
            return filtered_items;
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
