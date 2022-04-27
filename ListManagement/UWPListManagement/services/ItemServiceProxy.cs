using ListManagement.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWPListManagement.ViewModels;

namespace UWPListManagement.services
{
    public class ItemServiceProxy : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private ObservableCollection<ItemViewModel> items;
        private static ItemServiceProxy instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        //private void NotifyCollectionChanged([CallerMemberName] String propertyName = "")
        //{
        //    CollectionChanged?.Invoke(this, new CollectionChangeEventArgs(propertyName));
        //}
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //private static ItemServiceProxy instance;
        private ItemService itemService;
        //private ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>(ItemService.Current.Items.Select(i => new ItemViewModel(i)));

        //public static ItemServiceProxy Current
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new ItemServiceProxy();
        //        return instance;
        //    }
        //}
        public static ItemServiceProxy Current
        {
            get
            {
                if (instance == null)
                    instance = new ItemServiceProxy();
                return instance;
            }
        }
        private ItemServiceProxy()
        {
            itemService = ItemService.Current;
            items = new ObservableCollection<ItemViewModel>();
            //foreach(var item in itemService.Items)
            //{
            //    await this.Add(new ItemViewModel(item));
            //}
            //await Refresh();
        }
        public async Task<ItemViewModel> Remove(ItemViewModel item)
        {
            //itemService.Remove(item.BoundItem);
            //NotifyPropertyChanged("Items");
            var item_DTO = await itemService.Remove(item.BoundItem);
            await Refresh();
            return new ItemViewModel(item_DTO);
        }
        public void RemoveAt(int index)
        {
            //itemService.RemoveAt(index);
            //NotifyPropertyChanged("Items");
        }
        public async Task<ItemViewModel> Add(ItemViewModel item)
        {
            //itemService.Add(item.BoundItem);
            //NotifyPropertyChanged("Items");
            //if (item.Id <= 0)
            //    item.Id = NextId;
            var item_DTO = await itemService.Add(item.BoundItem);
            await Refresh();
            //if (item.IsTask)
            //    Items.Insert(0, item);
            //else
            //    Items.Insert(Items.Count, item);
            return new ItemViewModel(item_DTO);
        }
        public async void Save()
        {
            itemService.Items.Clear();
            foreach (var item in Items)
                await itemService.Add(item.BoundItem);
            itemService.Save();
        }
        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                items = Refresh().Result;
                return items;
            }
        }

        public void SearchItems()
        {
            //itemService.Items.Clear();
            //foreach (var item in Items)
            //    itemService.Items.Add(item.BoundItem);
            //itemService.ShowQuery = true;
            //var filtered_items = new ObservableCollection<ItemViewModel>(itemService.FilteredItems.Select(i => new ItemViewModel(i)));
            //Items.Clear();
            //foreach(var item in filtered_items)
            //    Items.Add(item);
            itemService.ShowQuery = true;
            itemService.ShowComplete = true;
            itemService.Query = Query;
        }
        public async Task<ObservableCollection<ItemViewModel>> Refresh()
        {
            items.Clear();
            if (itemService.ShowQuery)
            {
                //itemService.Items.Clear();
                foreach (var i in itemService.FilteredItems)
                {
                    var item = new ItemViewModel(i);
                    if (item.IsTask)
                        items.Insert(0, item);
                    else
                        items.Insert(items.Count, item);
                }
                itemService.ShowQuery = false;
            }
            else
            {
                foreach (var i in itemService.Items)
                {
                    var item = new ItemViewModel(i);
                    if (item.IsTask)
                        items.Insert(0, item);
                    else
                        items.Insert(items.Count, item);
                }
            }
            return items;
        }
        public string Query
        {
            get
                {
                return itemService.Query;
            }
            set
            { 
                itemService.Query = value; 
            }
        }
        private int NextId
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
