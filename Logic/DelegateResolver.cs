namespace HalcyonFlowProject.Logic {
    /// <summary>
    /// Used to delegate actions to be run in the background on a separate thread,
    /// with the possibility of retrying each delegate several times until it 
    /// completes without throwing an exception.
    /// </summary>
    public class DelegateResolver : IDisposable {
        protected Queue<Delegation> queuedActions;
        protected Delegation? currentAction;
        /// <summary>
        /// Whether this object has already been disposed.
        /// </summary>
        public bool Disposed { get; protected set; }

        /// <summary>
        /// Whether to dispose this object once the queued tasks are all completed.
        /// </summary>
        public bool DisposeOnFinish { get; set; } = false;
        /// <summary>
        /// The waiting time (in milliseconds) between each task attempt.
        /// </summary>
        public int AttemptDelay { get; set; }
        /// <summary>
        /// The amount of queued tasks.
        /// </summary>
        public int QueuedDelegatesCount => queuedActions.Count + (currentAction is null ? 0 : 1);
        /// <summary>
        /// Whether any task is currently being run.
        /// </summary>
        public bool IsBusy => QueuedDelegatesCount != 0;
        protected Task resolver;

        /// <summary>
        /// Create a new <see cref="DelegateResolver"/> and start its background 
        /// <see cref="Task"/>, which will check the queue for delegated tasks
        /// every <paramref name="attemptDelay"/> milliseconds.
        /// </summary>
        /// <param name="attemptDelay">Milliseconds to wait before each 
        /// queued action's attempt.</param>
        public DelegateResolver(int attemptDelay = 200) {
            resolver = Task.Run(StartResolver);
            AttemptDelay = attemptDelay;
            queuedActions = new();
        }

        protected virtual async Task StartResolver() {
            while(!Disposed) {
                await Task.Delay(AttemptDelay);
                
                if(currentAction is not null) {
                    // Try the current action and save progress.
                    currentAction.Attempt();
                    if(currentAction.Completed || currentAction.Attempts >= currentAction.MaxAttempts) {
                        currentAction = null;
                    }
                }
                
                if(currentAction is null) {
                    // Action completed or not set - check if any is queued
                    if(queuedActions.Count != 0) {
                        // Start working on a new task
                        currentAction = queuedActions.Dequeue();
                    }else if(DisposeOnFinish) {
                        Dispose();
                        return; // Just to be safe
                    }
                }
            }
        }

        /// <summary>
        /// Add an action to the queue, to be attempted up to 
        /// <paramref name="attempts"/> number of times.
        /// </summary>
        /// <param name="action">The action to be executed.</param>
        /// <param name="attempts">The maximum number of times to try running the 
        /// <paramref name="action"/>.</param>
        /// <exception cref="InvalidOperationException">Thrown when <see cref="DisposeOnFinish"/>
        /// is set to <see langword="true"/>, as it's not safe to access data that's
        /// queued for disposal on another thread.</exception>
        public void EnqueueAction(Delegate action, int attempts = 1) {
            if(DisposeOnFinish)
                throw new InvalidOperationException("This action is not safe. Avoid queuing disposal before all tasks are queued.");
            queuedActions.Enqueue(new(action, attempts));
        }

        ~DelegateResolver() {
            Dispose();
        }

        public void Dispose() {
            if(!Disposed) {
                Disposed = true;
                queuedActions.Clear();
                queuedActions = null!;
                resolver.Wait();
                resolver.Dispose();
                resolver = null!;
            
                GC.SuppressFinalize(this);
            }
        }

        protected class Delegation {
            public Delegate Action { get; protected set; }
            public int MaxAttempts { get; protected set; }
            public int Attempts { get; protected set; }
            public bool Completed { get; protected set; } = false;

            public Delegation(Delegate action, int attempts = 1) {
                MaxAttempts = attempts;
                Action = action;
                Attempts = 0;
            }

            public bool Attempt() {
                Attempts++;
                try {
                    Action.DynamicInvoke();
                    Completed = true;
                } catch { }
                return Completed;
            }
        }

    }
}
