using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.Application.Repository
{
    internal class TaskRepository
    {
        private List<Task> _tasks;

        public TaskRepository() { 

            // prepair the info from the file
            this.loadFromFiles();
        

        }


        public List<Task> GetAll()
        {
            return _tasks;
        }
        public Task Get(int index)
        {
            return _tasks[index];
        }
        public void Add(Task task)
        {
            _tasks.Add(task);
        }
        public void Remove(int index)
        {
            _tasks.RemoveAt(index);
        }
        public void Update(Task task)
        {

        }




        private void loadFromFiles()
        {

            string path = "../../../Infrastructure/Data.txt";

            try
            {

                // Read the JSON string from the filetry
                string jsonString = File.ReadAllText(path);

                if (string.IsNullOrEmpty(jsonString))
                {
                    // Return a default or null ToDo object
                    Console.WriteLine(" ---> File is empty to load <---");
                }
                else
                {
                    // Deserialize the JSON string into an Item object
                    _tasks = JsonConvert.DeserializeObject<List<Task>>(jsonString);
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(" ---> Error in loading File <---");

            }

        }

        public void saveToFile()
        {

            try
            {
                string jsonString = JsonConvert.SerializeObject(_tasks);
                string path = "../../../Infrastructure";
                // Write the JSON string to a file
                File.WriteAllText(Path.Combine(path, "data.txt"), jsonString);

            }
            catch (Exception ex)
            {
                Console.WriteLine("---> Error in Saving Data <---");
            }

        }


    }
}
