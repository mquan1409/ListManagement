using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class ToDoDialog : ContentDialog
    {
        ItemService itemService = ItemService.Current;
        public ToDoDialog()
        {
            this.InitializeComponent();
            DataContext = new Task();
        }

        public ToDoDialog(Item selected_item)
        {
            this.InitializeComponent();
            DataContext = selected_item;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = DataContext as Task;
            if(itemService.Items.Any(i => i.Id == item.Id))
            {
                var edited_item = itemService.Items.FirstOrDefault(i => i.Id == item.Id);
                itemService.RemoveAt(itemService.Items.IndexOf(edited_item));
                itemService.Add(edited_item);
            }
            else if (item != null)
            {
                itemService.Add(item);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
