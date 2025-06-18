using FlixStore.Data.Base;
using FlixStore.Models;

namespace FlixStore.Data.Services
{
    public class CinemasService:EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
            
        }
    }
}
