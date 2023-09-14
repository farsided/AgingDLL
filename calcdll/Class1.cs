using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calcdll
{
    public class Age
    {
        public object Aging { get; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public int Years { get; set; }
        public int Month { get; set; }
        public int Days { get; set; }

        public int TotalYears { get { return GetYear(start, end); } }
        public int TotalMonths { get { return GetMonth(start, end); } }
        public int TotalDays { get { return GetDay(start, end); } }
        public static void GetAge(DateTime start, DateTime end)
        {
            DateTime current = start;
            TimeSpan dateDiff = end - start;
            int maxYear = end.Year;

            int yearCount = 0;
            int monthCount = 0;
            int dayCount = 0;

            //year
            for (int year = current.Year; year < maxYear; year++)
            {
                bool isLeapYear = DateTime.IsLeapYear(current.Year);
                if (((isLeapYear && (dateDiff.TotalDays >= 366)) ||
                            (!isLeapYear && (dateDiff.TotalDays >= 365))))
                {
                    current = current.AddYears(1);
                    yearCount++;
                }
            }

            //month
            for (int month = current.Month; month <= ((current.Year == end.Year) ? end.Month : 12);)
            {
                if (current.Month != (end.Month))
                {
                    if ((dateDiff.Days >= DateTime.DaysInMonth(current.Year, current.Month)))
                    {
                        current = current.AddMonths(1);
                        monthCount++;
                        dateDiff = end - current;
                    }

                }
                if (current.Year == end.Year && month == end.Month - 1)
                {
                    break;
                }
                else
                {
                    month = ((month + 1) % 12);
                }
            }

            //day
            dayCount = (end - current).Days;

            return;
        }

        public static int GetYear(DateTime start, DateTime end)
        {
            int yearCount = 0;
            for (int year = start.Year; year < end.Year; year++)
            {
                bool isLeapYear = DateTime.IsLeapYear(start.Year);
                if (((isLeapYear && ((end - start).TotalDays >= 366)) ||
                            (!isLeapYear && ((end - start).TotalDays >= 365))))
                {
                    start = start.AddYears(1);
                    yearCount++;
                }
            }
            return yearCount;
        }

        public static int GetMonth(DateTime start, DateTime? End = null)
        {
            DateTime end = (End == null ? DateTime.Now : (DateTime)End);
            TimeSpan dateDiff = end - start;
            DateTime current = start;
            int monthCount = 0;
            for (int month = current.Month; month <= ((current.Year == end.Year) ? end.Month : 12);)
            {
                if (((current.Year == end.Year) && (current.Month != (end.Month))) || ((current.Year < end.Year)))
                {
                    if ((dateDiff.Days >= DateTime.DaysInMonth(current.Year, current.Month)) || (current.Year <= end.Year))
                    {
                        current = current.AddMonths(1);
                        monthCount++;
                        dateDiff = end - current;
                    }
                }
                if (current.Year == end.Year && month == end.Month - 1)
                {
                    break;
                }
                else
                {
                    month = ((month % 12) + 1);
                }
            }

            return monthCount;
        }

        public static int GetDay(DateTime start, DateTime? End = null)
        {
            return ((DateTime)End - start).Days;
        }
    }
}
