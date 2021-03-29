// --------------------------------------------------------------------
// Copyright (c) 2005-2020 Arad ITC.
//
// Author : Ammar Heidari <ammar@arad-itc.org>
// Licensed under the Apache License, Version 2.0 (the "License")
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0 
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// --------------------------------------------------------------------

using System;
using System.Globalization;

namespace Arad.SMS.Gateway.GeneralLibrary
{
    public class DateManager
    {
        private DateManager(DateTime dateTime)
        {
            this.OrginalDateTime = dateTime;
        }

        #region	 Instance Variables And Methods
        private DateTime OrginalDateTime
        {
            get;
            set;
        }

        public int Day
        {
            get;
            private set;
        }

        public int Month
        {
            get;
            private set;
        }

        public int Year
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return GetDisplayDateTime(OrginalDateTime);
        }
        #endregion

        public static string GetToday()
        {
            if (Language.ActiveLanguage == Language.AvalibaleLanguages.fa)
                return Persia.Calendar.ConvertToPersian(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Simple;
            else
                return DateTime.Now.ToShortDateString();
        }

        public static DateManager GetTodayDateObject()
        {
            return GetDispalyDateObject(DateTime.Now);
        }

        public static string GetTime(DateTime dateTime)
        {
            return string.Format("{0}:{1}:{2}",
                                                                    dateTime.Hour < 10 ? "0" + Helper.GetString(dateTime.Hour) : Helper.GetString(dateTime.Hour),
                                                                    dateTime.Minute < 10 ? "0" + Helper.GetString(dateTime.Minute) : Helper.GetString(dateTime.Minute),
                                                                    dateTime.Second < 10 ? "0" + Helper.GetString(dateTime.Second) : Helper.GetString(dateTime.Second)
                                                                );
        }

        public static bool PersianDateIsValid(string date, out DateTime christianDateTime)
        {
            PersianCalendar persianDate = new PersianCalendar();
            DateTime myDateContainer = new DateTime();
            Boolean dateCreated = false;
            try
            {
                int timePosition = date.IndexOf(" ");
                if (timePosition != -1)
                    myDateContainer = GetChristianDateTimeForDB(date);//persianDate.ToDateTime(year, month, day, 0, 0, 0, 0);
                else
                    myDateContainer = GetChristianDateForDB(date);
                dateCreated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            christianDateTime = myDateContainer;

            return dateCreated;
        }

        public static string GetChristianDate(string strDate)
        {
            string[] datePart = strDate.Split('/');
            int month = Helper.GetInt(datePart[1]);
            int day = Helper.GetInt(datePart[2]);
            int year = Helper.GetInt(datePart[0]);
            string date = Persia.Calendar.ConvertToGregorian(year, month, day).ToString();
            return date.Substring(0, date.IndexOf(" "));
        }

        public static string GetChristianDateTime(string strDateTime)
        {
            string[] datePart = strDateTime.Substring(0, strDateTime.IndexOf(" ")).Split('/');
            string time = strDateTime.Substring(strDateTime.IndexOf(" ") + 1);

            int month = Helper.GetInt(datePart[1]);
            int day = Helper.GetInt(datePart[2]);
            int year = Helper.GetInt(datePart[0]);

            string date = Persia.Calendar.ConvertToGregorian(year, month, day).ToString();
            return date.Substring(0, date.IndexOf(" ")) + " " + Helper.ConvertNumbers(time);
        }

        public static DateTime GetChristianDateForDB(string strDate)
        {
            if (strDate == string.Empty)
                return DateTime.MinValue;

            return Convert.ToDateTime(GetChristianDate(strDate));
        }

        public static DateTime GetChristianDateTimeForDB(string strDateTime)
        {
            if (strDateTime == string.Empty)
                return DateTime.MinValue;

            return Convert.ToDateTime(GetChristianDateTime(strDateTime));
        }

        public static bool ValidateSolarDate(string strdate)
        {
            try
            {
                if (Helper.GetInt(strdate.Substring(5, 2)) > 12 || Helper.GetInt(strdate.Substring(8, 2)) > 31)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateManager GetDispalyDateObject(DateTime dateTime)
        {
            DateManager dateManager = new DateManager(dateTime);

            if (Language.ActiveLanguage == Language.AvalibaleLanguages.fa)
            {
                var date = Persia.Calendar.ConvertToPersian(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).ArrayType;

                dateManager.Year = date[0];
                dateManager.Month = date[1];
                dateManager.Day = date[2];
            }
            else
            {
                var date = DateTime.Now;

                dateManager.Year = date.Year;
                dateManager.Month = date.Month;
                dateManager.Day = date.Day;
            }

            return dateManager;
        }

        #region Date Formatter
        #region Christian Date
        public static string GetChristianDate(DateTime date)
        {
            return string.Format("{0}/{1}/{2}",
                                                    date.Year,
                                                    (date.Month < 10 ? "0" + Helper.GetString(date.Month) : Helper.GetString(date.Month)),
                                                    (date.Day < 10 ? "0" + Helper.GetString(date.Day) : Helper.GetString(date.Day)));
        }

        public static string GetChristianDateTime(DateTime dateTime)
        {
            return string.Format("{0} {1}", GetChristianDate(dateTime), GetTime(dateTime));
        }

        public static string GetChristianDetailedDate(DateTime date)
        {
            return date.ToLongDateString();
        }

        public static string GetChristianDetailedDateTime(DateTime dateTime)
        {
            return string.Format("{0} {1}", GetChristianDetailedDate(dateTime), GetTime(dateTime));
        }
        #endregion

        #region Solar Date
        public static string GetSolarDate(DateTime date)
        {
            int month = Helper.GetInt(date.Month);
            int day = Helper.GetInt(date.Day);
            int year = Helper.GetInt(date.Year);

            int[] solarDate = Persia.Calendar.ConvertToPersian(year, month, day).ArrayType;

            return string.Format("{0}/{1}/{2}",
                                                    solarDate[0],
                                                    (solarDate[1] < 10 ? "0" + Helper.GetString(solarDate[1]) : Helper.GetString(solarDate[1])),
                                                    (solarDate[2] < 10 ? "0" + Helper.GetString(solarDate[2]) : Helper.GetString(solarDate[2])));
        }

        public static string GetSolarDateTime(DateTime dateTime)
        {
            return string.Format("{0} {1}", GetSolarDate(dateTime), GetTime(dateTime));
        }

        public static string GetSolarDetailedDate(DateTime date)
        {
            int month = Helper.GetInt(date.Month);
            int day = Helper.GetInt(date.Day);
            int year = Helper.GetInt(date.Year);

            return Persia.Calendar.ConvertToPersian(year, month, day).Weekday;
        }

        public static string GetSolarDetailedDateTime(DateTime dateTime)
        {
            return string.Format("{0} {1}", GetSolarDetailedDate(dateTime), GetTime(dateTime));
        }
        #endregion

        #region Lunar Date
        public static string GetLunarDate(DateTime date)
        {
            int month = Helper.GetInt(date.Month);
            int day = Helper.GetInt(date.Day);
            int year = Helper.GetInt(date.Year);

            int[] lunarDate = Persia.Calendar.ConvertToIslamic(year, month, day).ArrayType;

            return string.Format("{0}/{1}/{2}",
                                                    lunarDate[0],
                                                    (lunarDate[1] < 10 ? "0" + Helper.GetString(lunarDate[1]) : Helper.GetString(lunarDate[1])),
                                                    (lunarDate[2] < 10 ? "0" + Helper.GetString(lunarDate[2]) : Helper.GetString(lunarDate[2])));
        }

        public static string GetLunarDateTime(DateTime dateTime)
        {
            return string.Format("{0} {1}", GetLunarDate(dateTime), GetTime(dateTime));
        }

        public static string GetLunarDetailedDate(DateTime date)
        {
            int month = Helper.GetInt(date.Month);
            int day = Helper.GetInt(date.Day);
            int year = Helper.GetInt(date.Year);

            return Persia.Calendar.ConvertToIslamic(year, month, day).Formal;
        }

        public static string GetLunarDetailedDateTime(DateTime dateTime)
        {
            return string.Format("{0} {1}", GetLunarDetailedDate(dateTime), GetTime(dateTime));
        }
        #endregion

        public static string GetDisplayDate(DateTime date)
        {
            if (Language.ActiveLanguage == Language.AvalibaleLanguages.fa)
                return GetSolarDate(date);
            else
                return GetChristianDate(date);
        }

        public static string GetDisplayDateTime(DateTime dateTime)
        {
            if (Language.ActiveLanguage == Language.AvalibaleLanguages.fa)
                return GetSolarDateTime(dateTime);
            else
                return GetChristianDateTime(dateTime);
        }

        public static string GetDisplayDetailedDate(DateTime date)
        {
            if (Language.ActiveLanguage == Language.AvalibaleLanguages.fa)
                return GetSolarDetailedDate(date);
            else
                return GetChristianDetailedDate(date);
        }

        public static string GetDisplayDetailedDateTime(DateTime dateTime)
        {
            if (Language.ActiveLanguage == Language.AvalibaleLanguages.fa)
                return GetSolarDetailedDateTime(dateTime);
            else
                return GetChristianDetailedDateTime(dateTime);
        }
        #endregion
    }
}
