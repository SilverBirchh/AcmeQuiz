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
        QuizRespository questionRepository = new QuizRespository(); //TODO: make interface
        List<Question> allQuestions;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the view to the Admin layout
            SetContentView(Resource.Layout.Admin);

            // Grab the UI controls from the page
            ListView questionsView = FindViewById<ListView>(Resource.Id.questionsList);
            Button homeBtn = FindViewById<Button>(Resource.Id.home);
            Button addBtn = FindViewById<Button>(Resource.Id.add);

            // Fetch all of the questions and set them as the adapter for the ListView
            string[] questions = FetchQuestions();
            questionsView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, questions);

            // Set up click listener to go back to the home page
            homeBtn.Click += delegate
            {
                Intent homeIntent = new Intent(this, typeof(MainActivity));
                StartActivity(homeIntent);
            };

            // Set up click listener to take the user to add a question
            addBtn.Click += delegate
            {
                Intent editIntent = new Intent(this, typeof(EditQuestionActivity));
                StartActivity(editIntent);
            };

            // Set up item click to take the user to edit a question based off the questions
            // position within the AllQuestions List
            questionsView.ItemClick += (s, args) =>
            {
                Intent goEdit = new Intent(this, typeof(EditQuestionActivity));
                goEdit.PutExtra("QuestionId", allQuestions[args.Position].QuestionID.ToString());
                StartActivity(goEdit);
            };
        }

        // OnResume of the activty. The user may come here after saving / editing / deleting an activity
        // We need to repopulate the list of qustions.
        protected override void OnResume()
        {
            base.OnResume();
            // Fetch the questions view
            ListView questionsView = FindViewById<ListView>(Resource.Id.questionsList);

            // Fetch all of the questions and set them as the adapter for the ListView
            string[] questions = FetchQuestions();
            questionsView.Adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, questions);
        }

        /*
         * Method to fetch all questions in the DB and create an array of Strings
         * which are in the format "Number. Question Text"
         */
        private string[] FetchQuestions() // TODO: Change this to just be a DB call.
        {
            int count = 1;
            List<string> questionArray = new List<string>();
            allQuestions = questionRepository.GetAllQuestions();
            foreach (Question question in allQuestions)
            {
                questionArray.Add($"{count}. {question.QuestionText}");
                count++;
            }
            return questionArray.ToArray();
        }
    }
}
