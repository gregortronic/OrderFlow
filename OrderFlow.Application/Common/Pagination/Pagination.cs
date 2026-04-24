namespace OrderFlow.Application.Common.Pagination;

public static class Pagination
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;

    public static int NormalizePage(int page)
    {
        return page < 1 ? DefaultPage : page;
    }

    public static int NormalizePageSize(int pageSize)
    {
        return pageSize switch
        {
            < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => pageSize
        };
    }

    public static int GetSkip(int page, int pageSize)
    {
        var skip = (long)(page - 1) * pageSize;
        return skip > int.MaxValue ? int.MaxValue : (int)skip;
    }
}