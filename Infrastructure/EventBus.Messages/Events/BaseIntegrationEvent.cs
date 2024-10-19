﻿namespace EventBus.Messages.Events
{
    public class BaseIntegrationEvent
    {
        public string CorrelationId { get; set; }
        public DateTime CreationDate { get; private set; }
        public BaseIntegrationEvent()
        {
            CorrelationId = Guid.NewGuid().ToString();
            CreationDate = DateTime.Now;
        }
        public BaseIntegrationEvent(Guid guid, DateTime creationDate)
        {
            CorrelationId = guid.ToString();
            CreationDate = creationDate;
        }
    }
}