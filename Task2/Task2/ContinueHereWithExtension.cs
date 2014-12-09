using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public static class ContinueHereWithExtension
    {
        #region Action & Action<Task>
        public static Task ContinueHereWith(this Task task, Action continuationAction)
        {
            return task
                .ContinueWith(
                    (t) => new Task(continuationAction).Start(),
                    TaskScheduler.FromCurrentSynchronizationContext()
                );
        }

        public static Task ContinueHereWith(this Task task, Action<Task> continuationAction)
        {
            return task
                .ContinueWith(
                    continuationAction,
                    TaskScheduler.FromCurrentSynchronizationContext()
                );
        }
        #endregion

        #region Func<T>, Func<Task,T>, Func<Task,Task<T>>
        public static Task<T> ContinueHereWith<T>(this Task<T> task, Func<T> continuationFunc)
        {
            return task
                .ContinueWith(
                (t) => continuationFunc.Invoke(), 
                TaskScheduler.FromCurrentSynchronizationContext()
                );
        }
            
        public static Task<T> ContinueHereWith<T>(this Task<T> task, Func<Task, T> continuationFunc)
        {
            return task
                .ContinueWith(
                (t) => continuationFunc.Invoke(t),
                TaskScheduler.FromCurrentSynchronizationContext()
                );
        }

        public static Task<T> ContinueHereWith<T>(this Task<T> task, Func<Task, Task<T>> continuationFunc)
        {
            return task
                .ContinueWith<T>(
                (t) => continuationFunc.Invoke(t).Result, 
                TaskScheduler.FromCurrentSynchronizationContext()
                );
        }
        #endregion
    }
}
