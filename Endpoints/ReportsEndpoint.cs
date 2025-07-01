using Project.Services.Reports;

namespace Project.Endpoints
{
    public static class ReportsEndpoint
    {
        public static void RegisterReportEndpoints(this WebApplication app)
        {
            app.MapGet("/Reporting/TotalSales/", (HttpContext context, IReportingService salesReport) =>
            {
                var totalSales = salesReport.GetTotalSales();
                return Results.Ok($"Total sales since the start of the business is {totalSales}");

            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/TotalSales/Month/{Month}", (HttpContext context, int month, IReportingService salesReport) =>
            {
                var monthlySales = salesReport.GetMonthlySales(month);
                return Results.Ok($"Total sales in selected month is {monthlySales}");

            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/TotalSales/Year/{Year}", (HttpContext context, int year, IReportingService salesReport) =>
            {
                var yearlySales = salesReport.GetYearlySales(year);
                return Results.Ok($"Total sales in selected year is {yearlySales}");

            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/MostPopularProducts", (HttpContext context, IReportingService salesReport) =>
            {
                var mostPopularProducts = salesReport.GetMostPopularProducts();
                return Results.Ok(mostPopularProducts);
            }).RequireAuthorization("adminAccess");

            app.MapGet("/Reporting/TopCustomers", (HttpContext context, IReportingService salesReport) =>
            {
                var topCustomersList = salesReport.GetTopCustomers();
                return Results.Ok(topCustomersList);

            }).RequireAuthorization("adminAccess");
        }
    }

}
