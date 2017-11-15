
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
    [Activity(Label = "PreQuiz")]
    public class PreQuiz : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "Instructions" layout resource
            SetContentView(Resource.Layout.PreQuiz);

            // Set the min / max values of the Number Picker
            NumberPicker NumberOfQuestion = FindViewById<NumberPicker>(Resource.Id.NumberOfQuestion);
            NumberOfQuestion.MaxValue = 20;
            NumberOfQuestion.MinValue = 1;

            Button StartQuiz = FindViewById<Button>(Resource.Id.PreQuizStart);

            StartQuiz.Click += delegate
            {
                Intent QuizIntent = new Intent(this, typeof(QuestionActivity));
                QuizIntent.PutExtra("NumberOfQuestions", NumberOfQuestion.Value);
                StartActivity(QuizIntent);
            };
        }
    }
}
