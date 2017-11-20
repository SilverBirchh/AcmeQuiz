using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "Instructions")]
    public class InstructionsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "Instructions" layout resource
            SetContentView(Resource.Layout.Instructions);

            // Grab the button on the page
            Button HomeBtn = FindViewById<Button>(Resource.Id.GoHome);

            // Attach Click event to go to home page
            HomeBtn.Click += delegate
            {
                Intent GoHome = new Intent(this, typeof(MainActivity));
                StartActivity(GoHome);
            };

        }
    }
}
