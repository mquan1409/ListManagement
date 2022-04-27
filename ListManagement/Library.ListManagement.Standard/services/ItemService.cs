using Library.ListManagement.helpers;
using Library.ListManagement.Standard.DTO;
using Library.ListManagement.Standard.utilities;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = ListManagement.models.Task;

namespace ListManagement.services
{
    public class ItemService
    {
        private ObservableCollection<ItemDTO> items;
        private string query;
        private ListNavigator<ItemDTO> item_nav;
        private static ItemService instance;
        private string persistence_path;
        private JsonSerializerSettings serializer_settings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All};

        public ObservableCollection<ItemDTO> Items { 
            get 
            {
                var payload = JsonConvert
                    .DeserializeObject<List<ItemDTO>>(new WebRequestHandler()
                    .Get("http://localhost:7020/Item").Result);
                items.Clear();
                payload.ForEach(items.Add);
                return items; 
            } 
        }

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
        public IEnumerable<ItemDTO> FilteredItems { 
            get {
                // var incomplete_items = items.Where(i =>

                //(!ShowComplete && !ShowQuery && !(((i as TaskDTO)?.isCompleted) ?? true))

                //|| ShowComplete);

                // var filtered_items = incomplete_items.Where(i =>
                //     ((ShowQuery &&
                //        ((i.Name.ToUpper() == Query) ||
                //        (i.Description.ToUpper() == Query) ||
                //        ((i as AppointmentDTO)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query) ?? false)))
                //     || !ShowQuery));

                // return filtered_items;
                var filteredItemsStr = new WebRequestHandler().Post("http://localhost:7020/Item/Search", new SearchItemDTO { Query = Query, ShowComplete = ShowComplete, ShowQuery = ShowQuery }).Result;
                var filtered_items = JsonConvert.DeserializeObject<IEnumerable<ItemDTO>>(filteredItemsStr);
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
        public Dictionary<object, ItemDTO> GetPage()
        {
            var page = item_nav.GetCurrentPage();
            if (item_nav.HasNextPage)
            {
                page.Add("N", new ItemDTO { Name = "Next" });
            }
            if (item_nav.HasPreviousPage)
            {
                page.Add("P", new ItemDTO { Name = "Previous" });
            }
            page.Add("E", new ItemDTO { Name = "Exit" });
            return page;
        }
        public Dictionary<object, ItemDTO> FirstPage()
        {
            return item_nav.GoToFirstPage();
        }
        public Dictionary<object, ItemDTO> NextPage()
        {
            return item_nav.GoForward();
        }

        public Dictionary<object, ItemDTO> PreviousPage()
        {
            return item_nav.GoBackward();
        }

        private ItemService()
        {
            items = new ObservableCollection<ItemDTO>();
            ShowComplete = true;
            ShowQuery = false;
            //var str = new WebRequestHandler()
            //    .Get("http://localhost:7020/Item").Result;
            //var payload = JsonConvert
            //    .DeserializeObject<List<ItemDTO>>(str);
            var payload = JsonConvert
                    .DeserializeObject<List<ItemDTO>>(new WebRequestHandler()
                    .Get("http://localhost:7020/Item").Result);
            items.Clear();
            payload.ForEach(items.Add);


            //persistence_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
            //if (File.Exists(persistence_path))
            //{
            //    try
            //    {
            //        items = JsonConvert.DeserializeObject<ObservableCollection<ItemDTO>>(File.ReadAllText(persistence_path), serializer_settings) ?? new ObservableCollection<ItemDTO>();
            //    }
            //    catch(Exception ex)
            //    {
            //        File.Delete(persistence_path);
            //        items = new ObservableCollection<ItemDTO>();
            //    }
            //}
            //item_nav = new ListNavigator<Item>(FilteredItems, 5);
        }
        public void Save()
        {
            
            //var list_json = JsonConvert.SerializeObject(Items, serializer_settings);
            //if (File.Exists(persistence_path))
            //    File.Delete(persistence_path);
            //File.WriteAllText(persistence_path, list_json);

            //Because I have implemented pass-through add, pass-through remove, pass-through edit, ...
            //Every change has been made to the web server
        }
        public async Task<ItemDTO> Add(ItemDTO item_added)
        {
            if(item_added is TaskDTO)
            {
                var taskStr = await new WebRequestHandler().Post("http://localhost:7020/Task/AddOrUpdate", item_added);
                return JsonConvert.DeserializeObject<TaskDTO>(taskStr);
            }
            else
            {
                var appointmentStr = await new WebRequestHandler().Post("http://localhost:7020/Appointment/AddOrUpdate", item_added);
                return JsonConvert.DeserializeObject<AppointmentDTO>(appointmentStr);
            }
        }

        public async Task<ItemDTO> Remove(ItemDTO item_removed)
        {
            if(item_removed is TaskDTO)
            {
                var deletedTaskStr = await new WebRequestHandler().Post("http://localhost:7020/Task/Delete", new DeleteItemDTO { IdToDelete = item_removed.Id });
                return JsonConvert.DeserializeObject<TaskDTO>(deletedTaskStr);
            }
            else
            {
                var deletedAppointmentStr = await new WebRequestHandler().Post("http://localhost:7020/Appointment/Delete", new DeleteItemDTO { IdToDelete = item_removed.Id });
                return JsonConvert.DeserializeObject<AppointmentDTO>(deletedAppointmentStr);
            }
        }
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        public void Replace(int index, ItemDTO item_replaced)
        {
            items[index] = item_replaced;
        }

        public int NextId 
        { 
            get
            {
                if (Items.Any())
                {
                    return Items.Select(item => item.Id).Max() + 1;
                }
                return 1;
            } 
        }
    }
}
