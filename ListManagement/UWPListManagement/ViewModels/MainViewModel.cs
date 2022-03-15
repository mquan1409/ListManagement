using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel
    {
        ItemService itemService = ItemService.Current;
        public ObservableCollection<Item> Items { get { return itemService.Items; } }

        public MainViewModel()
        {
        }

        public Item SelectedItem { get; set; }
    }
}
