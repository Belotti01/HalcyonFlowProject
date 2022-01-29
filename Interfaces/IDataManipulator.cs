namespace HalcyonFlowProject.Interfaces {
    public interface IDataManipulator {
        TItem CreateItem<TItem>() where TItem : class, new();
        void InsertItem<TItem>(TItem item) where TItem : class, new();
        void UpdateItem<TItem>(TItem item) where TItem : class, new();
        void DeleteItem<TItem>(TItem item) where TItem : class, new();
        void UpdateOrInsert<TItem>(TItem item) where TItem : class, new();

        bool TryInsertItem<TItem>(TItem item) where TItem : class, new();
        bool TryDeleteItem<TItem>(TItem item) where TItem : class, new();

        bool ItemExists<TItem>(TItem item) where TItem : class, new();
    }
}
