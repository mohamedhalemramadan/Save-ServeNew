using Domain.Contracts;
using Domain.Entities;
using Servcies.Abstractions;
using Shared.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servcies
{
    public class ConsumerService : IConsumerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConsumerRepository _consumerRepository;

        public ConsumerService(IUnitOfWork unitOfWork, IConsumerRepository consumerRepository)
        {
            _unitOfWork = unitOfWork;
            _consumerRepository = consumerRepository;
        }

        

        public async Task<ConsumerDto> GetByIdAsync(int id)
        {
            var consumer = await _consumerRepository.GetByIdAsync(id);
            if (consumer == null)
                throw new Exception("Consumer not found");

            return MapToDto(consumer);
        }

        public async Task<IEnumerable<ConsumerDto>> GetAllAsync()
        {
            var consumers = await _consumerRepository.GetAllAsync();
            return consumers.Select(c => MapToDto(c));
        }

        public async Task<ConsumerDto> GetByUserIdAsync(string userId)
        {
            var consumer = await _consumerRepository.GetByUserIdAsync(userId);
            if (consumer == null)
                throw new Exception("Consumer not found for this user");

            return MapToDto(consumer);
        }

        public async Task<ConsumerDto> CreateAsync(CreateConsumerDto dto, string userId)
        {
            // Check if user already has a consumer profile
            var existing = await _consumerRepository.GetByUserIdAsync(userId);
            if (existing != null)
                throw new Exception("User already has a consumer profile");

            var consumer = new Consumer
            {
                UserId = userId,
                Age = dto.Age,
                Gender = dto.Gender,
                PreferredPaymentMethod = dto.PreferredPaymentMethod
            };

            await _consumerRepository.AddAsync(consumer);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(consumer);
        }

        public async Task<ConsumerDto> UpdateAsync(int id, UpdateConsumerDto dto)
        {
            var consumer = await _consumerRepository.GetByIdAsync(id);
            if (consumer == null)
                throw new Exception("Consumer not found");

            if (dto.Age.HasValue)
                consumer.Age = dto.Age.Value;

            consumer.Gender = dto.Gender ?? consumer.Gender;
            consumer.PreferredPaymentMethod = dto.PreferredPaymentMethod ?? consumer.PreferredPaymentMethod;

            _consumerRepository.Update(consumer);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(consumer);
        }

        public async Task DeleteAsync(int id)
        {
            var consumer = await _consumerRepository.GetByIdAsync(id);
            if (consumer == null)
                throw new Exception("Consumer not found");

            _consumerRepository.Delete(consumer);
            await _unitOfWork.SaveChangesAsync();
        }

        // Helper method
        private ConsumerDto MapToDto(Consumer consumer)
        {
            return new ConsumerDto
            {
                Id = consumer.Id,
                Name = consumer.User?.DisplayName ?? "N/A",
                Age = consumer.Age,
                Gender = consumer.Gender,
                PreferredPaymentMethod = consumer.PreferredPaymentMethod
            };
        }
    }
}
