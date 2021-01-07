using System.Threading.Tasks;
using WorkWearGroup.API.Models;

namespace WorkWearGroup.API.Data
{
    public interface IRepository
    {
        public Task<Item> GetAsync(string key);
        public Task<bool> AddAsync(Item item);
        public Task<bool> AddOrUpdateAsync(Item item);
    }
}
