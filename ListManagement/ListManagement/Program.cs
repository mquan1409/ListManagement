﻿// Quan Pham (FSUID: QMP20A)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    CreateTask(itemService);
                else if (input == 2)
                    DeleteTask(itemService);
                else if (input == 3)
                    EditTask(itemService);
                else if (input == 4)
                    CompleteTask(itemService);
                else if (input == 5)
                    ListNotCompleteTasks(itemService);
                else if (input == 6)
                    ListAllTasks(itemService);
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

        static void CreateTask(ItemService itemService)
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

        static void DeleteTask(ItemService itemService)
        {
            Console.Write("Enter the name of the task you want to delete: ");
            var name_delete = Console.ReadLine();
            var task_found = false;
            for(int i = 0; i < itemService.Items.Count; i++)
            {
                if(itemService.Items[i].Name == name_delete)
                {
                    Console.WriteLine("\nTask " + itemService.Items[i].Name + " Deleted Successfully!\n");
                    itemService.Items.RemoveAt(i);   // !!!
                    task_found = true;
                }
            }
            if (!task_found)
                Console.WriteLine("\nTask Not Found!\n");

            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void EditTask(ItemService itemService)
        {
            Console.Write("Enter the name of the task you want to edit: ");
            var name_edit = Console.ReadLine();
            var task_found = false;
            for(int i = 0; i < itemService.Items.Count; i++)
            {
                if(itemService.Items[i].Name == name_edit)
                {
                    var task_edited = new Task();
                    task_found = true;
                    Console.Write("Enter new Name: ");
                    var name = Console.ReadLine();
                    Console.Write("Enter new Description: ");
                    var description = Console.ReadLine();
                    Console.Write("Enter new Deadline: ");
                    var deadline = Console.ReadLine();
                    task_edited.Name = name;
                    task_edited.Description = description;
                    task_edited.Deadline = deadline;
                    task_edited.isCompleted = false;
                    itemService.Replace(i, task_edited);
                    Console.WriteLine("\nTask Edited Successfully!\n");

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
            var not_complete_count = 0;
            for(int i = 0; i < itemService.Items.Count; i++)
            {
                if (!(itemService.Items[i] as Task)?.isCompleted ?? false)
                {   
                    not_complete_count++;
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Task Name: " + itemService.Items[i].Name);
                    Console.WriteLine("Task Description: " + itemService.Items[i].Description);
                    Console.WriteLine("Task Deadline: " + ((itemService.Items[i] as Task)?.Deadline ?? "No Deadline"));
                    Console.WriteLine();
                }
            }
            if (itemService.Items.Count == 0)
            {
                Console.WriteLine("\nThere is no outstanding (not complete) task in the List\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();

        }
        static void ListAllTasks(ItemService itemService)
        {
            itemService.Items.ForEach(task => {
                PrintItem(task);
            });
            if(itemService.Items.Count == 0)
            {
                Console.WriteLine("\nThere is no task in the List\n");
            }
            Console.WriteLine("---Press any key to continue---");
            Console.ReadLine();
        }

        static void PrintItem(Item task)
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Task Name: " + task.Name);
            Console.WriteLine("Task Description: " + task.Description);
            Console.WriteLine("Task Deadline: " + ((task as Task)?.Deadline ?? "No Deadline"));
            Console.WriteLine();
        }
    }
}
