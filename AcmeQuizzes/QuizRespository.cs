using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace AcmeQuizzes
{
    public class QuizRespository : IQuizRepository
    {
        private IQuizConnection quizConnection = null;

        public QuizRespository() : this(new QuizConnection(DatabaseFilePath)) { }

        public QuizRespository(IQuizConnection iQuizConnection)
        {
            this.quizConnection = iQuizConnection;
        }

        private static string DatabaseFilePath
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

    }
}
