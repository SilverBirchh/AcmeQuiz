using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
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

            // Set layout to Review
            SetContentView(Resource.Layout.Review);

            // Grab the buttons and text view on the layout
            Button startAgainBtn = FindViewById<Button>(Resource.Id.again);
            Button goHomeBtn = FindViewById<Button>(Resource.Id.home);
            TextView reviewView = FindViewById<TextView>(Resource.Id.reviewQuestions);

            // Set the review text
            reviewView.Text = GenerateReview();

            // Create Click handler to take the user to the PreQuizActivity page
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
        }

        /*
         * Method to build up a string of questions answered, if the user was correct
         * or not and the correct answer of the question.
         * @return string
         */
        private string GenerateReview()
        {
            StringBuilder sb = new StringBuilder();
            int count = 1;
            foreach (KeyValuePair<Question, string> entry in QuizManager.answeredQuestions)
            {
                // Set the details required to review the question
                sb.AppendLine($"{count}. {entry.Key.QuestionText}");
                sb.AppendLine($"You were {(entry.Value.Equals(entry.Key.CorrectAnswer) ? "Correct." : "Incorrect.")}");
                sb.AppendLine($"{(entry.Key.Option1)} {MarkOption(entry.Key, entry.Value, "1")}");
                sb.AppendLine($"{(entry.Key.Option2)} {MarkOption(entry.Key, entry.Value, "2")}");
                sb.AppendLine($"{(entry.Key.Option3)} {MarkOption(entry.Key, entry.Value, "3")}");
                sb.AppendLine($"{(entry.Key.Option4)} {MarkOption(entry.Key, entry.Value, "4")}");
                if (!entry.Key.Option5.Equals(""))
                {
                    sb.AppendLine($"{(entry.Key.Option5)} {MarkOption(entry.Key, entry.Value, "5")}");
                }
                sb.AppendLine("\n");
                count++;
            }

            return sb.ToString();
        }

        private string MarkOption(Question question, string userAnswer, string optionToMark)
        {
            if ((question.CorrectAnswer.Equals(userAnswer) && question.CorrectAnswer.Equals(optionToMark)) || question.CorrectAnswer.Equals(optionToMark))
            {
                return " - Correct answer";
            }
            else if (optionToMark.Equals(userAnswer) && !question.CorrectAnswer.Equals(userAnswer))
            {
                return " - Your answer";
            }
            return "";
        }

        /*
         * Method used to return the text value of the answer for a question.
         * TODO: Pull out to Core logic
         * @return string
         */
        private string GetAnswer(Question question, string optionNumber)
        {
            switch (optionNumber)
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
