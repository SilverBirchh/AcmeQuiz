using System;
using Android.App;
using Android.Content;
using Android.OS;
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

            // Set the layout to Question
            SetContentView(Resource.Layout.Question);

            // Grab the number of questions that the user asked for from the intent
            string numberOfQuestions = Intent.GetStringExtra("NumberOfQuestions");

            // Set up the QuestionManager for this session. This will initialise the
            // correct number of questions to ask the user and reset any
            // previous session.
            questionManager.InitialseQuestions(Int32.Parse(numberOfQuestions));

            // Grab the correct UI elements 
            TextView questionTitle = FindViewById<TextView>(Resource.Id.questionNumber);
            ListView answersView = FindViewById<ListView>(Resource.Id.answers);

            // Grab the first question to show the user.
            Question nextQuestion = questionManager.GetNextQuestion();

            // Initialse an array to store answers to show the user.
            String[] answers = null;

            // Workout how many possibel answers there are for the given question and set Answers array
            if (nextQuestion.Option5.Equals(""))
            {
                answers = new String[] { nextQuestion.Option1, nextQuestion.Option2, nextQuestion.Option3, nextQuestion.Option4 };
            }
            else
            {
                answers = new String[] { nextQuestion.Option1, nextQuestion.Option2, nextQuestion.Option3, nextQuestion.Option4, nextQuestion.Option5 };
            }

            // Show the question and possible answers
            questionTitle.Text = nextQuestion.QuestionText;
            answersView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, answers);

            // Set up a click listener for when the user clicks an answer to a question
            answersView.ItemClick += (s, args) =>
            {
                // Send the answered question to the QuestionManager to store
                questionManager.AnswerQuestion(nextQuestion, args.Position);


                // Check if there is another question to ask the user. If not the user should
                // be taken to the results page
                if (!questionManager.HasNextQuestion())
                {
                    Intent resultsIntent = new Intent(this, typeof(ResultsActivity));
                    StartActivity(resultsIntent);
                    return;
                }

                // Grab the next question
                nextQuestion = questionManager.GetNextQuestion();

                // Like above set the question title and possible answers by working out and creating
                // a new answers array and setting this as the adapter for AnswersView
                String[] newAnswers = null;
                if (nextQuestion.Option5.Equals(""))
                {
                    newAnswers = new String[] { nextQuestion.Option1, nextQuestion.Option2, nextQuestion.Option3, nextQuestion.Option4 };
                }
                else
                {
                    newAnswers = new String[] { nextQuestion.Option1, nextQuestion.Option2, nextQuestion.Option3, nextQuestion.Option4, nextQuestion.Option5 };
                }

                questionTitle.Text = nextQuestion.QuestionText;
                answersView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, newAnswers);
            };

        }
    }
}
