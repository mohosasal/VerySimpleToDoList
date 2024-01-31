public class Task
{

    private static long CurrentId = 0;

    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeadlineDate { get; set; }
    public Priority Priority { get; set; }

    public bool IsCompleted { get; set; }
    public DateTime DoneDate { get; set; }


    public Task(string title, string description, DateTime deadlineDate, Priority priority = Priority.Low)
    {
        //set id
        this.Id = CurrentId++;
        //set title
        if (title == null) Title = "Uknown" + Id.ToString();
        else this.Title = title;
        //set description
        if (description == null) Description = "Empty";
        else this.Description = description;
        //set priority
        this.Priority = priority;
        //set dates
        this.CreateDate = DateTime.Now;
        this.UpdateDate = DateTime.Now;
        this.DeadlineDate = deadlineDate;
    }

    public bool MarkAsDone()
    {
        if (IsCompleted) return true;
        if (DateTime.Now < this.DeadlineDate) return false;
        this.IsCompleted = true;
        this.DoneDate = DateTime.Now;
        return true;
    }


    public override string ToString()
    {

        return $"-----------------------\n" +
            $"Title: {Title}\n" +
            $"Description: {Description}\n" +
            $"Date of creation: {CreateDate}\n" +
            $"Deadline date: {DeadlineDate}\n" +
            $"Priority: {Priority}\n" +
            $"-----------------------\n";
    }
}


