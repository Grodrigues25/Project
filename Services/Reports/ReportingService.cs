using Project.Services.Database;
using System.Globalization;
using System.Linq;
using Project.Models.Orders;
using Project.Models.Reports;

namespace Project.Services.Reports
{
    public class ReportingService : IReportingService
    {
        private readonly UserDbContext _userDbContext;

        public ReportingService (UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public float GetMonthlySales(int month)
        {
            var totalMonthlySales = _userDbContext.order
                .Where(o => o.Timestamp.Month == month)
                .Select(o => o.TotalPrice)
                .Sum();

            return totalMonthlySales;
        }

        public List<ReportTopProductsModel> GetMostPopularProducts()
        {
            var mostPopularProducts = _userDbContext.orderItems
                .GroupBy(orderItem => orderItem.ProductId)
                .Select(group => new ReportTopProductsModel
                {
                    ProductId = group.Key,
                    TotalQuantity = group.Sum(item => item.Quantity)
                })
                .ToList();

            var mostPopularProductsOrdered = mostPopularProducts
                .OrderByDescending(item => item.TotalQuantity)
                .Take(10)
                .ToList();

            return mostPopularProductsOrdered;
        }

        public List<ReportTopCustomersModel> GetTopCustomers()
        {
            var topCustomers = _userDbContext.order
                .GroupBy(order => order.UserId)
                .Select(group => new ReportTopCustomersModel
                {
                    UserId = group.Key,
                    TotalSpent = group.Sum(orders => orders.TotalPrice)
                })
                .ToList();

            var topCustomersOrdered = topCustomers
                .OrderByDescending(item => item.TotalSpent)
                .Take(10)
                .ToList();

            return topCustomersOrdered;
        }

        public float GetTotalSales()
        {
            var totalSales = _userDbContext.order
                .Select(o => o.TotalPrice)
                .Sum();

            return totalSales;
        }

        public float  GetYearlySales(int year)
        {
            var totalYearlySales = _userDbContext.order
                .Where(o => o.Timestamp.Year == year)
                .Select(o => o.TotalPrice)
                .Sum();

            return totalYearlySales;
        }
    }
}
