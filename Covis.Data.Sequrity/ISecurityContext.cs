namespace Covis.Data.DynamicLinq.Security
{
    public interface ISecurityContext
    {
        ISecurityConsumer Consument { get; set; }

        IEntityRule GetRule(string entityName);
    }
}
