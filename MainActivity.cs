using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Content;
using Android.Util;
using System.IO;
using System;
using System.Threading.Tasks;
using static Android.Icu.Text.AlphabeticIndex;
using System.Threading;
using Android.Graphics;
using Java.Lang;
using Thread = Java.Lang.Thread;
using Android.Views.Animations;
using Android.Animation;
using System.Reflection;

namespace Holidays_CountDown_App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/Logo")]
    public class MainActivity : AppCompatActivity
    {

        int[] Images_Splash = {Resource.Drawable.Splash_1, Resource.Drawable.Splash_2,
       Resource.Drawable.Splash_3,Resource.Drawable.Splash_4,Resource.Drawable.Splash_5,
       Resource.Drawable.Splash_6,Resource.Drawable.Splash_7,Resource.Drawable.Splash_8};

        Random random;
        ImageView img, title;
        private TextView textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10, textView11,
                textView12, textView13, textView14, textView15, textView16;
       

        TextView[] list_texts = {};


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            
            //TimeAnimation();




            list_texts = new TextView[16] {textView1, textView2, textView3, textView4, textView5, textView6, textView7, textView8, textView9, textView10, textView11,
                textView12, textView13, textView14, textView15, textView16 };
            img = FindViewById<ImageView>(Resource.Id.Image_Splash);
            //title = FindViewById<ImageView>(Resource.Id.imageView1);


            list_texts[0] = FindViewById<TextView>(Resource.Id.textView1);
            list_texts[1] = FindViewById<TextView>(Resource.Id.textView2);
            list_texts[2] = FindViewById<TextView>(Resource.Id.textView3);
            list_texts[3] = FindViewById<TextView>(Resource.Id.textView4);
            list_texts[4] = FindViewById<TextView>(Resource.Id.textView5);
            list_texts[5] = FindViewById<TextView>(Resource.Id.textView6);
            list_texts[6] = FindViewById<TextView>(Resource.Id.textView7);
            list_texts[7] = FindViewById<TextView>(Resource.Id.textView8);
            list_texts[8] = FindViewById<TextView>(Resource.Id.textView9);
            list_texts[9] = FindViewById<TextView>(Resource.Id.textView10);
            list_texts[10] = FindViewById<TextView>(Resource.Id.textView11);
            list_texts[11] = FindViewById<TextView>(Resource.Id.textView12);
            list_texts[12] = FindViewById<TextView>(Resource.Id.textView13);
            list_texts[13] = FindViewById<TextView>(Resource.Id.textView14);
            list_texts[14] = FindViewById<TextView>(Resource.Id.textView15);
            list_texts[15] = FindViewById<TextView>(Resource.Id.textView16);


            foreach (TextView txt in list_texts)
            {

                txt.Rotation = 180;
                txt.SetBackgroundResource(Resource.Drawable.text_back_front);
                TextFonts("LuckiestGuy-Regular.ttf", txt);
            }


            ImageVhanger();

            TitleAnimation();
            ToNext();
        }

        public void TextFonts(string FontType, TextView txt_fonts)
        {

            Typeface txt = Typeface.CreateFromAsset(Assets, FontType);

            txt_fonts.SetTypeface(txt, TypefaceStyle.Normal);

        }
        public void TitleAnimation()
        {
            int timer = 3000;

            foreach(TextView img in list_texts)
            {

                ObjectAnimator anim_x = ObjectAnimator.OfFloat(img, "rotationX", 0, 180);
                anim_x.SetDuration(timer);

                ObjectAnimator anim_y = ObjectAnimator.OfFloat(img, "rotationY", 0, 180);
                anim_y.SetDuration(timer);

                AnimatorSet animationSet = new AnimatorSet();

                animationSet.PlayTogether(anim_x, anim_y);



                animationSet.Start();

                timer += 100;
            }

        }

        private void ImageVhanger()
        {


            random = new Random();

            int number = random.Next(0, 7);

            img.SetBackgroundResource(Images_Splash[number]);
        }

        public Task ToNext()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task startup = new Task(() =>
            {
                Thread.Sleep(5000);
            });


             startup.ContinueWith(t =>
            {

                StartActivity(new Intent(Application.Context, typeof(HomePage)));
                tokenSource.Cancel();

            },  tokenSource.Token);
            startup.Start();
            
            return Task.CompletedTask;

        }
     


        /*LayoutInflater inflarter = (LayoutInflater)
                this.GetSystemService(Context.LayoutInflaterService);

        View view = inflarter.Inflate(Resource.Layout.ToastStyleable,
            (ViewGroup)FindViewById(Resource.Layout.ToastStyleable), true);*/

    }
}