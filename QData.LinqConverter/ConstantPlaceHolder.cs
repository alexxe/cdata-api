namespace QData.LinqConverter
{
    public class ConstantPlaceHolder<T> : IConstantPlaceHolder
    {
        public T Value { get; set; }

        public bool IsEmpty { get; set; } 
        public object GetValue()
        {
            return this.Value;
        }
    }
}
