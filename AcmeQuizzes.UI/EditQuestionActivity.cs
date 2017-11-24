using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "Add / Edit Question")]
    public class EditQuestionActivity : Activity
    {
        QuizRespository questionRepository = new QuizRespository(); //TODO: make interface

        // Array used to validate the CorrectAnswer property of a Question
        string[] correctOptions = { "1", "2", "3", "4", "5" };

        // Initialise UI variables that are needed outside OnCreate()
        EditText questionTitleView;
        EditText op1View;
        EditText op2View;
        EditText op3View;
        EditText op4View;
        EditText op5View;
        EditText correctView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the View to Edit Question
            SetContentView(Resource.Layout.EditQuestion);

            // Grab the UI elements from the layout and save them as variables
            TextView idView = FindViewById<TextView>(Resource.Id.qId);
            questionTitleView = FindViewById<EditText>(Resource.Id.QTextEdit);
            op1View = FindViewById<EditText>(Resource.Id.op1Edit);
            op2View = FindViewById<EditText>(Resource.Id.op2Edit);
            op3View = FindViewById<EditText>(Resource.Id.op3Edit);
            op4View = FindViewById<EditText>(Resource.Id.op4Edit);
            op5View = FindViewById<EditText>(Resource.Id.op5Edit);
            correctView = FindViewById<EditText>(Resource.Id.correctEdit);
            Button saveBtn = FindViewById<Button>(Resource.Id.save);
            Button cnlBtn = FindViewById<Button>(Resource.Id.cnl);
            Button dltBtn = FindViewById<Button>(Resource.Id.dlt);

            // The user may have reached this page by clicking on a question
            // Grab the QuestionId that might be on the intent.
            // If it is present set the correct text on the UI related to the correct question
            string questionId = Intent.GetStringExtra("QuestionId");
            if (questionId != null)
            {
                Question question = questionRepository.GetQuestion(Int32.Parse(questionId));

                idView.Text = question.QuestionID.ToString();
                questionTitleView.Text = question.QuestionText;
                op1View.Text = question.Option1;
                op2View.Text = question.Option2;
                op3View.Text = question.Option3;
                op4View.Text = question.Option4;
                op5View.Text = question.Option5;
                correctView.Text = question.CorrectAnswer;
            }

            // Add a click listener to the save button
            saveBtn.Click += delegate
            {
                // Check if the Question Object is valid. If not alert the user of this
                if (!IsValidQuestion())
                {
                    ThrowAlert();
                    return;
                }

                // QuestionId is passed in from the intent to get to this page. If this is not set
                // the user is creating a new question.
                if (questionId != null)
                {
                    // Grab the question being editted
                    Question question = questionRepository.GetQuestion(Int32.Parse(questionId));

                    // Set new attributes on the question and save the edit
                    questionRepository.EditQuestion(SetQuestionAttributes(question));
                }
                else
                {
                    // Create a new Question Object
                    Question question = new Question();

                    // Set all of the attributes on the Quesiton and save its
                    questionRepository.SaveQuestion(SetQuestionAttributes(question));

                }

                // Return to the Admin page
                Finish();
            };

            // Add a click listener to the delete button
            dltBtn.Click += delegate
            {
                // The user can only delete a question if they have come to this page after clicking directly on a question
                if (questionId != null)
                {
                    // Grab the correct question and delete it
                    Question question = questionRepository.GetQuestion(Int32.Parse(questionId));
                    questionRepository.DeleteQuestion(question);
                }

                // Return to the Admin page
                Finish();
            };

            // Add a click listener to the cancle button to return to the admin page
            cnlBtn.Click += delegate
            {
                Intent cnlIntent = new Intent(this, typeof(AdminActivity));
                StartActivity(cnlIntent);
            };

        }

        /*
         * Method to determine if the question being added is valid or not.
         * TODO: Simplify this check
         * @return bool
         */
        bool IsValidQuestion()
        {
            bool isValid = true;

            // The possible correct answers for a question. Minus the option "5".
            string[] PossibleAnswers = { "1", "2", "3", "4" };

            // Check to see if none of the required entries are null. Checks if each required input is not blank. 
            isValid = questionTitleView.Text != null
                                       && op1View.Text != null
                                       && op2View.Text != null
                                       && op3View.Text != null
                                       && op3View.Text != null
                                       && op4View.Text != null
                                       && correctView.Text != null
                                       && questionTitleView.Text.Trim() != ""
                                       && op1View.Text.Trim() != ""
                                       && op2View.Text.Trim() != ""
                                       && op3View.Text.Trim() != ""
                                       && op3View.Text.Trim() != ""
                                       && op4View.Text.Trim() != "";

            // If the first check is okay this step is run
            if (isValid)
            {
                // Checks if CorrectView input contains a value from the PossibleAnswers array.
                // Finally checks if the Option5 answer is not null or blank and the the correct answer is in PossibleAnswers or equals 5.
                isValid = (PossibleAnswers.Contains(correctView.Text)
                                               || (op5View.Text != null && op5View.Text != ""
                                               && (correctView.Text.Equals("5")
                                                       || PossibleAnswers.Contains(correctView.Text))));
            }

            return isValid;
        }

        /*
         * Method used to set the Question properties to the values that the user has entered
         * on the page. Since we do not know which options have been editted we have to set them all
         * 
         * @return Question
         */
        Question SetQuestionAttributes(Question question)
        {
            question.QuestionText = questionTitleView.Text;
            question.Option1 = op1View.Text;
            question.Option2 = op2View.Text;
            question.Option3 = op3View.Text;
            question.Option4 = op4View.Text;
            question.Option5 = op5View.Text;
            question.CorrectAnswer = correctView.Text;
            return question;
        }

        /*
         * Method to throw an alert to the user that something is wrong with the question trying to be saved
         */
        void ThrowAlert()
        {
            RunOnUiThread(() =>
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Invalid Question");
                builder.SetMessage("Hmm something is wrong with the question. Please try again.");
                builder.SetPositiveButton("Ok", (sender, e) => { });
                builder.Show();
            });
        }
    }
}
