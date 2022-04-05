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
            foreach(var item in itemService.Items)
            {
                this.Add(new ItemViewModel(item));
            }
        }
        public void Remove(ItemViewModel item)
        {
            //itemService.Remove(item.BoundItem);
            //NotifyPropertyChanged("Items");
            Items.Remove(item);
        }
        public void RemoveAt(int index)
        {
            //itemService.RemoveAt(index);
            //NotifyPropertyChanged("Items");
            Items.RemoveAt(index);
        }
        public void Add(ItemViewModel item)
        {
            //itemService.Add(item.BoundItem);
            //NotifyPropertyChanged("Items");
            if (item.Id <= 0)
                item.Id = NextId;
            if(item.IsTask)
                Items.Insert(0,item);
            else
                Items.Insert(Items.Count,item);
        }
        public void Save()
        {
            itemService.Items.Clear();
            foreach (var item in Items)
                itemService.Items.Add(item.BoundItem);
            itemService.Save();
        }
        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return items;
            }
        }

        public void SearchItems()
        {
            itemService.Items.Clear();
            foreach (var item in Items)
                itemService.Items.Add(item.BoundItem);
            itemService.ShowQuery = true;
            var filtered_items = new ObservableCollection<ItemViewModel>(itemService.FilteredItems.Select(i => new ItemViewModel(i)));
            Items.Clear();
            foreach(var item in filtered_items)
                Items.Add(item);
        }
        public void Refresh()
        {
            Items.Clear();
            foreach (var item in itemService.Items)
            {
                this.Add(new ItemViewModel(item));
            }
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
