using ListManagement.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Task = ListManagement.models.Task;

namespace UWPListManagement.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool IsEditing { get; set; }
        private bool lowPriority = true;
        private bool medPriority = false;
        private bool highPriority = false;
        public bool LowPriority { get { return lowPriority; } set { lowPriority = value; Priority = 1; NotifyPropertyChanged("LowPriority"); } }
        public bool MedPriority { get { return medPriority; } set { medPriority = value; Priority = 2; NotifyPropertyChanged("MedPriority"); } }
        public bool HighPriority { get { return highPriority; } set { highPriority = value; Priority = 3; NotifyPropertyChanged("HighPriority"); } }

        private bool isRadioButtonTaskChecked = true;
        public bool RadioButtonTaskChecked { 
            get { return isRadioButtonTaskChecked; } 
            set {
                if (value == true)
                {
                    isRadioButtonTaskChecked = value;
                    BoundTask = new Task();
                    BoundAppointment = null;
                    BoundDeadline = DateTime.Now;
                }
                else
                {
                    isRadioButtonTaskChecked = value;
                    BoundTask = null;
                    BoundAppointment = new Appointment();
                    BoundStartDate = DateTime.Now;
                    BoundEndDate = DateTime.Now;
                }
                NotifyPropertyChanged("IsTaskCreating");
                NotifyPropertyChanged("IsAppointmentCreating");
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Description");
                NotifyPropertyChanged("Attendees");
            }
        }
        private DateTimeOffset boundDeadline;
        public DateTimeOffset BoundDeadline
        {
            get { 
                return boundDeadline;
            }
            set 
            {
                boundDeadline = value;
                if (IsTask)
                {
                    BoundTask.Deadline = boundDeadline.Date;
                }
                NotifyPropertyChanged("BoundDeadline");
            }
        }
        private DateTimeOffset boundStartDate;
        public DateTimeOffset BoundStartDate
        {
            get
            {
                return boundStartDate;
            }
            set
            {
                boundStartDate = value;
                if (!IsTask)
                {
                    BoundAppointment.Start = boundStartDate.Date;
                }
                NotifyPropertyChanged("BoundStartDate");
            }
        }
        private DateTimeOffset boundEndDate;
        public DateTimeOffset BoundEndDate
        {
            get
            {
                return boundEndDate;
            }
            set
            {
                boundEndDate = value;
                if (!IsTask)
                {
                    BoundAppointment.End = boundEndDate.Date;
                }
                NotifyPropertyChanged("BoundEndDate");
            }
        }

        public Visibility IsTaskCreating
        {
            get
            {
                return IsTask ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility IsAppointmentCreating
        {
            get
            {
                return (!IsTask) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Task BoundTask { get; set; }
        public Appointment BoundAppointment { get; set; }
        public Item BoundItem 
        { 
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
        public Visibility IsTaskVisibility
        {
            get
            {
                return IsTask ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility IsAppointmentVisibility
        {
            get
            {
                return (!IsTask) ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility IsEditingVisibility
        {
            get
            {
                return (!IsEditing) ? Visibility.Visible : Visibility.Collapsed;
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
        public string Attendees
        {
            get
            {
                if ((!IsTask) && (BoundAppointment.Attendees.Count != 0))
                {
                    string attendees_str = System.String.Empty;
                    foreach (string name in BoundAppointment.Attendees)
                    {
                        attendees_str += name;
                        attendees_str += ", ";
                    }
                    attendees_str = attendees_str.Remove(attendees_str.Length - 1);
                    attendees_str = attendees_str.Remove(attendees_str.Length - 1);
                    return attendees_str;
                }
                else
                    return "";
            }
            set
            {
                BoundAppointment.Attendees.Clear();
                if(!IsTask)
                    foreach (var name in value.Split(", "))
                    {
                        BoundAppointment.Attendees.Add(name);
                    };
            }
        }
        public string Name 
        { 
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
        public string Description 
        { 
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
        public int Id
        {
            get { return BoundItem?.Id ?? -1;}
            set { BoundItem.Id = value;}
        }
        public int Priority
        {
            get { return BoundItem?.Priority ?? -1;}
            set { BoundItem.Priority = value;}
        }
        public ItemViewModel(Item item)
        {
            if(item is Task)
            {
                BoundTask = item as Task;
                BoundAppointment = null;
                IsCompleted = BoundTask.isCompleted;
                BoundDeadline = DateTime.Now;
            }
            else if(item is Appointment)
            {
                BoundTask = null;
                BoundAppointment = item as Appointment;
                IsCompleted = false;
                BoundStartDate = DateTime.Now;
                BoundEndDate = DateTime.Now;
            }

        }
        public override string ToString()
        {
            return $"{Id} {Name} {Description}";
        }
    }
}
