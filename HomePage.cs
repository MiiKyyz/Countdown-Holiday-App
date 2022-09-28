using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using AndroidX.AppCompat.App;
using Android.Views.Animations;
using Android.Animation;
using Android.Graphics.Drawables;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Util;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static Android.Icu.Text.AlphabeticIndex;
using static Android.Content.ClipData;
using Android.Graphics;
using System.Reflection;

namespace Holidays_CountDown_App
{
    [Activity(Label = "HomePage", Theme = "@style/AppTheme")]
    public class HomePage : AppCompatActivity
    {
        string holiday_name;
        RelativeLayout panel, back_panel;
        Button btn_Previous, btn_Next;
        ObjectAnimator anim_next, anim_previous;
        float duration = 1500f, Pager=0f, d=1000f;
        TextView holidays, counter_1, counter_2;
        ImageView img, back_img;
        DateTime final_day, now, date_wanted;
         
        HolidaysSchedule holidays_Time = new HolidaysSchedule();
        TimeSpan result;
        Dictionary<string, int> Holidays_List = new Dictionary<string, int>()
        {
            {"Christmas", Resource.Drawable.Chrismas_photo },
            {"Halloween", Resource.Drawable.Halloween_photo  },
            {"New Year", Resource.Drawable.New_year_photo  },
            {"Valentine's Day", Resource.Drawable.Valentine_s_day_photo },
            {"ThanksGiving", Resource.Drawable.ThanksGiving_photo  },
            {"Easter", Resource.Drawable.Easter_photo }


        };
        bool Panel_State;
        AnimationDrawable anim; 


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.home_page_layout);

            
            



            panel = FindViewById<RelativeLayout>(Resource.Id.Panel_Content);
            btn_Next = FindViewById<Button>(Resource.Id.button_Next);
            btn_Previous = FindViewById<Button>(Resource.Id.button_Previous);

            holidays = FindViewById<TextView>(Resource.Id.Holidays_txt);
            counter_1 = FindViewById<TextView>(Resource.Id.Counter_1_id);
            counter_2 = FindViewById<TextView>(Resource.Id.Counter_2_id);

            img = FindViewById<ImageView>(Resource.Id.Back_Image);
            back_img = FindViewById<ImageView>(Resource.Id.back_image_back);

            back_panel = FindViewById<RelativeLayout>(Resource.Id.back_panel);

            

            anim_next = ObjectAnimator.OfFloat(panel, "rotationY", 0, 360);
            anim_next.SetDuration((int)duration);

            anim_previous = ObjectAnimator.OfFloat(panel, "rotationY", 0, -360);
            anim_previous.SetDuration((int)duration);

            //TextFonts("LuckiestGuy-Regular.ttf", btn_Previous);
            //TextFonts("LuckiestGuy-Regular.ttf", btn_Next);

            Typeface txt = Typeface.CreateFromAsset(Assets, "LuckiestGuy-Regular.ttf");

            btn_Previous.SetTypeface(txt, TypefaceStyle.Normal);


            

            btn_Next.SetTypeface(txt, TypefaceStyle.Normal);


            btn_Next.Click += Btn_Next_Click;
            btn_Previous.Click += Btn_Previous_Click;
            SwitherPanel();
            



            BackAnimation();



