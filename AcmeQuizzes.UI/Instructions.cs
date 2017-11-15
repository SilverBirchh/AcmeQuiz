
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "Instructions")]
    public class Instructions : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "Instructions" layout resource
            SetContentView(Resource.Layout.Instructions);

            // Attach Click event to go to home page
            Button Home = FindViewById<Button>(Resource.Id.GoHome);

            Home.Click += delegate
            {
                Intent GoHome = new Intent(this, typeof(MainActivity));
                StartActivity(GoHome);
            };

        }
    }
}
