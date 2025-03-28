﻿using System.Globalization;
using System.Reflection;

namespace AmmFramework.Utilities.DateTimes;
/// <summary>
/// فرهنگ فارسی سفارشی سازی شده
/// </summary>
public static class PersianCulture
{
    /// <summary>
    /// معادل فارسی روزهای هفته میلادی
    /// </summary>
    public static readonly IDictionary<DayOfWeek, string> PersianDayWeekNames = new Dictionary<DayOfWeek, string>
          {
            {DayOfWeek.Saturday, "شنبه"},
            {DayOfWeek.Sunday,  "یک شنبه"},
            {DayOfWeek.Monday,  "دو شنبه"},
            {DayOfWeek.Tuesday, "سه شنبه"},
            {DayOfWeek.Wednesday, "چهار شنبه"},
            {DayOfWeek.Thursday, "پنج شنبه"},
            {DayOfWeek.Friday, "جمعه"}
          };

    /// <summary>
    /// عدد به حروف روزهای شمسی
    /// </summary>
    public static readonly IDictionary<int, string> PersianMonthDayNumberNames = new Dictionary<int, string>
           {
             { 1, "یکم" },
             { 2, "دوم" },
             { 3, "سوم" },
             { 4, "چهارم" },
             { 5, "پنجم" },
             { 6, "ششم" },
             { 7, "هفتم" },
             { 8, "هشتم" },
             { 9, "نهم" },
             { 10, "دهم" },
             { 11, "یازدهم" },
             { 12, "دوازدهم" },
             { 13, "سیزدهم" },
             { 14, "چهاردهم" },
             { 15, "پانزدهم" },
             { 16, "شانزدهم" },
             { 17, "هفدهم" },
             { 18, "هجدهم" },
             { 19, "نوزدهم" },
             { 20, "بیستم" },
             { 21, "بیست یکم" },
             { 22, "بیست ‌دوم" },
             { 23, "بیست سوم" },
             { 24, "بیست چهارم" },
             { 25, "بیست پنجم" },
             { 26, "بیست ششم" },
             { 27, "بیست هفتم" },
             { 28, "بیست هشتم" },
             { 29, "بیست نهم" },
             { 30, "سی‌ام" },
             { 31, "سی یکم" }
           };

    /// <summary>
    /// نام فارسی ماه‌های شمسی
    /// </summary>
    public static readonly IDictionary<int, string> PersianMonthNames = new Dictionary<int, string>
           {
            {1, "فروردین"},
            {2, "اردیبهشت"},
            {3, "خرداد"},
            {4, "تیر"},
            {5, "مرداد"},
            {6, "شهریور"},
            {7, "مهر"},
            {8, "آبان"},
            {9, "آذر"},
            {10, "دی"},
            {11, "بهمن"},
            {12, "اسفند"}
           };


