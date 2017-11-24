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
        QuizRespository QuestionRepository = new QuizRespository(); //TODO: make interface

        // Array used to validate the CorrectAnswer property of a Question
        string[] CorrectOptions = { "1", "2", "3", "4", "5" };

        // Initialise UI variables that are needed outside OnCreate()
        EditText QuestionTitleView;
        EditText Op1View;
        EditText Op2View;
        EditText Op3View;
        EditText Op4View;
        EditText Op5View;
        EditText CorrectView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the View to Edit Question
            SetContentView(Resource.Layout.EditQuestion);

            // Grab the UI elements from the layout and save them as variables
            TextView idView = FindViewById<TextView>(Resource.Id.qId);
            QuestionTitleView = FindViewById<EditText>(Resource.Id.QTextEdit);
            Op1View = FindViewById<EditText>(Resource.Id.op1Edit);
            Op2View = FindViewById<EditText>(Resource.Id.op2Edit);
            Op3View = FindViewById<EditText>(Resource.Id.op3Edit);
            Op4View = FindViewById<EditText>(Resource.Id.op4Edit);
            Op5View = FindViewById<EditText>(Resource.Id.op5Edit);
            CorrectView = FindViewById<EditText>(Resource.Id.correctEdit);
            Button SaveBtn = FindViewById<Button>(Resource.Id.save);
            Button CnlBtn = FindViewById<Button>(Resource.Id.cnl);
            Button DltBtn = FindViewById<Button>(Resource.Id.dlt);

            // The user may have reached this page by clicking on a question
            // Grab the QuestionId that might be on the intent.
            // If it is present set the correct text on the UI related to the correct question
            string QuestionId = Intent.GetStringExtra("QuestionId");
            if (QuestionId != null)
            {
                Question question = QuestionRepository.GetQuestion(Int32.Parse(QuestionId));

                idView.Text = question.QuestionID.ToString();
                QuestionTitleView.Text = question.QuestionText;
                Op1View.Text = question.Option1;
                Op2View.Text = question.Option2;
                Op3View.Text = question.Option3;
                Op4View.Text = question.Option4;
                Op5View.Text = question.Option5;
                CorrectView.Text = question.CorrectAnswer;
            }

            // Add a click listener to the save button
            SaveBtn.Click += delegate
            {
                // Check if the Question Object is valid. If not alert the user of this
                if (!IsValidQuestion())
                {
                    ThrowAlert();
                    return;
                }

                // QuestionId is passed in from the intent to get to this page. If this is not set
                // the user is creating a new question.
                if (QuestionId != null)
                {
                    // Grab the question being editted
                    Question question = QuestionRepository.GetQuestion(Int32.Parse(QuestionId));

                    // Set new attributes on the question and save the edit
                    QuestionRepository.EditQuestion(SetQuestionAttributes(question));
                }
                else
                {
                    // Create a new Question Object
                    Question question = new Question();

                    // Set all of the attributes on the Quesiton and save its
                    QuestionRepository.SaveQuestion(SetQuestionAttributes(question));

                }

                // Return to the Admin page
                Finish();
            };

            // Add a click listener to the delete button
            DltBtn.Click += delegate
            {
                // The user can only delete a question if they have come to this page after clicking directly on a question
                if (QuestionId != null)
                {
                    // Grab the correct question and delete it
                    Question question = QuestionRepository.GetQuestion(Int32.Parse(QuestionId));
                    QuestionRepository.DeleteQuestion(question);
                }

                // Return to the Admin page
                Finish();
            };

            // Add a click listener to the cancle button to return to the admin page
            CnlBtn.Click += delegate
            {
                Intent CnlIntent = new Intent(this, typeof(AdminActivity));
                StartActivity(CnlIntent);
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
            isValid = QuestionTitleView.Text != null
                                       && Op1View.Text != null
                                       && Op2View.Text != null
                                       && Op3View.Text != null
                                       && Op3View.Text != null
                                       && Op4View.Text != null
                                       && CorrectView.Text != null
                                       && QuestionTitleView.Text.Trim() != ""
                                       && Op1View.Text.Trim() != ""
                                       && Op2View.Text.Trim() != ""
                                       && Op3View.Text.Trim() != ""
                                       && Op3View.Text.Trim() != ""
                                       && Op4View.Text.Trim() != "";

            // If the first check is okay this step is run
            if (isValid)
            {
                // Checks if CorrectView input contains a value from the PossibleAnswers array.
                // Finally checks if the Option5 answer is not null or blank and the the correct answer is in PossibleAnswers or equals 5.
                isValid = (PossibleAnswers.Contains(CorrectView.Text)
                                               || (Op5View.Text != null && Op5View.Text != ""
                                               && (CorrectView.Text.Equals("5")
                                                       || PossibleAnswers.Contains(CorrectView.Text))));
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
            question.QuestionText = QuestionTitleView.Text;
            question.Option1 = Op1View.Text;
            question.Option2 = Op2View.Text;
            question.Option3 = Op3View.Text;
            question.Option4 = Op4View.Text;
            question.Option5 = Op5View.Text;
            question.CorrectAnswer = CorrectView.Text;
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
