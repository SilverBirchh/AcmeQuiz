
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
    [Activity(Label = "Finish")]
    public class Finish : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Finish);

            Button StartAgain = FindViewById<Button>(Resource.Id.again);
            Button GoHome = FindViewById<Button>(Resource.Id.home);
            TextView Score = FindViewById<TextView>(Resource.Id.score);

            // Work out the mount of question asked and how many were correct
            int AmountOfQuestions = QuizRespository.AnsweredQuestions.Count();
            int CorrectAnswers = QuizRespository.AnsweredQuestions.Where(kvp => kvp.Value).Count();

            Score.Text = $"{CorrectAnswers} / {AmountOfQuestions}";

            // Create Click handler to take the user to the PreQuiz page
            StartAgain.Click += delegate
            {
                Intent AgainIntent = new Intent(this, typeof(PreQuiz));
                StartActivity(AgainIntent);
            };

            // Create Click handler to take the user to the PreQuiz page
            GoHome.Click += delegate
            {
                Intent HomeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(HomeIntent);
            };
        }
    }
}
