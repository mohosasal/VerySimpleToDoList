using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDoList.Application.Models
{
    public interface IToDo
    {
        public void AddTask(Task task);
        public void UpdateTask(Task task);
        public void RemoveTaskByTitle(string Name);
        public void RemoveTaskByIndex(int index);
        public void ViewTasks();
        public void saveToFile();
        public void PrintOptions();
        public Task GetNearestTask();
    }
}
