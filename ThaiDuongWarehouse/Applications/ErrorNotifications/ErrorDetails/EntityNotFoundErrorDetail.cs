﻿namespace ThaiDuongWarehouse.Api.Applications.ErrorNotifications.ErrorDetails;

public class EntityNotFoundErrorDetail
{
    public string EntityType { get; set; }
    public string EntityId { get; set; }

    public EntityNotFoundErrorDetail(string entityType, string entityId)
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}
