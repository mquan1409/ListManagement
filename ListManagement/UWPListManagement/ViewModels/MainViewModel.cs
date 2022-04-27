using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWPListManagement.Dialogs;
using UWPListManagement.services;
using Windows.UI.Xaml;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        ItemServiceProxy itemService = ItemServiceProxy.Current;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private ObservableCollection<ItemViewModel> sortedItems;
        public ObservableCollection<ItemViewModel> SortedItems
        {
            get { return sortedItems; }
            set { sortedItems = value; }
        }

        public ObservableCollection<ItemViewModel> Items 
        { 
            get {
                if (sortChecked || showCompleteChecked)
                {
                    sortedItems = new ObservableCollection<ItemViewModel>(itemService.Items);
                    if (sortChecked)
                    {
                        sortedItems = new ObservableCollection<ItemViewModel>(itemService.Items.OrderBy(i => i.Priority));
                    }
                    if (showCompleteChecked)
                    {
                        sortedItems = new ObservableCollection<ItemViewModel>(sortedItems.Where(i => i.IsCompleted));
                    }
                    return sortedItems;
                }
                

                return itemService.Items; 
            } 
        }

        public MainViewModel()
        {
        }

        public ItemViewModel SelectedItem { get; set; }

        public async void MoreDetails()
        {
            var dialog = new DetailDialog(SelectedItem);
            await dialog.ShowAsync();
        }

        public void DeleteItem()
        {
            itemService.Remove(SelectedItem);
        }

        public void SaveItem()
        {
            itemService.Save();
        }

        public void SearchItems()
        {
            itemService.SearchItems();
            NotifyPropertyChanged("Items");
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
        public void Refresh() 
        {
            itemService.Refresh();
            Query = "";
            SortChecked = false;
            NotifyPropertyChanged("Query");
        }
        private bool sortChecked = false;
        public bool SortChecked
        {
            get
            { 
                return sortChecked; 
            }
            set
            {
                sortChecked = value;
                NotifyPropertyChanged("Items");
            }
        }
        private bool showCompleteChecked = false;
        public bool ShowCompleteChecked
        {
            get
            {
                return showCompleteChecked;
            }
            set
            {
                showCompleteChecked = value;
                NotifyPropertyChanged("Items");
            }
        }
    }
}
