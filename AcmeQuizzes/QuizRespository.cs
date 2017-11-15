using System;
using System.IO;
using System.Collections.Generic;

namespace AcmeQuizzes
{
    public class QuizRespository : IQuizRepository
    {
        private IQuizConnection quizConnection = null;
        private string dbLocation;

        public QuizRespository()
        {
            dbLocation = DatabaseFilePath;
            this.quizConnection = new QuizConnection(dbLocation);
        }

        private string DatabaseFilePath
        {
            get
            {
                string FileName = "Quiz.sqlite";
                string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;

                string path = Path.Combine(FolderPath, FileName);
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
    }
}
