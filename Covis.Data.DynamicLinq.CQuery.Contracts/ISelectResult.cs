namespace Covis.Data.DynamicLinq.CQuery.Contracts
{
    public interface ISelectResult<TResult> : IDescriptorAccsessor
        where TResult : class
    {
    }
}