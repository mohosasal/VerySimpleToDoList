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
        public void RemoveTaskByTitle(string Name);
        public void MarkAsDone(int index);
        public void ViewTasks();
        public void PrintOptions();
        public Task GetNearestTask();
    }
}