    private static readonly Lazy<CultureInfo> _cultureInfoBuilder =
                new Lazy<CultureInfo>(getPersianCulture, LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// وهله‌ی یکتای فرهنگ فارسی سفارشی سازی شده
    /// </summary>
    public static CultureInfo Instance { get; } = _cultureInfoBuilder.Value;

    /// <summary>
    /// Returns the day-of-month part of this <see cref="DateTime"/> localized in Persian calendar.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to extend.</param>
    /// <returns>An integer between 1 and 31 representing the day-of-month part of this <see cref="DateTime"/>.</returns>
    public static int GetPersianDayOfMonth(this DateTime dateTime)
    {
        return Instance.DateTimeFormat.Calendar.GetDayOfMonth(dateTime);
    }

    /// <summary>
    /// Returns the month part of this <see cref="DateTime"/> localized in Persian calendar.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to extend.</param>
    /// <returns>An integer between 1 and 12 representing the month part of this <see cref="DateTime"/>.</returns>
    public static int GetPersianMonth(this DateTime dateTime)
    {
        return Instance.DateTimeFormat.Calendar.GetMonth(dateTime);
    }

    /// <summary>
    /// عدد به حروف روزهای شمسی
    /// </summary>
    public static string GetPersianMonthDayNumberName(this int dayNumber)
    {
        if (dayNumber < 1 || dayNumber > 31)
        {
            throw new ArgumentOutOfRangeException($"{nameof(dayNumber)} must be between 1, 31.");
        }
        return PersianMonthDayNumberNames[dayNumber];
    }

    /// <summary>
    /// نام فارسی ماه‌های شمسی
    /// </summary>
    public static string GetPersianMonthName(this int monthNumber)
    {
        if (monthNumber < 1 || monthNumber > 12)
        {
            throw new ArgumentOutOfRangeException($"{nameof(monthNumber)} must be between 1, 12.");
        }
        return PersianMonthNames[monthNumber];
    }

    /// <summary>
    /// دریافت معادل فارسی نام روز هفته‌ی میلادی
    /// </summary>
    public static string GetPersianWeekDayName(this DayOfWeek dayOfWeek)
    {
        return PersianDayWeekNames[dayOfWeek];
    }

    /// <summary>
    /// گرفتن نام فارسی روزهای هفته
    /// </summary>
    public static string GetPersianWeekDayName(int persianYear, int persianMonth, int persianDay)
    {
        return PersianDayWeekNames[new PersianCalendar().ToDateTime(persianYear, persianMonth, persianDay, 0, 0, 0, 0).DayOfWeek];
    }

    /// <summary>
    /// گرفتن نام فارسی روزهای هفته
    /// </summary>
    public static string GetPersianWeekDayName(this DateTime dt)
    {
        var dateParts = dt.ToPersianYearMonthDay();
        return PersianDayWeekNames[new PersianCalendar().ToDateTime(dateParts.Year, dateParts.Month, dateParts.Day, dt.Hour, dt.Minute, dt.Second, 0).DayOfWeek];
    }

    /// <summary>
    /// گرفتن نام فارسی روزهای هفته
    /// </summary>
    public static string GetPersianWeekDayName(this DateTime? dt)
    {
        return dt == null ? string.Empty : dt.Value.GetPersianWeekDayName();
    }

    /// <summary>
    /// گرفتن نام فارسی روزهای هفته
    /// </summary>
    /// <param name="dt">تاریخ و زمان</param>
    /// <param name="dateTimeOffsetPart">کدام جزء این وهله مورد استفاده قرار گیرد؟</param>
    public static string GetPersianWeekDayName(this DateTimeOffset? dt, DateTimeOffsetPart dateTimeOffsetPart = DateTimeOffsetPart.IranLocalDateTime)
    {
        return dt == null ? string.Empty : dt.Value.GetDateTimeOffsetPart(dateTimeOffsetPart).GetPersianWeekDayName();
    }

    /// <summary>
    /// گرفتن نام فارسی روزهای هفته
    /// </summary>
    /// <param name="dt">تاریخ و زمان</param>
    /// <param name="dateTimeOffsetPart">کدام جزء این وهله مورد استفاده قرار گیرد؟</param>
    public static string GetPersianWeekDayName(this DateTimeOffset dt, DateTimeOffsetPart dateTimeOffsetPart = DateTimeOffsetPart.IranLocalDateTime)
    {
        return dt.GetDateTimeOffsetPart(dateTimeOffsetPart).GetPersianWeekDayName();
    }

    /// <summary>
    /// Returns the year part of this <see cref="DateTime"/> localized in Persian calendar.
    /// </summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to extend.</param>
    /// <returns>An integer between 1 and 9999 representing the year part of this <see cref="DateTime"/>.</returns>
    public static int GetPersianYear(this DateTime dateTime)
    {
        return Instance.DateTimeFormat.Calendar.GetYear(dateTime);
    }

    /// <summary>
    /// تاریخ روزهای ابتدا و انتهای سال شمسی را بازگشت می‌دهد
    /// </summary>
    public static PersianYear GetPersianYearStartAndEndDates(this int persianYear)
    {
        var persianCalendar = new PersianCalendar();
        return new PersianYear
        {
            StartDate = persianCalendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0),
            EndDate = persianCalendar.ToDateTime(persianYear, 12, persianYear.GetPersianMonthLastDay(12), 23, 59, 59, 0)
        };
    }

    /// <summary>
    /// سال شمسی معادل را محاسبه کرده و سپس
    /// تاریخ روزهای ابتدا و انتهای آن سال شمسی را بازگشت می‌دهد
    /// </summary>
    public static PersianYear GetPersianYearStartAndEndDates(this DateTime dateTime)
    {
        var persianYear = dateTime.GetPersianYear();
        return persianYear.GetPersianYearStartAndEndDates();
    }

    /// <summary>
    /// سال شمسی معادل را محاسبه کرده و سپس
    /// تاریخ روزهای ابتدا و انتهای آن سال شمسی را بازگشت می‌دهد
    /// </summary>
    public static PersianYear GetPersianYearStartAndEndDates(this DateTimeOffset dateTimeOffset, DateTimeOffsetPart dateTimeOffsetPart = DateTimeOffsetPart.IranLocalDateTime)
    {
        var persianYear = dateTimeOffset.GetDateTimeOffsetPart(dateTimeOffsetPart).GetPersianYear();
        return persianYear.GetPersianYearStartAndEndDates();
    }

