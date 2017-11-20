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
        QuizManager QuestionManager = new QuizManager(); //TODO: make interface

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the layout to Question
            SetContentView(Resource.Layout.Question);

            // Grab the number of questions that the user asked for from the intent
            string NumberOfQuestions = Intent.GetStringExtra("NumberOfQuestions");

            // Set up the QuestionManager for this session. This will initialise the
            // correct number of questions to ask the user and reset any
            // previous session.
            QuestionManager.InitialseQuestions(Int32.Parse(NumberOfQuestions));

            // Grab the correct UI elements 
            TextView QuestionTitle = FindViewById<TextView>(Resource.Id.questionNumber);
            ListView AnswersView = FindViewById<ListView>(Resource.Id.answers);

            // Grab the first question to show the user.
            Question NextQuestion = QuestionManager.GetNextQuestion();

            // Initialse an array to store answers to show the user.
            String[] Answers = null;

            // Workout how many possibel answers there are for the given question and set Answers array
            if (NextQuestion.Option5.Equals(""))
            {
                Answers = new String[] { NextQuestion.Option1, NextQuestion.Option2, NextQuestion.Option3, NextQuestion.Option4 };
            }
            else
            {
                Answers = new String[] { NextQuestion.Option1, NextQuestion.Option2, NextQuestion.Option3, NextQuestion.Option4, NextQuestion.Option5 };
            }

            // Show the question and possible answers
            QuestionTitle.Text = NextQuestion.QuestionText;
            AnswersView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, Answers);

            // Set up a click listener for when the user clicks an answer to a question
            AnswersView.ItemClick += (s, args) =>
            {
                // Send the answered question to the QuestionManager to store
                QuestionManager.AnswerQuestion(NextQuestion.QuestionID, args.Position, NextQuestion.CorrectAnswer);

                // Check if there is another question to ask the user. If not the user should
                // be taken to the results page
                if (!QuestionManager.HasNextQuestion())
                {
                    Intent ResultsIntent = new Intent(this, typeof(ResultsActivity));
                    StartActivity(ResultsIntent);
                    return;
                }

                // Grab the next question
                NextQuestion = QuestionManager.GetNextQuestion();

                // Like above set the question title and possible answers by working out and creating
                // a new answers array and setting this as the adapter for AnswersView
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
