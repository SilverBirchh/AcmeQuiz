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
            Button startAgainBtn = FindViewById<Button>(Resource.Id.again);
            Button goHomeBtn = FindViewById<Button>(Resource.Id.home);
            Button goReviewBtn = FindViewById<Button>(Resource.Id.review);
            TextView scoreView = FindViewById<TextView>(Resource.Id.score);

            // Work out the amount of question asked and how many were correct
            // This is done by a the size of AnsweredQuestions List. This is then
            // filtered to keep only the questions that were correct. 
            // By the KeyValuePair Value being `true`. A count of these is taken
            int amountOfQuestions = QuizManager.answeredQuestions.Count();
            int correctAnswers = QuizManager.answeredQuestions.Where(kvp => kvp.Value).Count();

            // Set the score
            scoreView.Text = $"{correctAnswers} / {amountOfQuestions}";

            // Create Click handler to take the user to the PreQuiz page
            startAgainBtn.Click += delegate
            {
                Intent againIntent = new Intent(this, typeof(PreQuizActivity));
                StartActivity(againIntent);
            };

            // Create Click handler to take the user to the home page
            goHomeBtn.Click += delegate
            {
                Intent homeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(homeIntent);
            };

            // Create Click handler to take the user to the review page
            goReviewBtn.Click += delegate
            {
                Intent reviewIntent = new Intent(this, typeof(ReviewActivity));
                StartActivity(reviewIntent);
            };
        }
    }
}
