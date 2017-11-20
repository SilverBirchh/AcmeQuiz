using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "Results")]
    public class ResultsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the layout to finish
            SetContentView(Resource.Layout.Finish);

            // Grab the controls on the screen and the text view to show the score
            Button StartAgainBtn = FindViewById<Button>(Resource.Id.again);
            Button GoHomeBtn = FindViewById<Button>(Resource.Id.home);
            Button GoReviewBtn = FindViewById<Button>(Resource.Id.review);
            TextView ScoreView = FindViewById<TextView>(Resource.Id.score);

            // Work out the amount of question asked and how many were correct
            // This is done by a the size of AnsweredQuestions List. This is then
            // filtered to keep only the questions that were correct. 
            // By the KeyValuePair Value being `true`. A count of these is taken
            int AmountOfQuestions = QuizManager.AnsweredQuestions.Count();
            int CorrectAnswers = QuizManager.AnsweredQuestions.Where(kvp => kvp.Value).Count();

            // Set the score
            ScoreView.Text = $"{CorrectAnswers} / {AmountOfQuestions}";

            // Create Click handler to take the user to the PreQuiz page
            StartAgainBtn.Click += delegate
            {
                Intent AgainIntent = new Intent(this, typeof(PreQuizActivity));
                StartActivity(AgainIntent);
            };

            // Create Click handler to take the user to the home page
            GoHomeBtn.Click += delegate
            {
                Intent HomeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(HomeIntent);
            };

            // Create Click handler to take the user to the review page
            GoReviewBtn.Click += delegate
            {
                Intent ReviewIntent = new Intent(this, typeof(ReviewActivity));
                StartActivity(ReviewIntent);
            };
        }
    }
}
