using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleToDoList.Application.Models;
using SimpleToDoList.Application.Repository;
using SimpleToDoList.Application.Models.Heap;


public class ToDo : IToDo
{

    private static long CurrentId = 0;
    private long Id { set; get; }
    public string Name { set; get; }
    private TaskRepository _taskRepository;



    public ToDo(string name)
    {
        Name = name;
        Id = CurrentId++;

        // try to load the json file

        try
        {
            _taskRepository = new TaskRepository();
        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Error in Loading Data <---\n");
        }
    }


    public void AddTask(Task task)
    {
        _taskRepository.Add(task);
        Console.WriteLine("---> Task created! <---\n");
    }


    public void RemoveByIndex(int index)
    {
        if (index >= 0 && index < _taskRepository.GetAll().Count())
        {
            _taskRepository.Remove(index);
            Console.WriteLine("---> Task removed! <---\n");
        }
        else { Console.WriteLine("---> index is invalid! <---\n"); }
    }

    public void RemoveTaskByTitle(string Name)
    {
        if (Name == null) Console.WriteLine("---> Title is null <---\n");
        else
        {
            int i = 0;
            var tasks = _taskRepository.GetAll();
            foreach (Task task in tasks)
            {
                if (task.Title.Equals(Name))
                {
                    _taskRepository.Remove(i);
                    Console.WriteLine($"---> Task{task.Title} removed successfully! <---\n");
                    break;
                }
                i++;
            }
        }
    }


    public void ViewTasks()
    {
        var tasks = _taskRepository.GetAll();

        if (tasks.Count > 0)
        {
            Console.WriteLine("---> Here are your tasks: <---\n");
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Task {i + 1}:");
                Console.WriteLine(tasks[i]);
            }
        }
        else
        {
            Console.WriteLine("---> You have no tasks. <---\n");
        }
    }

    public void MarkAsDone(int index) {

        if (index >= 0 && index < _taskRepository.GetAll().Count())
        {
            var task =_taskRepository.Get(index);
            if (task.DeadlineDate < DateTime.Now)
            {
                Console.WriteLine("---> outDated! <---\n");
                return;
            }
            task.IsCompleted = true;
            task.DoneDate= DateTime.Now;
            Console.WriteLine("---> Task is done! <---\n");
        }
        else { Console.WriteLine("---> index is invalid! <---\n"); }
    }




    public void PrintOptions()
    {
        Console.WriteLine("Please choose an option with its number \n");
        string optionsMenu = "///// options \\\\\\\\\\" + "\n\n" +
            "1 . Add a new task \n" +
            "2 . Delete a task \n" +
            "3 . Mark as done \n" +
            "4 . All tasks \n" +
            "5 . prior tasks \n" +
            "6 . overal informations \n" +
            "7 . Exit \n\n" +
            "///// options \\\\\\\\\\ \n";
        Console.WriteLine(optionsMenu);

    }

    public Task? GetNearestTask()
    {
        var tasks = _taskRepository.GetAll();
        var validTasks = tasks.Where(t => t.DeadlineDate >= DateTime.Now);
        var minDate = validTasks.Min(t => t.DeadlineDate);
        var nearestTasks = validTasks.Where(t => t.DeadlineDate == minDate);
        var finalTask = nearestTasks.OrderByDescending(t => t.Priority);
        return finalTask.ToList().FirstOrDefault();
    }

    

    internal void PrintStatisticsOptions()
    {
        Console.WriteLine("Please choose an option with its number \n");
        string optionsMenu = "///// statistic options \\\\\\\\\\" + "\n\n" +
            "1 . Overall Counts \n" +
            "2 . In desired interval tasks count \n" +
            "3 . Last 3 done and created task in today\n" +
            "4 . Not done tasks with 5 days past the createion \n" +
            "5 . Return \n\n" +
            "///// statistic options \\\\\\\\\\ \n";
        Console.WriteLine(optionsMenu);
    }

    public void OverallCounts()
    {
        string output = "";
        output += $"all tasks count : {_taskRepository.GetAll().Count()} \n";
        output += $"done tasks count : {_taskRepository.GetAll().Count(task => task.IsCompleted)}\n";
        output += $"done tasks count : {_taskRepository.GetAll().Count(task => !task.IsCompleted)}\n";
        output += $"expired tasks count : {_taskRepository.GetAll().Count(task => task.DeadlineDate < DateTime.Now && !task.IsCompleted)}\n";
        output += $"not done task count based on priority :\n";
        var aggregateResault = _taskRepository.GetAll()
            .Where(t => !t.IsCompleted)
            .GroupBy(t => t.Priority)
            .Select(g => new
            {
               Type = g.Key,
               Count = g.Count()
            })
            .OrderBy(g => g.Type)
            .ToList();
        foreach (var item in aggregateResault)
        {
            output += $"{item.Type} : {item.Count}\n";
        }
        Console.WriteLine(output);
        
    }

    public void SaveToFile()
    {
        _taskRepository.saveToFile();
    }

    public int DesiredIntervalTasks(DateTime startDate, DateTime endDate)
    {
        return _taskRepository.GetAll().Count(task => task.DoneDate <= endDate && task.DoneDate >= startDate);
    }

    internal List<Task> Last3TasksCreatedAndDoneToday()
    {
        return _taskRepository.GetAll().Where(task => 
        task.CreateDate.Date == DateTime.Today && 
        task.DoneDate == DateTime.Today)
            .TakeLast(3).ToList();
    }

    internal List<Task> NotDoneTasksWith5DaysPast()
    {
        return _taskRepository.GetAll().Where(task => 
        (DateTime.Now - task.CreateDate).TotalDays > 5)
            .OrderByDescending(task => task.CreateDate)
            .Take(3)
            .ToList();
    }

    internal Task GetTheMostPriorTaskByHeap()
    {
        var comparer = Comparer<Task>.Create((x, y) => y.DeadlineDate.Value.CompareTo(x.DeadlineDate.Value));
       
        var heap = _taskRepository.GetAll().ToHeap(comparer);

        Task item;
        

        while(heap.Count > 0)
        {
            item = heap.Remove();
            if (item.DeadlineDate > DateTime.Now)
            {
                return item;
            }

        }
        return null;



    }
}
