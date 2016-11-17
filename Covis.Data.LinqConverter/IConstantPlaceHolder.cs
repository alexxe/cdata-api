namespace Covis.Data.LinqConverter
{
    public interface IConstantPlaceHolder
    {
        object GetValue();

        bool IsEmpty { get; set; }
    }
}
