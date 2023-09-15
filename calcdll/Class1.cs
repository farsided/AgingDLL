using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calcdll
{
    public class Samp
    {
        public static void Main()
        {
            Age s = new Age(DateTime.Parse("July 24, 1998"));
            Console.WriteLine($"{s.Years} {s.Months} {s.Days}");
            Console.ReadLine();
        }
    }
    public enum OutputFormat
    {
        UseComma, UseAmpersand, UseAndWord
    }

    public class Age
    {
        private DateTime start { get; set; }
        private DateTime end { get; set; }
        private DateTime current { get; set; }

        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }

        public int TotalYears { get { return GetYear(start, end); } }
        public int TotalMonths { get { return GetMonth(start, end); } }
        public int TotalDays { get { return GetDay(start, end); } }

        public Age()
        {

        }

        public string DurationToWord(DateTime Start, DateTime? End = null, OutputFormat Format = OutputFormat.UseComma, bool Longstring = true)
        {
            DateTime end = (End ?? DateTime.Now);
            Init(Start, end);
            var output = "";
            switch (Format)
            {
                case OutputFormat.UseComma:
                    output = Longstring ? $"{Years} year(s), {Months} month(s), {Days} day(s)" : $"{Years} year(s), {Months} month(s)";
                    break;
                case OutputFormat.UseAmpersand:
                    output = Longstring ? $"{Years} year(s) & {Months} month(s) & {Days} day(s)" : $"{Years} year(s) & {Months} month(s)";
                    break;
                case OutputFormat.UseAndWord:
                    output = Longstring ? $"{Years} year(s) and {Months} month(s) and {Days} day(s)" : $"{Years} year(s) and {Months} month(s)";
                    break;
            }
            return output;
        }

        public Age(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            Init(start, end);
        }

        public void Init(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            TimeSpan dateDiff = end - start;
            current = start;

            this.Years = GetYear(current, end);
            this.Months = GetMonth(current, end);
            this.Days = GetDay(current, end);
        }

        private int GetYear(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            int yearCount = 0;
            for (int year = start.Year; year < end.Year; year++)
            {
                bool isLeapYear = DateTime.IsLeapYear(start.Year);
                if (((isLeapYear && ((end - start).TotalDays >= 366)) || (!isLeapYear && ((end - start).TotalDays >= 365))))
                {
                    start = start.AddYears(1);
                    current = start;
                    yearCount++;
                }
            }
            return yearCount;
        }

        private int GetMonth(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            TimeSpan dateDiff = end - start;
            current = start;
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

        private int GetDay(DateTime start, DateTime? End = null)
        {
            current = current.AddDays(((DateTime)End - start).Days);
            return ((DateTime)End - start).Days;
        }
    }
}
