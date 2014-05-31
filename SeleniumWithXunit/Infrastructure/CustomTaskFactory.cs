using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumWithXunit.Infrastructure
{
    // We need to explicitly use Default Scheduler so tests do not dead lock when run in xunit which uses its own scheduler.
    // https://github.com/xunit/xunit/issues/88
    public class CustomTaskFactory
    {
        public static Task<TResult> StartNew<TResult>(Func<TResult> func)
        {
            var task = Task.Factory.StartNew(func, new CancellationToken(), TaskCreationOptions.None, TaskScheduler.Default);
            return task;
        }

        public static Task StartNew(Action action)
        {
            var task = Task.Factory.StartNew(action, new CancellationToken(), TaskCreationOptions.None, TaskScheduler.Default);
            return task;
        }
    }
}