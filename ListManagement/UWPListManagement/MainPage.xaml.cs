using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPListManagement.Dialogs;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPListManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }
        private async void AddItem(object sender, RoutedEventArgs e)
        {
            var dialog = new TaskDialog();
            await dialog.ShowAsync();
        }

        private async void EditItem(object sender, RoutedEventArgs e)
        {
            var dialog = new TaskDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).DeleteItem();
        }

        private void SaveItem(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SaveItem();
        }
    }
}
