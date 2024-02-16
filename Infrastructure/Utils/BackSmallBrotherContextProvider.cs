using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Utils;

public class BackSmallBrotherContextProvider
{
    private readonly IConnectionStringProvider _connectionStringProvider;

    public BackSmallBrotherContextProvider(IConnectionStringProvider connectionStringProvider)
    {
        _connectionStringProvider = connectionStringProvider;
    }

    public BackSmallBrotherContext NewContext()
    {
        return new BackSmallBrotherContext(_connectionStringProvider);
    }
}