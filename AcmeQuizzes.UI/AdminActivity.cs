using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "Admin")]
    public class AdminActivity : Activity
    {
        QuizRespository QuestionRepository = new QuizRespository(); //TODO: make interface
        List<Question> AllQuestions;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the view to the Admin layout
            SetContentView(Resource.Layout.Admin);

            // Grab the UI controls from the page
            ListView QuestionsView = FindViewById<ListView>(Resource.Id.questionsList);
            Button HomeBtn = FindViewById<Button>(Resource.Id.home);
            Button AddBtn = FindViewById<Button>(Resource.Id.add);

            // Fetch all of the questions and set them as the adapter for the ListView
            string[] Questions = FetchQuestions();
            QuestionsView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, Questions);

            // Set up click listener to go back to the home page
            HomeBtn.Click += delegate
            {
                Intent HomeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(HomeIntent);
            };

            // Set up click listener to take the user to add a question
            AddBtn.Click += delegate
            {
                Intent EditIntent = new Intent(this, typeof(EditQuestionActivity));
                StartActivity(EditIntent);
            };

            // Set up item click to take the user to edit a question based off the questions
            // position within the AllQuestions List
            QuestionsView.ItemClick += (s, args) =>
            {
                Intent GoEdit = new Intent(this, typeof(EditQuestionActivity));
                GoEdit.PutExtra("QuestionId", AllQuestions[args.Position].QuestionID.ToString());
                StartActivity(GoEdit);
            };
        }

        /*
         * Method to fetch all questions in the DB and create an array of Strings
         * which are in the format "Number. Question Text"
         */
        private string[] FetchQuestions() // TODO: Change this to just be a DB call.
        {
            int count = 1;
            List<string> questionArray = new List<string>();
            AllQuestions = QuestionRepository.GetAllQuestions();
            foreach (Question question in AllQuestions)
            {
                questionArray.Add($"{count}. {question.QuestionText}");
                count++;
            }
            return questionArray.ToArray();
        }
    }
}