    /// <summary>
    /// تاریخ روزهای ابتدا و انتهای ماه شمسی را بازگشت می‌دهد
    /// </summary>
    public static PersianMonth GetPersianMonthStartAndEndDates(this int persianYear, int persianMonth)
    {
        var persianCalendar = new PersianCalendar();
        var isLeapYear = persianCalendar.IsLeapYear(persianYear);
        return new PersianMonth
        {
            StartDate = persianCalendar.ToDateTime(persianYear, persianMonth, 1, 0, 0, 0, 0),
            EndDate = persianCalendar.ToDateTime(persianYear, persianMonth, persianYear.GetPersianMonthLastDay(persianMonth), 23, 59, 59, 0)
        };
    }

    /// <summary>
    /// ماه شمسی معادل را محاسبه کرده و سپس
    /// تاریخ روزهای ابتدا و انتهای آن ماه شمسی را بازگشت می‌دهد
    /// </summary>
    public static PersianMonth GetPersianMonthStartAndEndDates(this DateTime dateTime)
    {
        var persianYear = dateTime.GetPersianYear();
        var persianMonth = dateTime.GetPersianMonth();
        return persianYear.GetPersianMonthStartAndEndDates(persianMonth);
    }

    /// <summary>
    /// ماه شمسی معادل را محاسبه کرده و سپس
    /// تاریخ روزهای ابتدا و انتهای آن ماه شمسی را بازگشت می‌دهد
    /// </summary>
    public static PersianMonth GetPersianMonthStartAndEndDates(this DateTimeOffset dateTimeOffset, DateTimeOffsetPart dateTimeOffsetPart = DateTimeOffsetPart.IranLocalDateTime)
    {
        var dateTime = dateTimeOffset.GetDateTimeOffsetPart(dateTimeOffsetPart);
        var persianYear = dateTime.GetPersianYear();
        var persianMonth = dateTime.GetPersianMonth();
        return persianYear.GetPersianMonthStartAndEndDates(persianMonth);
    }

    /// <summary>
    /// شماره آخرین روز ماه شمسی را بر می‌گرداند
    /// </summary>
    /// <param name="persianYear">سال شمسی</param>
    /// <param name="persianMonth">ماه شمسی</param>
    /// <returns>شماره آخرین روز ماه</returns>
    public static int GetPersianMonthLastDay(this int persianYear, int persianMonth)
    {
        if (persianMonth > 12 || persianMonth <= 0)
        {
            throw new ArgumentOutOfRangeException("ماه وارد شده معتبر نیست.");
        }

        if (persianMonth <= 6)
        {
            return 31;
        }

        if (persianMonth == 12)
        {
            var persianCalendar = new PersianCalendar();
            return persianCalendar.IsLeapYear(persianYear) ? 30 : 29;
        }
        return 30;
    }

    /// <summary>
    /// اصلاح تقویم فرهنگ فارسی
    /// </summary>
    private static CultureInfo getPersianCulture()
    {
        var persianCulture = new CultureInfo("fa-IR")
        {
            DateTimeFormat =
                {
                    AbbreviatedDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" },
                    AbbreviatedMonthGenitiveNames =
                        new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                    AbbreviatedMonthNames =
                        new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                    AMDesignator = "ق.ظ",
                    CalendarWeekRule = CalendarWeekRule.FirstDay,
                    //DateSeparator = "؍",
                    DayNames = new[] { "یکشنبه", "دوشنبه", "سه‌شنبه", "چهار‌شنبه", "پنجشنبه", "جمعه", "شنبه" },
                    FirstDayOfWeek = DayOfWeek.Saturday,
                    FullDateTimePattern = "dddd dd MMMM yyyy",
                    LongDatePattern = "dd MMMM yyyy",
                    LongTimePattern = "h:mm:ss tt",
                    MonthDayPattern = "dd MMMM",
                    MonthGenitiveNames =
                        new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                    MonthNames =
                        new[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند", string.Empty },
                    PMDesignator = "ب.ظ",
                    ShortDatePattern = "yyyy/MM/dd",
                    ShortestDayNames = new[] { "ی", "د", "س", "چ", "پ", "ج", "ش" },
                    ShortTimePattern = "HH:mm",
                    //TimeSeparator = ":",
                    YearMonthPattern = "MMMM yyyy"
                }
        };

        var persianCalendar = new PersianCalendar();
        var fieldInfo = persianCulture.GetType()
                                      .GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        fieldInfo?.SetValue(persianCulture, persianCalendar);

        var info = persianCulture.DateTimeFormat.GetType()
                                                .GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        info?.SetValue(persianCulture.DateTimeFormat, persianCalendar);

        persianCulture.NumberFormat.NumberDecimalSeparator = "/";
        persianCulture.NumberFormat.NumberNegativePattern = 0;

        return persianCulture;
    }
}
