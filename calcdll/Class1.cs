using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcDll;


namespace CalcDll
{
    /// <summary>
    /// <para>
    /// A class that retrieves timespan relative to the Gregorian Calendar's common and leap years and months with varrying day counts.
    /// </para>
    /// </summary>
    public class Age
    {
        /// <summary>
        /// DateTime property with vlaue less than or equal to DateTime 'end' property
        /// </summary>
        private DateTime start { get; set; }
        /// <summary>
        /// DateTime property with vlaue greater than or equal to DateTime 'start' property
        /// </summary>
        private DateTime end { get; set; }
        /// <summary>
        /// DateTime property with value where the day counter ends
        /// </summary>
        private DateTime current { get; set; }
        /// <summary>
        /// Usual timespan between two dates, here is (end- start)
        /// </summary>
        public TimeSpan diff { get { return (end - start); } }

        /// <summary>
        /// Integer part of the year in the timespan between the DateTime 'start' and 'end' properties.
        /// </summary>
        public int Years { get; set; }
        /// <summary>
        /// Integer part of to the month in the timespan between the DateTime 'start' and 'end' properties.
        /// </summary>
        public int Months { get; set; }
        /// <summary>
        /// Integer part of to the day in the timespan between the DateTime 'start' and 'end' properties.
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// Gets the start date defined from constructor Age(DateTime start, DateTime? End = null) or Init(DateTime start, DateTime? End = null)
        /// </summary>
        public DateTime Start { get { return start; } }
        /// <summary>
        /// Gets the end date defined from constructor Age(DateTime start, DateTime? End = null) or Init(DateTime start, DateTime? End = null)
        /// </summary>
        public DateTime End { get { return end; } }

        /// <summary>
        /// Evaluates to : Months / 4
        /// </summary>
        public int Quarters { get { return (Months / 4); } }
        /// <summary>
        /// Evaluates to : Days / 7
        /// </summary>
        public int Weeks { get { return Days / 7; } }
        /// <summary>
        /// Evaluates to : diff.Hours % 24
        /// </summary>
        public int Hours { get { return (diff.Hours % 24); } }
        /// <summary>
        /// Evaluates to : diff.Minutes % 60
        /// </summary>
        public int Minutes { get { return (diff.Minutes % 60); } }
        /// <summary>
        /// Evaluates to : diff.Seconds % 60
        /// </summary>
        public int Seconds { get { return (diff.Seconds % 60); } }

        /// <summary>
        /// Year value in time span betweem DateTime 'start' and 'end' properties disregarding other the object property values
        /// </summary>
        public int TotalYears { get { return GetYear(start, end); } }
        /// <summary>
        /// Month value in time span betweem DateTime 'start' and 'end' properties disregarding other the object property values
        /// </summary>
        public int TotalMonths { get { return GetMonth(start, end); } }
        /// <summary>
        /// Day value in time span betweem DateTime 'start' and 'end' properties disregarding other the object property values
        /// </summary>
        public int TotalDays { get { return GetDay(start, end); } }

        /// <summary>
        /// Count of no. of units included in the GetString() private member function
        /// </summary>
        public int UnitCount { get; set; }

        public Age()
        {

        }

        /// <summary>
        /// Initializes a new instance of the CalcDll.Age object with specified range of date and time and 
        /// evaluates the object's settable properties 'Years', 'Months' and 'Days'.
        /// </summary>
        /// <param name="start">
        /// A date and time value less than or equal to the parameter 'DateTime? end'
        /// </param>
        /// <param name="End">
        /// An optional date and time value greater than or equal to the parameter 'DateTime? start'.
        /// If null, will be equal to the current date and time.
        /// </param>
        /// <exception cref="start">
        /// Date out of range
        /// </exception>
        public Age(DateTime start, DateTime? End = null)
        {
            Init(start, End);
        }

        /// <summary>
        /// <para>
        /// Initializes the object's 'Years', 'Months' and 'Days' properties. Invoke to reevaluate properties.
        /// </para>
        /// </summary>
        /// <param name="start">
        /// A date and time value less than or equal to the parameter 'DateTime? end'
        /// </param>
        /// <param name="End">
        /// <br/>
        /// An optional date and time value greater than or equal to the parameter 'DateTime? start'.
        /// <br/>
        /// If null, will be equal to the current date and time.
        /// </param>
        private void Init(DateTime start, DateTime? End = null)
        {
            current = start;
            this.start = start;
            this.end = (End ?? DateTime.Now);

            Years = GetYear(current, end);
            Months = GetMonth(current, end);
            Days = GetDay(current, end);
        }

        /// <summary>
        /// The list of key-value pair of units in words and unit values
        /// </summary>
        public List<KeyValuePair<string, int>> TimeUnits
        {
            get
            {
                return new List<KeyValuePair<string, int>>(new List<KeyValuePair<string, int>>()
                    {
                        new KeyValuePair<string, int>("Year", Years),
                        new KeyValuePair<string, int>("Month", Months),
                        new KeyValuePair<string, int>("Week", Weeks),
                        new KeyValuePair<string, int>("Day", Days % 7),
                        new KeyValuePair<string, int>("Hour", Hours),
                        new KeyValuePair<string, int>("Minute", Minutes),
                        new KeyValuePair<string, int>("Second", Seconds)
                    }
                );
            }
        }

