using Library.ListManagement.helpers;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = ListManagement.models.Task;

namespace ListManagement.services
{
    public class ItemService
    {
        private List<Item> items;
        private string query;
        private ListNavigator<Item> item_nav;
        private static ItemService instance;
        private string persistence_path;
        private JsonSerializerSettings serializer_settings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All};

        public List<Item> Items { get { return items; } }

        public bool ShowComplete {get; set; }
        public bool ShowQuery { get; set; }
        public string Query {
            get
            {
                return query;
            } 
            set
            {
                query = value.ToUpper();
            } 
        }
        public IEnumerable<Item> FilteredItems { 
            get {
                var incomplete_items =  items.Where(i => 
                
                (!ShowComplete && !ShowQuery && !(((i as Task)?.isCompleted) ?? true))

                || ShowComplete);

                var filtered_items = incomplete_items.Where(i =>
                    ((ShowQuery &&
                       ((i.Name.ToUpper() == Query) ||
                       (i.Description.ToUpper() == Query) ||
                       ((i as Appointment)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query) ?? false)))
                    || !ShowQuery));

                return filtered_items;


                
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
        public Dictionary<object, Item> FirstPage()
        {
            return item_nav.GoToFirstPage();
        }
        public Dictionary<object, Item> NextPage()
        {
            return item_nav.GoForward();
        }

        public Dictionary<object, Item> PreviousPage()
        {
            return item_nav.GoBackward();
        }

        private ItemService()
        {
            items = new List<Item>();
            ShowComplete = true;
            ShowQuery = false;
            
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
            item_nav = new ListNavigator<Item>(FilteredItems, 5);
        }
        public void Save()
        {
            
            var list_json = JsonConvert.SerializeObject(Items, serializer_settings);
            if (File.Exists(persistence_path))
                File.Delete(persistence_path);
            File.WriteAllText(persistence_path, list_json);
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
