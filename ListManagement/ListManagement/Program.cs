// Quan Pham (FSUID: QMP20A)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Task = ListManagement.models.Task;

namespace ListManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            var TaskList = new List<Task>();
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
                    CreateTask(TaskList);
                else if (input == 2)
                    DeleteTask(TaskList);
                else if (input == 3)
                    EditTask(TaskList);
                else if (input == 4)
                    CompleteTask(TaskList);
                else if (input == 5)
                    ListNotCompleteTasks(TaskList);
                else if (input == 6)
                    ListAllTasks(TaskList);
                else if (input == 7)
                    Console.WriteLine("Thank you for using the List Management App!");

            }
            while (input != 7);
                
            Console.ReadLine();
            
        }

        static void PrintMenu()
        {
            Console.WriteLine("\nChoose one of the following options:\n");
            Console.WriteLine("1. Create a new task");
            Console.WriteLine("2. Delete an existing task");
            Console.WriteLine("3. Edit an existing task");
            Console.WriteLine("4. Complete a task");
            Console.WriteLine("5. List all outstanding (not complete) tasks");
            Console.WriteLine("6. List all tasks");
            Console.WriteLine("7. Exit");
            Console.Write("\nYour menu option (Please enter number 1 -> 7): ");
        }

        static void CreateTask(List<Task> TaskList)
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
            TaskList.Add(task);
            Console.WriteLine("\nTask Created Successfully!");
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void DeleteTask(List<Task> TaskList)
        {
            Console.Write("Enter the name of the task you want to delete: ");
            var name_delete = Console.ReadLine();
            var task_found = false;
            for(int i = 0; i < TaskList.Count; i++)
            {
                if(TaskList[i].Name == name_delete)
                {
                    Console.WriteLine("\nTask " + TaskList[i].Name + " Deleted Successfully!\n");
                    TaskList.RemoveAt(i);
                    task_found = true;
                }
            }
            if (!task_found)
                Console.WriteLine("\nTask Not Found!\n");

            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void EditTask(List<Task> TaskList)
        {
            Console.Write("Enter the name of the task you want to edit: ");
            var name_edit = Console.ReadLine();
            var task_found = false;
            for(int i = 0; i < TaskList.Count; i++)
            {
                if(TaskList[i].Name == name_edit)
                {
                    task_found = true;
                    Console.Write("Enter new Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Enter new Description: ");
                    var description = Console.ReadLine();
                    Console.Write("Enter new Deadline: ");
                    var deadline = Console.ReadLine();
                    TaskList[i].Name = name;
                    TaskList[i].Description = description;
                    TaskList[i].Deadline = deadline;
                    Console.WriteLine("\nTask Edited Successfully!\n");

                }
            }
            if (!task_found)
                Console.WriteLine("\nTask Not Found!\n");

            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void CompleteTask(List<Task> TaskList)
        {
            Console.Write("Enter the name of the task you want to complete: ");
            var name_complete = Console.ReadLine();
            var task_found = false;
            for(int i = 0; i < TaskList.Count; i++)
            {
                if (TaskList[i].Name == name_complete)
                {
                    task_found = true;
                    TaskList[i].isCompleted = true;
                    Console.WriteLine("\nTask " + TaskList[i].Name + " has been marked Completed!\n");
                }
            }
            if (!task_found)
            {
                Console.WriteLine("\nTask Not Found!\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void ListNotCompleteTasks(List<Task> TaskList)
        {
            var not_complete_count = 0;
            for(int i = 0; i < TaskList.Count; i++)
            {
                if (TaskList[i].isCompleted != true)
                {   
                    not_complete_count++;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Task Name: " + TaskList[i].Name);
                    Console.WriteLine("Task Description: " + TaskList[i].Description);
                    Console.WriteLine("Task Deadline: " + TaskList[i].Deadline);
                    Console.WriteLine();
                }
            }
            if (TaskList.Count == 0)
            {
                Console.WriteLine("\nThere is no outstanding (not complete) task in the List\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();

        }
        static void ListAllTasks(List<Task> TaskList)
        {
            TaskList.ForEach(task => {
                Console.WriteLine("----------------------------");
                Console.WriteLine("Task Name: " + task.Name);
                Console.WriteLine("Task Description: " + task.Description);
                Console.WriteLine("Task Deadline: " + task.Deadline);
                Console.WriteLine();
            });
            if(TaskList.Count == 0)
            {
                Console.WriteLine("\nThere is no task in the List\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }
    }
}
