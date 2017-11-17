
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AcmeQuizzes.UI
{
    [Activity(Label = "EditQuestionActivity")]
    public class EditQuestionActivity : Activity
    {
        QuizRespository questionRepository = new QuizRespository(); //TODO: make interface
        string[] correctOptions = new string[] { "1", "2", "3", "4", "5" };

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

            // Create your application here
            SetContentView(Resource.Layout.EditQuestion);
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

            string QuestionId = Intent.GetStringExtra("QuestionId");
            if (QuestionId != null)
            {
                Question question = questionRepository.GetQuestion(Int32.Parse(QuestionId));

                idView.Text = question.QuestionID.ToString();
                QuestionTitleView.Text = question.QuestionText;
                Op1View.Text = question.Option1;
                Op2View.Text = question.Option2;
                Op3View.Text = question.Option3;
                Op4View.Text = question.Option4;
                Op5View.Text = question.Option5;
                CorrectView.Text = question.CorrectAnswer;
            }

            SaveBtn.Click += delegate
            {
                if (!IsValidQuestion())
                {
                    RunOnUiThread(() =>
                    {
                        var builder = new AlertDialog.Builder(this);
                        builder.SetTitle("Invalid Question");
                        builder.SetMessage("Hmm something is wrong with the question. Please try again.");
                        builder.SetPositiveButton("Ok", (sender, e) => { });
                        builder.Show();
                    }
                     );
                    return;
                }

                Question question;
                if (QuestionId != null)
                {
                    question = questionRepository.GetQuestion(Int32.Parse(QuestionId));
                    question.QuestionText = QuestionTitleView.Text;
                    question.Option1 = Op1View.Text;
                    question.Option2 = Op2View.Text;
                    question.Option3 = Op3View.Text;
                    question.Option4 = Op4View.Text;
                    question.Option5 = Op5View.Text;
                    question.CorrectAnswer = CorrectView.Text;
                    questionRepository.EditQuestion(question);
                }
                else
                {
                    question = new Question();
                    question.QuestionText = QuestionTitleView.Text;
                    question.Option1 = Op1View.Text;
                    question.Option2 = Op2View.Text;
                    question.Option3 = Op3View.Text;
                    question.Option4 = Op4View.Text;
                    question.Option5 = Op5View.Text;
                    question.CorrectAnswer = CorrectView.Text;
                    questionRepository.SaveQuestion(question);

                }
                var SaveIntent = new Intent(this, typeof(AdminActivity));
                StartActivity(SaveIntent);
            };

            DltBtn.Click += delegate
            {
                if (QuestionId != null)
                {
                    Question question = questionRepository.GetQuestion(Int32.Parse(QuestionId));
                    questionRepository.DeleteQuestion(question);
                }
                var DltIntent = new Intent(this, typeof(AdminActivity));
                StartActivity(DltIntent);
            };

            CnlBtn.Click += delegate
            {
                var CnlIntent = new Intent(this, typeof(AdminActivity));
                StartActivity(CnlIntent);
            };

        }

        bool IsValidQuestion()
        {
            bool isValid = true;
            string[] PossibleAnswers = { "1", "2", "3", "4" };

            isValid = QuestionTitleView.Text != null && Op1View.Text != null && Op2View.Text != null && Op3View.Text != null && Op3View.Text != null && Op4View.Text != null && CorrectView.Text != null;

            if (isValid)
            {
                isValid = QuestionTitleView.Text.Trim() != "" && Op1View.Text.Trim() != "" && Op2View.Text.Trim() != "" && Op3View.Text.Trim() != "" && Op3View.Text.Trim() != "" && Op4View.Text.Trim() != "" && (PossibleAnswers.Contains(CorrectView.Text) || (Op5View.Text != null && Op5View.Text != "" && CorrectView.Text.Equals("5")));
            }

            return isValid;
        }
    }
}
