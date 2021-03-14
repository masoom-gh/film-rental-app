using FilmStudio.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace FilmStudio.Data
{
    public class RentalRecordRepository : IRentalRecordRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<RentalRecordRepository> _logger;

        public RentalRecordRepository(AppDbContext context, ILogger<RentalRecordRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        // General
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating an object of type {entity.GetType()} in the context.");
        }
        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            return (await _context.SaveChangesAsync()) > 0;
        }

        // RentalRecords
        public async Task<RentalRecord[]> GetAllRentalRecordsAsync()
        {
            _logger.LogInformation($"Getting all Camps");

            IQueryable<RentalRecord> query = _context.RentalRecords
                .Include(r => r.FilmStudio)
                .Include(r=>r.Film);

            query = query.OrderByDescending(c => c.OrderId);

            return await query.ToArrayAsync();
        }

        public async Task<RentalRecord> GetRentalRecordAsync(int rentalRecordId)
        {
            _logger.LogInformation($"Getting a rental record with Id {rentalRecordId}");

            IQueryable<RentalRecord> query = _context.RentalRecords
                .Include(r => r.FilmStudio)
                .Include(r => r.Film);

            query = query.Where(r => r.OrderId == rentalRecordId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<RentalRecord[]> GetAllRentalRecordsByFilmStudio(string filmStudioUsername, bool includeOrderItem = false)
        {
            _logger.LogInformation($"Getting all rental records for a film studio");

            IQueryable<RentalRecord> query = _context.RentalRecords
                .Include(r => r.FilmStudio);

            if (includeOrderItem)
            {
                query = query
                    .Include(r => r.Film);
            }

            query = query.OrderByDescending(r => r.OrderId)
                .Where(r => r.FilmStudio.UserName == filmStudioUsername);

            return await query.ToArrayAsync();
        }
        
        
        // Films
        public async Task<Film[]> GetAllFilmsAsync()
        {
            _logger.LogInformation($"Getting all films");

            var query = _context.Films
                .OrderBy(f => f.FilmName);

            return await query.ToArrayAsync();
        }

        public async Task<Film> GetFilmAsync(int filmId)
        {
            _logger.LogInformation($"Getting a film");

            var query = _context.Films
                .Where(f => f.FilmId == filmId);

            return await query.FirstOrDefaultAsync();
        }

      
    }
}
