namespace QData.Common
{
    public enum NodeType
    {
        Querable = 0,
        Member = 1,
        Constant = 2,
        Binary = 3,
        Method = 4

    }

    public enum MethodType
    {
        Where = 0,
        Select = 1,
        Any = 2,
        Count = 3,
        OrderBy = 4,
        OrderByDescending = 5,
        Take = 6,
        Skip = 7

    }

    public enum BinaryType
    {
        And = 0,
        Or = 1,
        Equal = 2,
        GreaterThan = 3,
        GreaterThanOrEqual = 4,
        LessThan = 5,
        LessThanOrEqual = 6,
        Contains = 7,
        StartsWith = 8,
        EndsWith = 9,
        In = 10

    }
}
