using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPListManagement.services;
using UWPListManagement.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class TaskDialog : ContentDialog, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ItemServiceProxy itemService = ItemServiceProxy.Current;
        public TaskDialog()
        {
            this.InitializeComponent();
            DataContext = new ItemViewModel(new TaskDTO());
            (DataContext as ItemViewModel).IsEditing = false;
        }

        public TaskDialog(ItemViewModel selected_item)
        {
            this.InitializeComponent();
            DataContext = selected_item;
            (DataContext as ItemViewModel).IsEditing = true;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = (DataContext as ItemViewModel);

            if (itemService.Items.Any(i => i.Id == item.Id))
            {
                var edited_item = itemService.Items.FirstOrDefault(i => i.Id == item.Id);
                itemService.RemoveAt(itemService.Items.IndexOf(edited_item));
                itemService.Add(item);
            }
            else if (item != null)
            {
                itemService.Add(item);
                //NotifyPropertyChanged("Items");
                
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
