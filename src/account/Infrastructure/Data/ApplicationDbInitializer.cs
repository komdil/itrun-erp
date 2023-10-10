using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    internal class ApplicationDbInitializer : IApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            if (_context.Database.IsRelational())
            {
                _context.Database.Migrate();
            }
        }
    }
}
