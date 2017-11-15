using System;
using System.Collections.Generic;
using SQLite;

namespace AcmeQuizzes
{
    public class QuizConnection : IQuizConnection
    {
        SQLiteConnection connection;

        public QuizConnection(string dbLocation)
        {
            connection = new SQLiteConnection(dbLocation);
              connection.CreateTable<Question>();
        }

        public List<Question> GetAllQuestions()
        {
            return new List<Question>(connection.Table<Question>());
        }

        public Question GetQuestion(int questionID)
        {
            return null;
        }
    }
}
