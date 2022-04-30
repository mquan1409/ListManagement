using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Task = ListManagement.models.Task;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _appointmentRoot;
        private string _taskRoot;
        private static Filebase _instance;


        public static Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = "C:\\temp";
            _appointmentRoot = $"{_root}\\Appointments";
            _taskRoot = $"{_root}\\Tasks";

            if(!Directory.Exists(_root))
            {
                Directory.CreateDirectory(_root);
                if(!Directory.Exists(_appointmentRoot))
                {
                    Directory.CreateDirectory(_appointmentRoot);
                }

                if(!Directory.Exists(_taskRoot))
                {
                    Directory.CreateDirectory(_taskRoot);
                }
            }


        }

        public Item AddOrUpdate(Item item)
        {

            //go to the right place
            string path;
            if (item is Task)
            {
                path = $"{_taskRoot}\\{item.Id}.json";
            } else if(item is Appointment)
            {
                path = $"{_appointmentRoot}\\{item.Id}.json";
            } else
            {
                throw new Exception("Polymorphic binding failed!!!!");
            }

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(item));

            //return the item, which now has an id
            return item;
        }

        public List<Task> Tasks
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }
                var root = new DirectoryInfo(_taskRoot);
                var _todos = new List<Task>();
                foreach(var todoFile in root.GetFiles())
                {
                    var task = JsonConvert.DeserializeObject<Task>(File.ReadAllText(todoFile.FullName));
                    _todos.Add(task);
                }
                return _todos;
            }
        }

        public Task GetById(int id)
        {
            //return ToDos?.FirstOrDefault(t => t.Id == id) ?? new ToDo();

            var fileName = $"{_taskRoot}\\{id}.json";
            return JsonConvert.DeserializeObject<Task>(File.ReadAllText(fileName)) ?? null;

        }

        public List<Appointment> Appointments
        {
            get
            {
                var root = new DirectoryInfo(_appointmentRoot);
                var _apps = new List<Appointment>();
                foreach (var appFile in root.GetFiles())
                {
                    var app = JsonConvert.DeserializeObject<Appointment>(File.ReadAllText(appFile.FullName));
                    _apps.Add(app);
                }
                return _apps;
            }
        }

        public bool Delete(Item item)
        {
            if (item != null)
            {
                //go to the right place
                string path;
                if (item is Task)
                {
                    path = $"{_taskRoot}\\{item.Id}.json";
                }
                else if (item is Appointment)
                {
                    path = $"{_appointmentRoot}\\{item.Id}.json";
                }
                else
                {
                    throw new Exception("Polymorphic binding failed!!!!");
                }

                //if the item has been previously persisted
                if (File.Exists(path))
                {
                    //blow it up
                    File.Delete(path);
                }
            }
            return true;
        }
    }

}