        /// <summary>
        /// The list of key-value pair of units in abbrev and unit values
        /// </summary>
        public virtual List<KeyValuePair<string, int>> TimeUnitsShort
        {
            get
            {
                return new List<KeyValuePair<string, int>>(new List<KeyValuePair<string, int>>()
                    {
                        new KeyValuePair<string, int>("Y", Years),
                        new KeyValuePair<string, int>("M", Months),
                        new KeyValuePair<string, int>("W", Weeks),
                        new KeyValuePair<string, int>("D", Days % 7),
                        new KeyValuePair<string, int>("h", Hours),
                        new KeyValuePair<string, int>("m", Minutes),
                        new KeyValuePair<string, int>("s", Seconds)
                    }
                );
            }
        }

        /// <summary>
        /// Sets the list of units and associate the values of properties with it in an english phrase
        /// </summary>
        /// <param name="unitCountMax"></param>
        /// <param name="suffix"></param>
        /// <param name="replacement"></param>
        /// <param name="separator"></param>
        /// <param name="unitCountMin"></param>
        /// <param name="unitLong"></param>
        /// <returns>
        /// String
        /// </returns>
        public string SetString(int unitCountMax, string suffix = "s", string replacement = "&", char separator = ',', int? unitCountMin = null, bool unitLong = true)
        {
            int unitAccumulator = 0;
            List<KeyValuePair<string, int>> timeUnits = unitLong ? TimeUnits : TimeUnitsShort;
            string str = InitializeString(unitCountMax, timeUnits, unitAccumulator: ref unitAccumulator, unitLong: unitLong);
            Compose(ref str, objCounter: unitAccumulator, replacement: replacement, separator: separator);
            UnitCount = unitAccumulator;
            return str;
        }

        /// <summary>
        /// Sets the list of units and associate the values of properties
        /// </summary>
        /// <param name="unitCountMax"></param>
        /// <param name="timeUnits"></param>
        /// <param name="unitAccumulator"></param>
        /// <param name="suffix"></param>
        /// <param name="separator"></param>
        /// <param name="unitCounter"></param>
        /// <param name="unitLong"></param>
        /// <returns></returns>
        private string InitializeString(int unitCountMax, List<KeyValuePair<string, int>> timeUnits, ref int unitAccumulator, string suffix = "s", char separator = ',', int unitCounter = 0, bool unitLong = true)
        {
            string str = null;
            for (; (unitCounter < timeUnits.Count) && (unitAccumulator < unitCountMax);)
            {
                if (timeUnits.ElementAt(unitCounter).Value > 0)
                {
                    str += $"{timeUnits.ElementAt(unitCounter).Value}{(unitLong ? " " : "")}{timeUnits.ElementAt(unitCounter).Key}{(timeUnits.ElementAt(unitCounter).Value > 1 ? (unitLong ? suffix : "") : "")}{separator.ToString()} ";
                    unitAccumulator++;
                }
                unitCounter++;
            }
            if (str.Length > 0)
            {
                str = str.Remove(str.Length - 2, 2);
            }
            return str;
        }

        /// <summary>
        /// Sets the list of units in an english phrase
        /// </summary>
        /// <param name="str"></param>
        /// <param name="objCounter"></param>
        /// <param name="replacement"></param>
        /// <param name="separator"></param>
        private void Compose(ref string str, int objCounter, string replacement = "&", char separator = ',')
        {
            if (objCounter > 1)
            {
                str = str.Insert(str.LastIndexOf(separator) + 1, " " + replacement);
                str = str.Remove(str.LastIndexOf(separator), 1);
            }
        }


        public int GetYear(DateTime start, DateTime? End = null)
        {
            DateTime end = (End ?? DateTime.Now);
            int yearCount = 0;
            for (int year = start.Year; year < end.Year; year++)
            {
                bool isLeapYear = DateTime.IsLeapYear(start.Year);
                if (((isLeapYear && ((end - start).TotalDays >= 366)) ||
                            (!isLeapYear && ((end - start).TotalDays >= 365))))
                {
                    start = start.AddYears(1);
                    current = start;
                    yearCount++;
                }
            }
            return yearCount;
        }

        public int GetMonth(DateTime start, DateTime? End = null)
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
                        if (current < end.AddMonths(-1))
                        {
                            current = current.AddMonths(1);
                            dateDiff = end - current;
                            monthCount++;
                        }
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

        public int GetDay(DateTime start, DateTime? End = null)
        {
            int dayDiff = ((DateTime)End - start).Days;
            current = current.AddDays(dayDiff);
            return dayDiff;
        }
    }
}
