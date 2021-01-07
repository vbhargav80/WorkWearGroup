using System.Threading.Tasks;
using WorkWearGroup.API.Models;

namespace WorkWearGroup.API.Services
{
    public interface IItemService
    {
        public Task<ServiceResult<Item>> GetItemAsync(string key);
        public Task<ServiceResult<bool>> AddItemAsync(string key, string value);
        public Task<ServiceResult<bool>> UpdateItemAsync(string key, string value);
    }
}
