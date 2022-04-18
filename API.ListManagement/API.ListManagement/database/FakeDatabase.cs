using ListManagement.models;
using Task = ListManagement.models.Task;

namespace API.ListManagement.database
{
    public class FakeDatabase
    {
        public static List<int> Test = new List<int> { 1,2,3,4,5};
        public static List<Item> Tasks = new List<Item>
        {
            new Task{Name = "aaa", Description = "aaa", isCompleted = false, Id = 1 },
        };
        public static List<Item> Appointments = new List<Item>
        {
            new Appointment{Name = "bbb", Description="bbb", Start=DateTime.Today, Id = 2},
        };
    }
}
