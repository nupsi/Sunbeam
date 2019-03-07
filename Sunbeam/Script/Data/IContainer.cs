namespace Sunbeam.Data
{
    public interface IContainer
    {
        string GetName();
        void SetData(IData data);
        IData GetData();
    }
}
