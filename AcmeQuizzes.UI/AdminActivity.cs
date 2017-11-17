
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
    [Activity(Label = "Admin")]
    public class AdminActivity : Activity
    {
        QuizRespository questionRepository = new QuizRespository(); //TODO: make interface

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Admin);
            ListView QuestionsView = FindViewById<ListView>(Resource.Id.questionsList);
            string[] Questions = FetchQuestions();
            QuestionsView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, Questions);
        }

        private string[] FetchQuestions() // TODO: Change this to just be a DB call.
        {
            int count = 1;
            List<string> questionArray = new List<string>();
            List<Question> AllQuestions = questionRepository.GetAllQuestions();
            foreach (Question question in AllQuestions)
            {
                questionArray.Add($"{count}. {question.QuestionText}");
                count++;
            }
            return questionArray.ToArray();
        }
    }
}
