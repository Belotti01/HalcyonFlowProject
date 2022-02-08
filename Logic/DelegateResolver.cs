namespace HalcyonFlowProject.Logic {
    public class DelegateResolver : IDisposable {
        protected Queue<Delegation> queuedActions;
        protected bool disposed;
        public int AttemptDelay { get; set; }

        public int QueuedDelegates => queuedActions.Count;
        public bool IsBusy => QueuedDelegates != 0;
        protected Task resolver;

        public DelegateResolver(int attemptDelay = 200) {
            resolver = Task.Run(StartResolver);
            AttemptDelay = attemptDelay;
            queuedActions = new();
        }

        protected virtual async Task StartResolver() {
            Delegation? currentAction = null;

            while(!disposed) {
                await Task.Delay(AttemptDelay);
                if(currentAction is not null) {
                    currentAction.Attempt();
                    if(currentAction.Completed || currentAction.Attempts >= currentAction.MaxAttempts) {
                        currentAction = null;
                    }
                }
                
                if(currentAction is null && queuedActions.Count != 0) {
                    currentAction = queuedActions.Dequeue();
                }
            }
        }

        public void EnqueueAction(Delegate action, int attempts = 1) {
            queuedActions.Enqueue(new(action, attempts));
        }

        ~DelegateResolver() {
            Dispose();
        }

        public void Dispose() {
            if(!disposed) {
                disposed = true;
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
