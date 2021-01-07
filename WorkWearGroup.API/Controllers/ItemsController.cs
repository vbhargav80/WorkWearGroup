using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WorkWearGroup.API.Models;
using WorkWearGroup.API.Services;

namespace WorkWearGroup.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ApiControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IItemService _itemService;

        public ItemsController(ILogger<ItemsController> logger, IItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(string key)
        {
            var result = await _itemService.GetItemAsync(key);
            return OkOrFailure(result);
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> CreateAsync(string key, [FromBody] SubmitValueModel model)
        {
            var result = await _itemService.AddItemAsync(key, model.Value);
            return OkOrFailure(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> UpdateAsync(string key, [FromBody] SubmitValueModel model)
        {
            var result = await _itemService.UpdateItemAsync(key, model.Value);
            return OkOrFailure(result);
        }
    }
}
