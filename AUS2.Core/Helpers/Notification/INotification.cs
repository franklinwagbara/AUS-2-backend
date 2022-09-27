using FluentValidation.Results;
using System.Collections.Generic;
using static AUS2.Core.Helper.Notification.NotificationModel;

namespace AUS2.Core.Helper.Notification
{
    public interface INotification
    {
        IReadOnlyCollection<NotificationModel> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message, ENotificationType notificationType);
        void AddNotification(string key, string message);
        void AddNotifications(IReadOnlyCollection<NotificationModel> notifications);
        void AddNotifications(IList<NotificationModel> notifications);
        void AddNotifications(ICollection<NotificationModel> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}
