// Quan Pham (FSUID: QMP20A)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListManagement.models;
using ListManagement.models;
using ListManagement.services;

using Task = ListManagement.models.Task;

namespace ListManagement
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var itemService = ItemService.Current;
            Console.WriteLine("Welcome to the List Management App\n\n");
            var input = 0;
            do
            {
                PrintMenu();
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nInvalid option! Please enter an integer (1 -> 7).\n");
                    Console.WriteLine("---Press any key to continue---");
                    Console.ReadLine();
                    input = 0;
                }
                if (input == 1)
                    CreateItem(itemService);
                else if (input == 2)
                    DeleteItem(itemService);
                else if (input == 3)
                    EditItem(itemService);
                else if (input == 4)
                    CompleteTask(itemService);
                else if (input == 5)
                    ListNotCompleteTasks(itemService);
                else if (input == 6)
                    ListAllTasks(itemService);
                else if (input == 7)
                    SearchItems(itemService);
                else if (input == 8)
                {
                    itemService.Save();
                    Console.WriteLine("Data saved successfully!");
                    Console.WriteLine("---Press any key to continue---");
                    Console.ReadLine();
                }
                else if (input == 9)
                    Console.WriteLine("Thank you for using the List Management App!");

            }
            while (input != 9);
                
            Console.ReadLine();
            
        }

        static void PrintMenu()
        {
            Console.WriteLine("\nChoose one of the following options:\n");
            Console.WriteLine("1. Create item");
            Console.WriteLine("2. Delete item(s)");
            Console.WriteLine("3. Edit an existing item");
            Console.WriteLine("4. Complete a task");
            Console.WriteLine("5. List all outstanding (not complete) tasks");
            Console.WriteLine("6. List all items");
            Console.WriteLine("7. Search Items");
            Console.WriteLine("8. Save data");
            Console.WriteLine("9. Exit");
            Console.Write("\nYour menu option (Please enter number 1 -> 9): ");
        }

        static void CreateItem(ItemService itemService)
        {
            Console.WriteLine("Do you want to create (t)ask or (a)ppointment? ");
            var item_type = Console.ReadLine();
            if (item_type == "t")
            {
                var task = new Task();
                Console.Write("Enter Task Name: ");
                var name = Console.ReadLine();
                Console.Write("Enter Task Description: ");
                var description = Console.ReadLine();
                Console.Write("Enter Task Deadline: ");
                var deadline = Console.ReadLine();
                task.Name = name;
                task.Description = description;
                task.Deadline = deadline;
                task.isCompleted = false;
                itemService.Add(task);
                Console.WriteLine("\nTask Created Successfully!");
                Console.WriteLine("---Press any key to continue---");
                Console.ReadLine();
            }
            else if(item_type == "a")
            {
                var appointment = new Appointment();
                Console.Write("Enter Appointment Name: ");
                appointment.Name = Console.ReadLine();
                Console.Write("Enter Appointment Description: ");
                appointment.Description = Console.ReadLine();
                Console.Write("Enter Start Date of the Appointment (mm/dd/yyyy): ");
                appointment.Start = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter End Date of the Appointment (mm/dd/yyyy): ");
                appointment.End = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Attendees of the Appointment (Type names seperated by comma): ");
                foreach(var name in Console.ReadLine().Split(", "))
                {
                    appointment.Attendees.Add(name);
                };
                itemService.Add(appointment);
            }
        }

        static void DeleteItem(ItemService itemService)
        {
            Console.Write("Do you want to delete (a)ll items or (o)ne item? ");
            var option = Console.ReadLine();
            if (option == "a")
            {
                itemService.Items.Clear();
                Console.WriteLine("Delete all items successfully!");
            }
            else if (option == "o")
            {
                Console.Write("Enter the name of the task you want to delete: ");
                var name_delete = Console.ReadLine();
                var task_found = false;
                for (int i = 0; i < itemService.Items.Count; i++)
                {
                    if (itemService.Items[i].Name == name_delete)
                    {
                        Console.WriteLine("\nTask " + itemService.Items[i].Name + " Deleted Successfully!\n");
                        itemService.Items.RemoveAt(i);   // !!!
                        task_found = true;
                    }
                }
                if (!task_found)
                    Console.WriteLine("\nTask Not Found!\n");

            }

            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void EditItem(ItemService itemService)
        {
            Console.Write("Enter the name of the task you want to edit: ");
            var name_edit = Console.ReadLine();
            var task_found = false;
            for(int i = 0; i < itemService.Items.Count; i++)
            {
                if(itemService.Items[i].Name == name_edit)
                {
                    task_found = true;
                    if(itemService.Items[i] is Task)
                    {
                        var item_edited = new Task();
                        Console.Write("Enter new Name: ");
                        var name = Console.ReadLine();
                        Console.Write("Enter new Description: ");
                        var description = Console.ReadLine();
                        Console.Write("Enter new Deadline: ");
                        var deadline = Console.ReadLine();
                        item_edited.Name = name;
                        item_edited.Description = description;
                        item_edited.Deadline = deadline;
                        item_edited.isCompleted = false;
                        itemService.Replace(i, item_edited);
                        Console.WriteLine("\nTask Edited Successfully!\n");
                    }
                    if(itemService.Items[i] is Appointment)
                    {
                        var item_edited = new Appointment();
                        Console.Write("Enter new Name: ");
                        item_edited.Name = Console.ReadLine();
                        Console.Write("Enter new Description: ");
                        item_edited.Description = Console.ReadLine();
                        Console.Write("Enter new Start Date of the Appointment (mm/dd/yyyy): ");
                        item_edited.Start = DateTime.Parse(Console.ReadLine());
                        Console.Write("Enter new End Date of the Appointment (mm/dd/yyyy): ");
                        item_edited.End = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Enter new Attendees of the Appointment (Type names seperated by comma): ");
                        foreach (var attendee in Console.ReadLine().Split(", "))
                        {
                            item_edited.Attendees.Add(attendee);
                        };
                        itemService.Replace(i, item_edited);
                    }


                }
            }
            if (!task_found)
                Console.WriteLine("\nTask Not Found!\n");

            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void CompleteTask(ItemService itemService)
        {
            Console.Write("Enter the name of the task you want to complete: ");
            var name_complete = Console.ReadLine();
            var task_found = false;
            for(int i = 0; i < itemService.Items.Count; i++)
            {
                if (itemService.Items[i].Name == name_complete)
                {
                    task_found = true;
                    var task_completed = itemService.Items[i] as Task;
                    if (task_completed != null)
                        task_completed.isCompleted = true;
                    Console.WriteLine("\nTask " + itemService.Items[i].Name + " has been marked Completed!\n");
                }
            }
            if (!task_found)
            {
                Console.WriteLine("\nTask Not Found!\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void ListNotCompleteTasks(ItemService itemService)
        {
            itemService.ShowComplete = false;
            itemService.ShowQuery = false;
            if (!StartPaging(itemService))
            {
                Console.WriteLine("\nThere is no outstanding (not complete) task in the List\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();

        }
        static void ListAllTasks(ItemService itemService)
        {
            itemService.ShowComplete = true;
            if(!StartPaging(itemService))
            {
                Console.WriteLine("\nThere is no task in the List\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }
        public static void SearchItems(ItemService itemService)
        {
            Console.Write("Enter search string: ");
            itemService.ShowQuery = true;
            itemService.ShowComplete = true;
            itemService.Query = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(itemService.Query) || string.IsNullOrEmpty(itemService.Query))
                Console.WriteLine("\nThere is no query!\n");

            else if (!StartPaging(itemService))
            {
                Console.WriteLine("\nThere is no task in the List\n");
            }
            itemService.Query = "";
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        private static void PrintItem(Item item)
        {
            if(item.Name == "Next")
                Console.WriteLine("\nClick \'N\' to go to next page.");
            else if(item.Name == "Previous")
                Console.WriteLine("\nClick \'P\' to go to previous page.");
            else if(item.Name == "Exit")
                Console.WriteLine("\nClick \'E\' to exit.");
            else
            {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Task Name: " + item.Name);
                Console.WriteLine("Task Description: " + item.Description);
                if(item is Task)
                {
                    Console.WriteLine("Task Deadline: " + ((item as Task)?.Deadline ?? "No Deadline"));
                    Console.WriteLine();
                }
                else if(item is Appointment)
                {
                    Console.WriteLine("Appointment Start: " + (item as Appointment)?.Start.ToString("d") ?? "No Start Date");
                    Console.WriteLine("Appointment End: " + (item as Appointment)?.End.ToString("d") ?? "No Start Date");
                    Console.Write("Attendees: ");
                    var attendees = (item as Appointment)?.Attendees;
                    if (attendees != null)
                        for(int i = 0;i < attendees.Count; i++)
                        {
                            Console.Write(attendees[i]);
                            if (i != (attendees.Count - 1))
                                Console.Write(", ");
                        }
                    Console.WriteLine();
                }
            }
        }
        private static bool StartPaging(ItemService itemService)
        {
            var user_selection = String.Empty;
            while ((user_selection.ToUpper() != "E") && (itemService.FilteredItems.Count() != 0))
            {
                try
                {
                    foreach (var item in itemService.GetPage())
                    {
                        PrintItem(item.Value);
                    }
                    user_selection = Console.ReadLine();

                    if (user_selection.ToUpper() == "N")
                    {
                        itemService.NextPage();
                    }
                    if (user_selection.ToUpper() == "P")
                    {
                        itemService.PreviousPage();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    user_selection = "E";
                    itemService.FirstPage();
                }
                
            }
            if (itemService.FilteredItems.Count() == 0)
                return false;
            return true;
        }
    }
}
