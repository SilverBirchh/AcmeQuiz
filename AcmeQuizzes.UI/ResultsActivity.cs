using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "ResultsActivity")]
    public class ResultsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Finish);

            Button StartAgain = FindViewById<Button>(Resource.Id.again);
            Button GoHome = FindViewById<Button>(Resource.Id.home);
            Button GoReview = FindViewById<Button>(Resource.Id.review);
            TextView Score = FindViewById<TextView>(Resource.Id.score);

            // Work out the mount of question asked and how many were correct
            int AmountOfQuestions = QuizManager.AnsweredQuestions.Count();
            int CorrectAnswers = QuizManager.AnsweredQuestions.Where(kvp => kvp.Value).Count();

            Score.Text = $"{CorrectAnswers} / {AmountOfQuestions}";

            // Create Click handler to take the user to the PreQuiz page
            StartAgain.Click += delegate
            {
                Intent AgainIntent = new Intent(this, typeof(PreQuizActivity));
                StartActivity(AgainIntent);
            };

            // Create Click handler to take the user to the PreQuiz page
            GoHome.Click += delegate
            {
                Intent HomeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(HomeIntent);
            };

            GoReview.Click += delegate
            {
                Intent ReviewIntent = new Intent(this, typeof(ReviewActivity));
                StartActivity(ReviewIntent);
            };
        }
    }
}
