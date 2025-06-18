using FlixStore.Data.Base;
using FlixStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixStore.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        private readonly AppDbContext _context;
        public ActorsService(AppDbContext context) : base(context) { }
       
       
    }
}
