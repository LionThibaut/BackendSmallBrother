using Infrastructure.Ef.DbEntities;

namespace Infrastructure.Ef;

public interface IReportingRepository
{
    IEnumerable<DbReporting> FetchAll();
    
    DbReporting FetchById(int id);

    DbReporting Create(float? longitude, float? latitude,
        string descriptionReporting, string lastSeenDate,
        string lastSeenHour, int idPos);
}