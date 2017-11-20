using System.Collections.Generic;
using SQLite.Net;

/**
 * IQuizConnection implementation. Used to directly interact with the sqlite DB on the mobile device
 */
namespace AcmeQuizzes
{
    public class QuizConnection : IQuizConnection
    {
        SQLiteConnection connection;

        public QuizConnection(string dbLocation)
        {
            // Sets up connection to the Databse and will either create the table if it does not exist or retrieve it
            connection = new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), dbLocation);
            connection.CreateTable<Question>();

        }

        /**
         * Returns the entire list of questions installed in the sqlite DB
         * @return List<Question>
         */
        public List<Question> GetAllQuestions()
        {
            return new List<Question>(connection.Table<Question>());
        }

        /**
         * Returns a single question based of a questionID
         * @param int questionID
         * @return Question
         */
        public Question GetQuestion(int questionID)
        {
            return connection.Get<Question>(questionID);
        }

        /**
         * Save a brand new Question to the DB
         * @param Question - Fully qualified Question Object
         */
        public void SaveQuestion(Question question)
        {
            connection.Insert(question);
        }

        /**
         * Deletes a question from the DB
         * @param Question - Fully qualified Question Object
         */
        public void DeleteQuestion(Question question)
        {
            connection.Delete(new Question() { QuestionID = question.QuestionID });
        }

        /**
         * Edits an existing Question in the DB
         * @param Question - Fully qualified Question Object
         */
        public void EditQuestion(Question question)
        {
            connection.Update(question);
        }
    }
}
