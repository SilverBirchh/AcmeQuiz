using System;
using System.Collections.Generic;
using SQLite;
using SQLite.Net;

namespace AcmeQuizzes
{
    public class QuizConnection : IQuizConnection
    {
        SQLiteConnection connection;

        public QuizConnection(string dbLocation)
        {

            this.connection = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbLocation);
            connection.CreateTable<Question>();
        
        }

        public List<Question> GetAllQuestions()
        {
            return new List<Question>(connection.Table<Question>());
        }

        public Question GetQuestion(int questionID)
        {
            return connection.Get<Question>(questionID);
        }
    }
}
