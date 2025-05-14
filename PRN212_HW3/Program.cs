// Build a task scheduler
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskScheduler
{
    // Simple task priority enum
    public enum TaskPriority
    {
        High,
        Normal,
        Low
    }

    // Interface for task definition
    public interface IScheduledTask
    {
        string Name { get; }
        TaskPriority Priority { get; }
        TimeSpan Interval { get; }
        DateTime LastRun { get; }
        Task ExecuteAsync();
    }

    // A basic implementation of a scheduled task
    public class SimpleTask : IScheduledTask
    {
        private readonly Func<Task> _action;
        private DateTime _lastRun = DateTime.MinValue;

        public string Name { get; }
        public TaskPriority Priority { get; }
        public TimeSpan Interval { get; }

        public DateTime LastRun => _lastRun;

        public SimpleTask(string name, TaskPriority priority, TimeSpan interval, Func<Task> action)
        {
            Name = name;
            Priority = priority;
            Interval = interval;
            _action = action;
        }

        public async Task ExecuteAsync()
        {
            await _action();
            _lastRun = DateTime.Now;
        }
    }

    // The scheduler that students need to implement
    public class TaskScheduler
    {
        // TODO: Implement task queue/storage mechanism
        private PriorityQueue<SimpleTask,TaskPriority> _queue;

        public TaskScheduler()
        {
            // TODO: Initialize your scheduler
            _queue= new PriorityQueue<SimpleTask, TaskPriority>();
        }

        public void AddTask(IScheduledTask task)
        {
            // TODO: Add task to the scheduler
            if(task is null)
            {
                throw new ArgumentException("Can not add null or empty task");
            }
            _queue.Enqueue((SimpleTask)task, task.Priority);
            
        }

        public void RemoveTask(string taskName)
        {
            // TODO: Remove task from the scheduler
            if(_queue.Count == 0)
            {
                throw new ArgumentException("No tasks to remove");
            }
            if (string.IsNullOrEmpty(taskName))
            {
                throw new ArgumentException("taskName must not be null or empty", nameof(taskName));
            }
            var tempQueue = new PriorityQueue<SimpleTask, TaskPriority>();
            foreach (var item in _queue.UnorderedItems)
            {
                if (item.Element.Name != taskName)
                {
                    tempQueue.Enqueue(item.Element, item.Priority);
                }
            }
            _queue = tempQueue;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // TODO: Implement the scheduling logic
            while (!cancellationToken.IsCancellationRequested)
            {
                // - Run higher priority tasks first
                var tempList = new List<SimpleTask>();
                while(_queue.Count>0){
                    IScheduledTask task = _queue.Dequeue();
                    tempList.Add((SimpleTask)task);
                    // - Only run tasks when their interval has elapsed since LastRun
                    if (DateTime.Now - task.LastRun >= task.Interval)
                    {
                        await task.ExecuteAsync();
                        break;
                    }
                    
                } 
                foreach(IScheduledTask task in tempList)
                {
                    _queue.Enqueue((SimpleTask)task,task.Priority);
                }
                // - Keep running until cancellation is requested
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        public List<IScheduledTask> GetScheduledTasks()
        {
            // TODO: Return a list of all scheduled tasks
            List<IScheduledTask> tasks = new List<IScheduledTask>();
            PriorityQueue<SimpleTask, TaskPriority> tempQueue = new PriorityQueue<SimpleTask, TaskPriority>();
            while (_queue.Count > 0)
            {
                var item = _queue.Dequeue();
                tasks.Add(item);
                tempQueue.Enqueue(item, item.Priority);
            }
            return tasks;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Task Scheduler Demo");

            // Create the scheduler
            var scheduler = new TaskScheduler();

            // Add some tasks
            scheduler.AddTask(new SimpleTask(
                "High Priority Task",
                TaskPriority.High,
                TimeSpan.FromSeconds(2),
                async () => {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Running high priority task");
                    await Task.Delay(500); // Simulate some work
                }
            ));

            scheduler.AddTask(new SimpleTask(
                "Normal Priority Task",
                TaskPriority.Normal,
                TimeSpan.FromSeconds(3),
                async () => {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Running normal priority task");
                    await Task.Delay(300); // Simulate some work
                }
            ));

            scheduler.AddTask(new SimpleTask(
                "Low Priority Task",
                TaskPriority.Low,
                TimeSpan.FromSeconds(4),
                async () => {
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Running low priority task");
                    await Task.Delay(200); // Simulate some work
                }
            ));

            // Create a cancellation token that will cancel after 20 seconds
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));

            // Or allow the user to cancel with a key press
            Console.WriteLine("Press any key to stop the scheduler...");

            // Run the scheduler in the background
            var schedulerTask = scheduler.StartAsync(cts.Token);

            // Wait for user input
            Console.ReadKey();
            cts.Cancel();

            try
            {
                await schedulerTask;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Scheduler stopped by cancellation.");
            }
            Console.WriteLine("Scheduler demo finished!");
        }
    }
}