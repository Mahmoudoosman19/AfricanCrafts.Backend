using Common.Application.Abstractions;

namespace Product.Infrastructure.Service
{
    public class TimeService:ITimeService
    {
        private readonly TimeZoneInfo _egyptTimeZone;

        public TimeService()
        {
            _egyptTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
        }

        public DateTime GetCurrentEgyptTime()
        {
            return TimeZoneInfo.ConvertTime(DateTime.Now, _egyptTimeZone);
        }
    }
}
