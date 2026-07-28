using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BGP.CodeGeneration;

public class TaskRunner
{
    private readonly IList<Func<Task>> actions;

    public TaskRunner(IList<Func<Task>> actions)
    {
        this.actions = actions;
    }

    public async Task RunAllAsync()
    {

        foreach (var action in actions)
        {
            await action();
        }

        // await Task.WhenAll(actions.Select(action => action()));
    }
}
