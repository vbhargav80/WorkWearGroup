using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using WorkWearGroup.API.Data;
using WorkWearGroup.API.Models;
using WorkWearGroup.API.Validation;

namespace WorkWearGroup.API.Services
{
    public class ItemService : IItemService
    {
        private readonly IRepository _repository;
        private readonly IValidator<Item> _validator;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IRepository repository, IValidator<Item> validator, ILogger<ItemService> logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<ServiceResult<Item>> GetItemAsync(string key)
        {
            // Repetitive - could be refactored time permitting
            if (!Regex.IsMatch(key, ItemValidator.RegexForUrlSafeKey))
            {
                return ServiceResult<Item>.Failed(ErrorCategory.Validation, "Invalid Key");
            }

            var item = await _repository.GetAsync(key);
            if (item == null)
            {
                _logger.LogWarning($"Item with key {key} not found");
                return ServiceResult<Item>.Failed(ErrorCategory.NotFound, "Item not found");
            }

            return ServiceResult<Item>.Success(item);
        }

        // TODO: Validation code is being repeated. Time permitting I would have done it via model validation or something
        // in one place before even the request hits the controller
        public async Task<ServiceResult<bool>> AddItemAsync(string key, string value)
        {
            var item = new Item { Key = key, Value = value };

            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
            {
                // TODO: time permitting I'd not serialize the error message, rather handle it in a better way
                return ServiceResult<bool>.Failed(ErrorCategory.Validation, JsonSerializer.Serialize(validationResult.Errors));
            }

            try
            {
                await _repository.AddAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to add the item. Error: {ex.Message}");
                return ServiceResult<bool>.Failed(ErrorCategory.InternalServerError, ex.Message);
            }

            return ServiceResult<bool>.Success(true);
        }

        // TODO: Validation code is being repeated. Time permitting I would have done it via model validation or something
        // in one place before even the request hits the controller
        public async Task<ServiceResult<bool>> UpdateItemAsync(string key, string value)
        {
            var item = new Item { Key = key, Value = value };

            var validationResult = _validator.Validate(item);
            if (!validationResult.IsValid)
            {
                return ServiceResult<bool>.Failed(ErrorCategory.Validation, JsonSerializer.Serialize(validationResult.Errors));
            }

            try
            {
                await _repository.AddOrUpdateAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to add/update the item. Error: {ex.Message}");
                return ServiceResult<bool>.Failed(ErrorCategory.InternalServerError, ex.Message);
            }

            return ServiceResult<bool>.Success(true);
        }
    }
}
