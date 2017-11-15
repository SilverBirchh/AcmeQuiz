
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
    [Activity(Label = "QuestionActivity")]
    public class QuestionActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string NumberOfQuestions = Intent.GetStringExtra("NumberOfQuestions");

            QuizRespository question = new QuizRespository();
            List<Question> qs = question.GetAllQuestions();

        }
    }
}
