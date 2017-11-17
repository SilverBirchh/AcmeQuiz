
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EditQuestion);
            TextView idView = FindViewById<TextView>(Resource.Id.qId);
            EditText QuestionTitleView = FindViewById<EditText>(Resource.Id.QTextEdit);
            EditText Op1View = FindViewById<EditText>(Resource.Id.op1Edit);
            EditText Op2View = FindViewById<EditText>(Resource.Id.op2Edit);
            EditText Op3View = FindViewById<EditText>(Resource.Id.op3Edit);
            EditText Op4View = FindViewById<EditText>(Resource.Id.op4Edit);
            EditText Op5View = FindViewById<EditText>(Resource.Id.op5Edit);
            EditText CorrectView = FindViewById<EditText>(Resource.Id.correctEdit);

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
                if (QuestionId != null)
                {
                    Question question = questionRepository.GetQuestion(Int32.Parse(QuestionId));
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
                    Question question = new Question();
                    question.QuestionText = QuestionTitleView.Text;
                    question.Option1 = Op1View.Text;
                    question.Option2 = Op2View.Text;
                    question.Option3 = Op3View.Text;
                    question.Option4 = Op4View.Text;
                    question.Option5 = Op5View.Text;
                    question.CorrectAnswer = CorrectView.Text;
                    questionRepository.SaveQuestion(question);

                }
                Finish();
            };

            DltBtn.Click += delegate
            {
                if (QuestionId != null)
                {
                    Question question = questionRepository.GetQuestion(Int32.Parse(QuestionId));
                    questionRepository.DeleteQuestion(question);
                }
                Finish();
            };

            CnlBtn.Click += delegate
            {
                Finish();
            };

        }
    }
}
