using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Icu.Text.AlphabeticIndex;

namespace Holidays_CountDown_App
{
    public class HolidaysSchedule 
    {


       
        float year,month, day, hour, minute, second;
       

      

        public float ThanksGivingDay()
        {


            List<float> date_list = new List<float>();


            for (int i = 1; i <= 30; i++)
            {
                DateTime date = new DateTime(DateTime.Now.Year, 11, i);
                if (date.DayOfWeek.ToString() == "Thursday")
                {
                    date_list.Add(i);
                }

            }




            float day = date_list.Max();

            return day;

        }

        public int[] SetNameHoliday(string holiday_names)
        {

            switch (holiday_names)
            {

                case "Christmas":
                    year = DateTime.Now.Year;
                    month = 12;
                    day = 25;
                    hour = 12;
                    minute = 0;
                    second = 0;
                    break;
                case "Halloween":
                    year = DateTime.Now.Year;
                    month = 10;
                    day = 31;
                    hour = 12;
                    minute = 0;
                    second = 0;
                    break;
                case "New Year":
                    year = DateTime.Now.Year;
                    month = 12;
                    day = 31;
                    hour = 12;
                    minute = 0;
                    second = 0;
                    break;
                case "Valentine's Day":
                    year = DateTime.Now.Year;
                    month = 2;
                    day = 14;
                    hour = 12;
                    minute = 0;
                    second = 0;
                    break;
                case "ThanksGiving":
                    year = DateTime.Now.Year;
                    month = 11;
                    day = (int)ThanksGivingDay();
                    hour = 16;
                    minute = 0;
                    second = 0;
                    break;
                case "Easter":
                    year = DateTime.Now.Year;
                    month = 4;
                    day = 9;
                    hour = 17;
                    minute = 0;
                    second = 0;
                    break;



            }
            int[] lista = { (int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second };
            return lista;

         
        }
    }
}