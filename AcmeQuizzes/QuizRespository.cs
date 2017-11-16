using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AcmeQuizzes
{
    public class QuizRespository : IQuizRepository
    {
        private IQuizConnection quizConnection = null;
        private string dbLocation;

        private ListDictionary AnsweredQuestions = new ListDictionary();
        List<Question> LimitedQuestions = new List<Question>();
        int QuestionCount = 0;

        public QuizRespository()
        {
            dbLocation = DatabaseFilePath;
            this.quizConnection = new QuizConnection(dbLocation);
        }

        private string DatabaseFilePath
        {
            get
            {
                string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                string path = Path.Combine(FolderPath, "Quiz.sqlite");
                return path;
            }
        }

        public List<Question> GetAllQuestions()
        {
            return quizConnection.GetAllQuestions();
        }

        public Question GetQuestion(int questionID)
        {
            return quizConnection.GetQuestion(questionID);
        }

        public void InitialseQuestions(int NumberOfQuestions) {
            List<Question> AllQuestions = GetAllQuestions();
            for (int i = 1; i <= NumberOfQuestions; i++) {
                LimitedQuestions.Add(AllQuestions[i]);
            }
        }

        public Question GetNextQuestion () {
            Question NextQuestion = LimitedQuestions[QuestionCount];
            QuestionCount++;
            return NextQuestion;
        }

        public bool HasNextQuestion() {
            return QuestionCount < LimitedQuestions.Count;
        }
    }
}
