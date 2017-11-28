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
            Button nextBtn = FindViewById<Button>(Resource.Id.next);
            ListView answersView = FindViewById<ListView>(Resource.Id.answers);
            answersView.ChoiceMode = ChoiceMode.Single;

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
            answersView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemSingleChoice, answers);

            // Used to track the users answer
            int answerChoice = -1;

            // Set up a click listener for when the user clicks an answer to a question
            answersView.ItemClick += (s, args) =>
            {
                answerChoice = args.Position;
            };

            nextBtn.Click += delegate
            {
                if (answerChoice == -1)
                {
                    return;
                }
                // Send the answered question to the QuestionManager to store
                questionManager.AnswerQuestion(nextQuestion, answerChoice);


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
                answersView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemSingleChoice, newAnswers);

                // Reset the selected answer
                answerChoice = -1;
            };

        }

        /*
         * Method to throw an that the question has not been answered
         */
        void ThrowAlert()
        {
            RunOnUiThread(() =>
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Invalid Question");
                builder.SetMessage("Please select an answer.");
                builder.SetPositiveButton("Ok", (sender, e) => { });
                builder.Show();
            });
        }
    }
}
