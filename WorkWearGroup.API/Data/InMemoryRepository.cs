using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkWearGroup.API.Models;

namespace WorkWearGroup.API.Data
{
    // TODO: if performance is an issue we could use a dictionary for a fast lookup
    public class InMemoryRepository : IRepository
    {
        private readonly List<Item> _items;

        public InMemoryRepository()
        {
            _items = new List<Item>();
        }

        public async Task<Item> GetAsync(string key)
        {
            var existingItem = _items.FirstOrDefault(x => x.Key.EqualsCaseInsensitive(key));
            return existingItem;
        }

        public Task<bool> AddAsync(Item item)
        {
            var existingItem = _items.FirstOrDefault(x => x.Key.EqualsCaseInsensitive(item.Key));
            if (existingItem != null)
            {
                throw new Exception($"Item with key {item.Key} already exists.");
            }

            _items.Add(item);
            return Task.FromResult(true);
        }

        public Task<bool> AddOrUpdateAsync(Item item)
        {
            var existingItem = _items.FirstOrDefault(x => x.Key.EqualsCaseInsensitive(item.Key));
            if (existingItem == null)
            {
                _items.Add(item);
            }
            else
            {
                existingItem.Value = item.Value;
            }

            return Task.FromResult(true);
        }
    }
}
