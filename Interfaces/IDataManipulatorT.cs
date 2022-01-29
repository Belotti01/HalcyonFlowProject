namespace HalcyonFlowProject.Interfaces {
    public interface IDataManipulator<TItem> where TItem : class, new() {
        TItem CreateItem();
        void InsertItem(TItem item);
        void UpdateItem(TItem item);
        void DeleteItem(TItem item);
        void UpdateOrInsert(TItem item);

        bool TryInsertItem(TItem item);
        bool TryDeleteItem(TItem item);

        bool ItemExists(TItem item);
    }
}