            Time();

        }

        public override void OnBackPressed(){}

        public void BackAnimation()
        {


            back_img.SetBackgroundResource(Resource.Drawable.animator_background);

            anim = (AnimationDrawable)back_img.Background;

            anim.SetEnterFadeDuration(1000);
            anim.SetExitFadeDuration(1000);

            

            anim.Start();



        }

        

        private void Btn_Previous_Click(object sender, EventArgs e)
        {
            if(Pager > 0 && Pager <= 5)
            {

                if (!anim_previous.IsRunning)
                {
                    Panel_State = true;
                    anim_previous.Start();
                    ChangerPanelDelay((int)d);
                    Pager -= 1;
                    
                }
            }

            
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {


            if (Pager >= 0 && Pager < 5)
            {
                if (!anim_next.IsRunning)
                {


                    Panel_State = false;
                    anim_next.Start();
                    ChangerPanelDelay((int)d);
                    
                    Pager += 1;

                }

            }

        }

        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }

        private void ChangerPanelDelay(int time)
        {

            Task startup = new Task(() =>
            {
                Thread.Sleep(time);
            });


            startup.ContinueWith(t =>
            {


                PanelChanger(Panel_State);




            }, TaskScheduler.FromCurrentSynchronizationContext());
            startup.Start();

        }
        private void ItemHoliday(string holiday)
        {
            img.SetBackgroundResource(Holidays_List[holiday]);
            foreach (var item in Holidays_List.Keys)
            {

                if (holiday == item)
                {
                    holidays.Text = item;
                
                    
               
                }

            }
        }

        private void SwitherPanel()
        {
            
            switch (Pager)
            {

                case 0:
                    ItemHoliday("Christmas");
                    holiday_name = "Christmas";
                    TextFonts("CartoonBlocksChristmas.otf", counter_2);
                    TextFonts("CartoonBlocksChristmas.otf", counter_1);
                    TextFonts("CartoonBlocksChristmas.otf", holidays);

                    counter_2.TextSize = 30;
                    counter_1.TextSize = 30;
                    holidays.TextSize = 30;

                    counter_2.SetTextColor(Color.ParseColor("#8c0000"));
                    counter_1.SetTextColor(Color.ParseColor("#8c0000"));
                    holidays.SetTextColor(Color.ParseColor("#8c0000"));



                    break;
                case 1:
                    ItemHoliday("Halloween");
                    holiday_name = "Halloween";
                    TextFonts("HalloweenMorning.ttf", counter_1);
                    TextFonts("HalloweenMorning.ttf", counter_2);
                    TextFonts("HalloweenMorning.ttf", holidays);


                    counter_2.TextSize = 30;
                    counter_1.TextSize = 30;
                    holidays.TextSize = 30;

                    counter_1.SetTextColor(Color.OrangeRed);
                    counter_2.SetTextColor(Color.OrangeRed);
                    holidays.SetTextColor(Color.OrangeRed);

                    break;
                case 2:
                    ItemHoliday("New Year");
                    holiday_name = "New Year";
                    TextFonts("HappyNewYear.ttf", counter_1);
                    TextFonts("HappyNewYear.ttf", counter_2);
                    TextFonts("HappyNewYear.ttf", holidays);

                    counter_1.TextSize = 45;
                    counter_2.TextSize = 45;
                    holidays.TextSize = 45;


                    counter_1.SetTextColor(Color.ParseColor("#2b1e00"));
                    counter_2.SetTextColor(Color.ParseColor("#2b1e00"));
                    holidays.SetTextColor(Color.ParseColor("#2b1e00"));

                    break;
                case 3:
                    ItemHoliday("Valentine's Day");
                    holiday_name = "Valentine's Day";

                    TextFonts("valentine_cute.ttf", counter_1);
                    TextFonts("valentine_cute.ttf", counter_2);
                    TextFonts("valentine_cute.ttf", holidays);

                    counter_1.TextSize = 15;
                    counter_2.TextSize = 15;
                    holidays.TextSize = 15;

                    counter_1.SetTextColor(Color.ParseColor("#ff00d0"));
                    counter_2.SetTextColor(Color.ParseColor("#ff00d0"));
                    holidays.SetTextColor(Color.ParseColor("#ff00d0"));

                    break;
                case 4:
                    ItemHoliday("ThanksGiving");
                    holiday_name = "ThanksGiving";

                    TextFonts("Edbindia.ttf", counter_1);
                    TextFonts("Edbindia.ttf", counter_2);
                    TextFonts("Edbindia.ttf", holidays);


                    counter_1.TextSize = 25;
                    counter_2.TextSize = 25;
                    holidays.TextSize = 25;



                    counter_1.SetTextColor(Color.DarkOrange);
                    counter_2.SetTextColor(Color.DarkOrange);
                    holidays.SetTextColor(Color.DarkOrange);

                    break;
                case 5:
                    ItemHoliday("Easter");
                    holiday_name = "Easter";

                    TextFonts("AlumniSansCollegiateOne-Italic.ttf", counter_1);
                    TextFonts("AlumniSansCollegiateOne-Italic.ttf", counter_2);
                    TextFonts("AlumniSansCollegiateOne-Italic.ttf", holidays);

                    counter_1.TextSize = 30;
                    counter_2.TextSize = 30;
                    holidays.TextSize = 30;


                    counter_1.SetTextColor(Color.ParseColor("#473700"));
                    counter_2.SetTextColor(Color.ParseColor("#473700"));
                    holidays.SetTextColor(Color.ParseColor("#473700"));



               
                    break;


            }
        }

        private void PanelChanger(bool Panel_State)
        {

            if (Panel_State)
            {
                

                SwitherPanel();
             

            }
            else if(!Panel_State)
            {
                SwitherPanel();
       
            }


        }



        public async Task Time()
        {
            final_day = new DateTime(DateTime.Now.Year, 12, 31);
     
            await Task.Run(() =>
            {

                while (true)
                {
                    //Log.Info("miky", "miky");
                    now = DateTime.Now;
                    date_wanted = new DateTime(holidays_Time.SetNameHoliday(holiday_name)[0],
                        holidays_Time.SetNameHoliday(holiday_name)[1],
                        holidays_Time.SetNameHoliday(holiday_name)[2],
                        holidays_Time.SetNameHoliday(holiday_name)[3],
                        holidays_Time.SetNameHoliday(holiday_name)[4],
                        holidays_Time.SetNameHoliday(holiday_name)[5]);
                    result = date_wanted - now;


                    int hour = result.Hours, days = result.Days, minutes= result.Minutes, seconds= result.Seconds;




                    Task.Delay(900);
                    //counter.Text = $"days left: {result.Days} \n Time Remaining: \n {result.Hours}:{result.Minutes}:{result.Seconds}\n ";
                    if (days < 0 && hour < 0 && minutes < 0 && seconds < 0)
                    {
                        try
                        {
                            counter_1.Text = "days left: " + (days + final_day.DayOfYear).ToString();

                            counter_2.Text = "Time Remaining:" + " \n" + (hour + 24).ToString() + ":" + (minutes + 59).ToString() + ":" + (seconds + 59).ToString();

                        }
                        catch
                        {


                        }
                        
                    }
                    else if (days >= 0 && hour >= 0 && minutes >= 0 && seconds >= 0)
                    {

                        //counter.Text = $"days left: {result.Days} \n Time Remaining: \n {result.Hours}:{result.Minutes}:{result.Seconds}\n ";
                        //counter_1.Text = "days left:" + (result.Days) + "\n" + "Time Remaining:" + " \n" + (result.Hours) + ":" + (result.Minutes ) + ":" + (result.Seconds) + "\n";
                        
                        try
                        {
                            counter_1.Text = "days left: " + (days).ToString();

                            counter_2.Text = "Time Remaining:" + " \n" + hour.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString();

                        }
                        catch
                        {

                        }

                    }
                    
                }
            });
            

        }
    }
}


