using SimpleToDoList.Application.Models;
using System;
using System.Globalization;
using System.Xml.Linq;



// this class is in charge of getting requests from user

class Program
{

    static void Main(string[] args)
    {

        // while loop to get user inputs

        ToDo todo = new ToDo("sample");
        int input;

        Console.WriteLine("---> welcome! <---\n");

        while (true)
        {

            todo.PrintOptions();
            input = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            try
            {



                switch (input)
                {
                    case 1:

                        //Add a new task
                        Console.WriteLine("---> Enter the title of the task: <---");
                        string title = Console.ReadLine();
                        Console.WriteLine("---> Enter the description of the task: <---");
                        string description = Console.ReadLine();
                        Console.WriteLine("---> Enter the deadline date of the task (MM/DD/YYYY): <---");
                        DateTime deadlineDate = DateTime.Parse(Console.ReadLine());
                        if (deadlineDate <= DateTime.Now)
                        {
                            Console.WriteLine("---> Deadline should be after now! <---");
                            continue;
                        }
                        Console.WriteLine("---> Enter the priority of the task (High, Intermediate, or Low)(be carefull about capitals: <---");
                        Priority priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine());
                        Task task = new Task(title, description, deadlineDate, priority);
                        todo.AddTask(task);

                        break;

                    case 2:

                        // Remove an existing task
                        Console.WriteLine("---> Enter the index of the task you want to remove: <---");
                        int index = int.Parse(Console.ReadLine()) - 1;
                        todo.RemoveByIndex(index);

                        break;

                    case 3:

                        // Remove an existing task
                        Console.WriteLine("---> Enter the index of the task you want to mark as done: <---");
                        int indexx = int.Parse(Console.ReadLine()) - 1;
                        todo.MarkAsDone(indexx);
                        break;

                    case 4:

                        // View all tasks
                        todo.ViewTasks();

                        break;

                    case 5:

                        // get the nearest duotime tasks
                        Task priorTask = todo.GetNearestTask();
                        if (priorTask == null) Console.WriteLine("---> Nothing! <---");
                        else Console.WriteLine(priorTask);

                        break;


                    case 6:

                        // get Informations
                        StatisticsMenu(todo);

                        break;

                    case 7:

                        // Exit the program
                        todo.SaveToFile();
                        Console.WriteLine(" ---> Thanks! <---");
                        Console.WriteLine(" ---> Saving Files! <---");
                        return;

                    default:

                        // Invalid choice
                        Console.WriteLine("---> Invalid option. Please try again. <---");

                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("---> Be carefull about the command foramts! <---");
            }


            Console.WriteLine("\n");
        }

    }


    static void StatisticsMenu(ToDo todo)
    {
        int input;

        Console.WriteLine("---> Statistics! <---\n");

        while (true)
        {

            todo.PrintStatisticsOptions();
            input = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            try
            {



                switch (input)
                {
                    case 1:
                        //Overall informations
                        todo.OverallCounts();
                        break;

                    case 2:
                        //Overall informations
                        Console.WriteLine("---> Enter the \"from\" date (MM/DD/YYYY): <---");
                        DateTime startDate = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("---> Enter the \"to\" date (MM/DD/YYYY): <---");
                        DateTime endDate = DateTime.Parse(Console.ReadLine());
                        var taskCount = todo.DesiredIntervalTasks(startDate, endDate);
                        Console.WriteLine(taskCount); 

                        break;

                    case 3:

                        List<Task> tasksToday = todo.Last3TasksCreatedAndDoneToday();
                        foreach (Task i in tasksToday)
                        {
                            Console.WriteLine(i);
                        }
                        break;

                    case 4:

                        List<Task> notDoneTasks = todo.NotDoneTasksWith5DaysPast();
                        foreach (Task i in notDoneTasks)
                        {
                            Console.WriteLine(i);
                        }
                        break;


                    case 5:
                        return;

                    default:

                        // Invalid choice
                        Console.WriteLine("---> Invalid option. Please try again. <---");

                        break;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("---> Be carefull about the command foramts! <---");
            }


            Console.WriteLine("\n");

        }
    }
}


