namespace HalcyonFlowProject.Logic {
    public class DbEditor<TItem> : IDisposable where TItem : class, new() {
        protected DB DbContext;
        protected List<TItem> addedItems;
        protected List<TItem> editedItems;
        protected List<TItem> removedItems;

        public bool HasPendingChanges => editedItems.Any() || addedItems.Any() || removedItems.Any();


        public DbEditor(DB dbContext) {
            DbContext = dbContext;
            addedItems = new();
            editedItems = new();
            removedItems = new();
        }

        public void AddOrUpdate(TItem? item = null) {
            if(item is null || !DbContext.ItemExists(item)) {
                Add();
            }else {
                Update(item);
            }
        }

        public void Add(TItem? item = null) {
            item ??= new();
            addedItems.Add(item);
            DbContext.Set<TItem>().Add(item);
            DbContext.SaveChanges();
        }

        public void Update(TItem item) {
            DbContext.UpdateItem(item);
            if(!editedItems.Contains(item)) {
                editedItems.Add(item);
            }
        }

        public void Remove(TItem item) {
            if(removedItems.Contains(item)) return;
            // Remove pending changes for the item
            if(editedItems.Contains(item)) {
                editedItems.Remove(item);
            } else if(addedItems.Contains(item)) {
                addedItems.Remove(item);
            }
            // Delete item
            removedItems.Add(item);
            DbContext.DeleteItem(item);
            DbContext.SaveChanges();
        }

        public void UndoChanges() {
            // Undo changes for all modified items
            editedItems.ForEach(x => DbContext.Entry(x).Reload());
            editedItems.Clear();
            // Remove all added items
            addedItems.ForEach(x => DbContext.Remove(x));
            addedItems.Clear();
            // Re-add removed items, then drop tracker of the added items
            removedItems.ForEach(x => DbContext.InsertItem(x));
            removedItems.Clear();
            DbContext.SaveChanges();
        }

        public void SaveChanges() {
            // Check for changes to the items
            editedItems.ForEach(x => DbContext.Entry(x).DetectChanges());
            DbContext.SaveChanges();

            // Stop tracking already applied changes
            addedItems.Clear();
            editedItems.Clear();
            removedItems.Clear();
        }

		public void Dispose() {
            addedItems?.Clear();
            addedItems = null!;
            editedItems?.Clear();
            editedItems = null!;
            removedItems?.Clear();
            removedItems = null!;

            DbContext = null!;

            GC.SuppressFinalize(this);
		}

        ~DbEditor() {
            Dispose();
        }
	}
}
