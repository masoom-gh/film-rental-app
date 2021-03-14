using System.Threading.Tasks;
using FilmStudio.Entities;

namespace FilmStudio.Data
{
    public interface IRentalRecordRepository
    {
        // General
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // RentalRecords
        Task<RentalRecord[]> GetAllRentalRecordsAsync();
        Task<RentalRecord> GetRentalRecordAsync(int rentalRecordId);
        Task<RentalRecord[]> GetAllRentalRecordsByFilmStudio(string filmStudioUsername, bool includeOrderItem = false);

        // Films
        Task<Film> GetFilmAsync(int filmId);
        Task<Film[]> GetAllFilmsAsync();

    
    }
}
