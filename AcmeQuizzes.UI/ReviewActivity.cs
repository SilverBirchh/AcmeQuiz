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
        QuizRespository questionRepository = new QuizRespository();

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

        /*
         * Method to add a remark to a question answer. Will work out if the option to mark is the correct
         * option and mark it as the correct answer. Will work out if the answer was wrong and if this was 
         * the option selected by the user and will mark the question as such.
         * 
         * @param question - Question Object
         * @param userAnswer - The option the user selected
         * @param optionToMark - The question option answer number to mark  
         */
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
    }
}
