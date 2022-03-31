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

        public ObservableCollection<ItemViewModel> Items { get { return itemService.Items; } }

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
        }
    }
}
