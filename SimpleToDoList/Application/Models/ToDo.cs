using System.Reflection;
using Newtonsoft.Json;
using SimpleToDoList.Application.Models;



public class ToDo : IToDo
{

    private static long CurrentId = 0;
    private long Id { set; get; }
    public string Name { set; get; }
    private List<Task> Tasks;



    public ToDo(string name)
    {
        Name = name;
        Id = CurrentId++;

        // try to load the json file

        try
        {
            List<Task> backUp = loadFromFiles();
            if (backUp != null)
            {
                Tasks = new List<Task>(backUp);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("---> Error in Loading Data <---\n");
            Tasks = new List<Task>();
        }
    }


    public void AddTask(Task task)
    {
        Tasks.Add(task);
        Console.WriteLine("---> Task created! <---\n");
    }

    public void RemoveTaskByIndex(int index)
    {
        if (index >= 0 && index < Tasks.Count)
        {
            Tasks.RemoveAt(index);
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
            foreach (Task task in Tasks)
            {
                if (task.Title.Equals(Name))
                {
                    Tasks.RemoveAt(i);
                    Console.WriteLine($"---> Task{task.Title} removed successfully! <---\n");
                    break;

                }
                i++;
            }
        }
    }


    public void ViewTasks()
    {
        if (Tasks.Count > 0)
        {
            Console.WriteLine("---> Here are your tasks: <---\n");
            for (int i = 0; i < Tasks.Count; i++)
            {
                Console.WriteLine($"Task {i + 1}:");
                Console.WriteLine(Tasks[i]);
            }
        }
        else
        {
            Console.WriteLine("---> You have no tasks. <---\n");
        }
    }

    // edit task

    private List<Task> loadFromFiles()
    {

        string path = "../../../Infrastructure/Data.txt";

        try
        {

            // Read the JSON string from the filetry
            string jsonString = File.ReadAllText(path);

            if (string.IsNullOrEmpty(jsonString))
            {
                // Return a default or null ToDo object
                return null;
            }
            else
            {
                // Deserialize the JSON string into an Item object
                List<Task> item = JsonConvert.DeserializeObject<List<Task>>(jsonString);
                return item;
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error in loading File");

        }

    }

    public void saveToFile()
    {

        try
        {
            string jsonString = JsonConvert.SerializeObject(Tasks);
            string path = "../../../Infrastructure";
            // Write the JSON string to a file
            File.WriteAllText(Path.Combine(path, "data.txt"), jsonString);

        }catch (Exception ex)
        {
            Console.WriteLine("---> Error in Saving Data <---");
        }

    }


    public void PrintOptions()
    {
        Console.WriteLine("Please choose an option with its number \n");
        string optionsMenu = "///// options \\\\\\\\\\" + "\n\n" +
            "1 . Add a new task \n" +
            "2 . Delete a task \n" +
            "3 . All tasks \n" +
            "4 . prior tasks \n" +
            "5 . Exit \n\n" +
            "///// options \\\\\\\\\\ \n";
        Console.WriteLine(optionsMenu);

    }

    public Task? GetNearestTask()
    {
        var validTasks = Tasks.Where(t => t.DeadlineDate >= DateTime.Now);
        var minDate = validTasks.Min(t => t.DeadlineDate);
        var nearestTasks = validTasks.Where(t => t.DeadlineDate == minDate);
        var finalTask = nearestTasks.OrderByDescending(t => t.Priority);
        return finalTask.ToList().FirstOrDefault();
    }

    public void UpdateTask(Task task)
    {
        throw new NotImplementedException();
    }

}


