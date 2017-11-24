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
            foreach (KeyValuePair<int, bool> entry in QuizManager.answeredQuestions)
            {
                // Grab the full Question Object from the ID in AnsweredQuestions
                Question question = questionRepository.GetQuestion(entry.Key);

                // Set the details required to review the question
                sb.AppendLine($"{count}. {question.QuestionText}");
                sb.AppendLine($"{(entry.Value ? "Correct" : "Incorrect")}");
                sb.AppendLine($"Correct answer: {GetAnswer(question, question.CorrectAnswer)}\n");
                count++;
            }

            return sb.ToString();
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
