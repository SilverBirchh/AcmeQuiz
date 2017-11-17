
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
    [Activity(Label = "Review")]
    public class ReviewActivity : Activity
    {
        QuizRespository questionRepository = new QuizRespository(); //TODO: make interface

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Review);
            Button StartAgain = FindViewById<Button>(Resource.Id.again);
            Button GoHome = FindViewById<Button>(Resource.Id.home);
            TextView Review = FindViewById<TextView>(Resource.Id.reviewQuestions);

            Review.Text = GenerateReview();

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

        private string GenerateReview()
        {
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (KeyValuePair<int, bool> entry in QuizManager.AnsweredQuestions)
            {
                Question question = questionRepository.GetQuestion(entry.Key);
                // do something with entry.Value or entry.Key
                sb.AppendLine($"{count}. {question.QuestionText}");
                sb.AppendLine($"{(entry.Value ? "Correct" : "Incorrect")}");
                sb.AppendLine($"Answer: {GetAnswer(question, question.CorrectAnswer)}\n");
                count++;
            }

            return sb.ToString();
        }

        private string GetAnswer(Question question, string OptionNumber)
        {
            switch (OptionNumber)
            {
                case "1":
                    return question.Option1;
                case "2":
                    return question.Option2;
                case "3":
                    return question.Option3;
                case "4":
                    return question.Option4;
                case "5":
                    return question.Option5;
                default:
                    return "Unable to find question answer.";

            }
        }
    }
}
