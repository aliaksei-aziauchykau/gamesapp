using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BGP.CodeGeneration;

public class TaskBuilder
{
    private readonly IList<Func<Task>> actions = new List<Func<Task>>();

    public TaskBuilder Create()
    {
        return new TaskBuilder();
    }

    public TaskBuilder CleanTasks()
    {
        actions.Clear();
        return this;
    }

    public TaskBuilder AddAction(params Func<Task>[] actions)
    {
        foreach (var action in actions)
        {
            this.actions.Add(action);
        }

        return this;
    }

    public TaskRunner GetTaskRunner()
    {
        return new TaskRunner(actions);
    }
}
