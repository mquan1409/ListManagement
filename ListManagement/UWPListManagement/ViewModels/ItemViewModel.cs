using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Task = ListManagement.models.Task;

namespace UWPListManagement.ViewModels
{
    public class ItemViewModel
    {
        public Task BoundTask { get; set; }
        public Appointment BoundAppointment { get; set; }
        public Item BoundItem { 
            get
            { 
                if(IsTask)
                    return BoundTask;
                else
                    return BoundAppointment;
            } 
        }
        public bool IsTask { 
            get
            {
                return BoundTask != null;
            }
        }
        public Visibility IsCompleteVisibility
        {
            get
            {
                return IsTask ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public bool IsCompleted
        {
            get
            {
                if (IsTask)
                    return BoundTask.isCompleted;
                else
                    return false;
            }
            set
            {
                if (IsTask)
                    BoundTask.isCompleted = value;
            }
        }
        public string Name { 
            get 
            { 
                return BoundItem?.Name ?? String.Empty;
            }
            set
            {
                if(BoundItem != null)
                    BoundItem.Name = value;
            }
        }
        public string Description { 
            get 
            { 
                return BoundItem?.Description ?? String.Empty;
            } 
            set
            {
                if(BoundItem != null)
                    BoundItem.Description = value;
            }
        }
        public ItemViewModel(Item item)
        {
            if(item is Task)
            {
                BoundTask = item as Task;
                BoundAppointment = null;
                IsCompleted = BoundTask.isCompleted;
            }
            else if(item is Appointment)
            {
                BoundTask = null;
                BoundAppointment = item as Appointment;
                IsCompleted = false;
            }

        }
    }
}
