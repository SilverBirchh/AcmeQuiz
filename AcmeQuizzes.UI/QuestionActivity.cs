
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
    [Activity(Label = "Quiz")]
    public class QuestionActivity : Activity
    {
        QuizManager questionManager = new QuizManager(); //TODO: make interface

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Question);

            string NumberOfQuestions = Intent.GetStringExtra("NumberOfQuestions");
            questionManager.InitialseQuestions(Int32.Parse(NumberOfQuestions));

            TextView QuestionTitle = FindViewById<TextView>(Resource.Id.questionNumber);
            ListView AnswersView = FindViewById<ListView>(Resource.Id.answers);

            Question NextQuestion = questionManager.GetNextQuestion();
            String[] Answers = null;
            if (NextQuestion.Option5.Equals(""))
            {
                Answers = new String[] { NextQuestion.Option1, NextQuestion.Option2, NextQuestion.Option3, NextQuestion.Option4 };
            }
            else
            {
                Answers = new String[] { NextQuestion.Option1, NextQuestion.Option2, NextQuestion.Option3, NextQuestion.Option4, NextQuestion.Option5 };
            }

            QuestionTitle.Text = NextQuestion.QuestionText;
            AnswersView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, Answers);

            AnswersView.ItemClick += (s, args) =>
            {
                questionManager.AnswerQuestion(NextQuestion.QuestionID, args.Position, NextQuestion.CorrectAnswer);

                if (!questionManager.HasNextQuestion())
                {
                    Intent GoFinish = new Intent(this, typeof(ResultsActivity));
                    StartActivity(GoFinish);
                    return;
                }

                NextQuestion = questionManager.GetNextQuestion();
                String[] NewAnswers = null;
                if (NextQuestion.Option5.Equals(""))
                {
                    NewAnswers = new String[] { NextQuestion.Option1, NextQuestion.Option2, NextQuestion.Option3, NextQuestion.Option4 };
                }
                else
                {
                    NewAnswers = new String[] { NextQuestion.Option1, NextQuestion.Option2, NextQuestion.Option3, NextQuestion.Option4, NextQuestion.Option5 };
                }

                QuestionTitle.Text = NextQuestion.QuestionText;
                AnswersView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, NewAnswers);
            };

        }
    }
}
