using System.Globalization;

namespace Order.Application.DTOs.OrderDetails
{
    public class OrderCountDto
    {
        public int DeliveredOrdersCount { get; set; }
        public int TotalOrdersCount { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month); 
    }
}
