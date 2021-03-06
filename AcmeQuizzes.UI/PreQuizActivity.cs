﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Views.InputMethods;
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
            NumberPicker numberOfQuestion = FindViewById<NumberPicker>(Resource.Id.NumberOfQuestion);
            numberOfQuestion.MaxValue = 20;
            numberOfQuestion.MinValue = 1;

            // Grab the button on the page
            Button startQuizBtn = FindViewById<Button>(Resource.Id.PreQuizStart);

            // Create Click handler to take the user to the quiz page. Adds the
            // number of questions the user asked for.
            startQuizBtn.Click += delegate
            {
                var inputMethodManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);

                // Incase the user has typed in a value and click start we need to force the correct
                // selected item
                if (inputMethodManager.IsAcceptingText)
                {
                    numberOfQuestion.ClearFocus();
                    inputMethodManager.HideSoftInputFromWindow(numberOfQuestion.WindowToken, 0);
                }
                Intent quizIntent = new Intent(this, typeof(QuestionActivity));
                quizIntent.PutExtra("NumberOfQuestions", numberOfQuestion.Value.ToString());
                StartActivity(quizIntent);
            };
        }
    }
}
