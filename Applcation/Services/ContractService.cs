using Application.Dtos.Notification;
using Application.Dtos.User;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContractService : BaseService<Contract, ContractDto>, IContractService
    {
        private readonly INotificationService _notificationService;

        public ContractService(IContractRepository repository, INotificationService notificationService)
            : base(repository)
        {
            _notificationService = notificationService;
        }

        public override async Task<int> UpdateAsync(ContractDto entity)
        {
            var original = await GetByIdAsync(entity.Id);
            var originalStatus = original.Status;

            var result = await base.UpdateAsync(entity);

            // send notification if the contract is canceled
            if (originalStatus == ContractStatus.InEffect
                && entity.Status == ContractStatus.Canceled)
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                if (entity.McCancelDate != null)
                {
                    await _notificationService.AddAsync(new NotificationDto
                    {
                        UserId = entity.ClientId,
                        Type = NotificationType.ContractCanceled,
                        Message = $"The contract on event {entity.EventName} is canceled.",
                        AdditionalInfo = JsonConvert.SerializeObject(new ContractCanceledAdditionalInfo
                        {
                            ContractId = entity.Id
                        }, serializerSettings),
                        IsRead = false,
                        Status = NotificationStatus.NotEditable,
                        ThumbUrl = original.Mc?.AvatarUrl
                    });
                }
                else if (entity.ClientCancelDate != null)
                {
                    await _notificationService.AddAsync(new NotificationDto
                    {
                        UserId = entity.McId,
                        Type = NotificationType.ContractCanceled,
                        Message = $"The contract on event {entity.EventName} is canceled.",
                        AdditionalInfo = JsonConvert.SerializeObject(new ContractCanceledAdditionalInfo
                        {
                            ContractId = entity.Id
                        }, serializerSettings),
                        IsRead = false,
                        Status = NotificationStatus.NotEditable,
                        ThumbUrl = original.Client?.AvatarUrl
                    });
                }
            }

            return result;
        }
    }
}
