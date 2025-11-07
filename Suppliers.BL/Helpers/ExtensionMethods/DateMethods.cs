namespace Suppliers.BL.Helpers.ExtensionMethods;

public static class DateMethods
{
    public static DateTime FirstDayOfMonth(this DateTime date) => new(date.Year, date.Month, 1);
    public static DateTime LastDayOfMonth(this DateTime date) => new(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
    public static DateTime FirstDayOfYear(this DateTime date) => new(date.Year, 1, 1);
    public static DateTime LastDayOfYear(this DateTime date) => new(date.Year, 12, 31);
    public static DateOnly ToDateOnly(this DateTime date) => DateOnly.FromDateTime(date);
    public static DateTime CopyWith(this DateTime? source, int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? second = null)
    {
        var date = source ?? DateTime.Now;
        return date.CopyWith(year, month, day, hour, minute, second);
    }

    public static DateTime CopyWith(this DateTime date, int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? second = null)
    {
        if (day != null)
        {
            // Verificamos que el día sea válido para el mes y año especificados, si no, ajustamos al último día del mes
            var targetYear = year ?? date.Year;
            var targetMonth = month ?? date.Month;
            var daysInMonth = DateTime.DaysInMonth(targetYear, targetMonth);
            if (day > daysInMonth)
            {
                day = daysInMonth;
            }
        }

        return new DateTime(
            year ?? date.Year,
            month ?? date.Month,
            day ?? date.Day,
            hour ?? date.Hour,
            minute ?? date.Minute,
            second ?? date.Second
        );
    }
}
