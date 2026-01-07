using Bus.Contracts.Enum;

namespace Bus.Contracts.Models.Notifications
{
    public class NotificationsModel
    {
        public Guid UserId { get; set; }
        public List<string> ReplaceValue { get; set; }
        public List<string> Roles { get; set; }
        public LanguageEnum Language { get; set; }
        public MessageEnumKey Message { get; set; } 
    }
}
