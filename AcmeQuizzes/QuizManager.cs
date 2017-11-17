using System;
using System.Collections.Generic;

namespace AcmeQuizzes
{
    public class QuizManager
    {
        QuizRespository quizRepository = new QuizRespository();
        List<Question> LimitedQuestions = new List<Question>();
        int QuestionCount = 1;

        public static Dictionary<int, bool> AnsweredQuestions = new Dictionary<int, bool>();

        public QuizManager(){}

        public void InitialseQuestions(int NumberOfQuestions)
        {
            AnsweredQuestions = new Dictionary<int, bool>();

            List<Question> AllQuestions = quizRepository.GetAllQuestions();
            AllQuestions.Randomise();
            for (int i = 1; i <= NumberOfQuestions; i++)
            {
                LimitedQuestions.Add(AllQuestions[i]);
            }
        }

        public Question GetNextQuestion()
        {
            Question NextQuestion = LimitedQuestions[QuestionCount - 1];
            QuestionCount++;
            return NextQuestion;
        }

        public bool HasNextQuestion()
        {
            return QuestionCount <= LimitedQuestions.Count;
        }

        public void AnswerQuestion(int QuestionID, int Answer, string CorrectAnswer)
        {
            Answer++;
            bool IsCorrect = Answer.ToString().Equals(CorrectAnswer);
            AnsweredQuestions.Add(QuestionID, IsCorrect);
        }
    }
}
