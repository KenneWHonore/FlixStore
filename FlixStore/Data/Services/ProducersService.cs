using FlixStore.Data.Base;
using FlixStore.Models;

namespace FlixStore.Data.Services
{
    public class ProducersService: EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
            
        }
    }
}
