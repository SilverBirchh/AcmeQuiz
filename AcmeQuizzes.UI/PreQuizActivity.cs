using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "Number of Questions")]
    public class PreQuizActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "PreQuiz" layout resource
            SetContentView(Resource.Layout.PreQuiz);

            // Set the min / max values of the Number Picker
            NumberPicker NumberOfQuestion = FindViewById<NumberPicker>(Resource.Id.NumberOfQuestion);
            NumberOfQuestion.MaxValue = 20;
            NumberOfQuestion.MinValue = 1;

            // Grab the button on the page
            Button StartQuizBtn = FindViewById<Button>(Resource.Id.PreQuizStart);

            // Create Click handler to take the user to the quiz page. Adds the
            // number of questions the user asked for.
            StartQuizBtn.Click += delegate
            {
                var QuizIntent = new Intent(this, typeof(QuestionActivity));
                QuizIntent.PutExtra("NumberOfQuestions", NumberOfQuestion.Value.ToString());
                StartActivity(QuizIntent);
            };
        }
    }
}
