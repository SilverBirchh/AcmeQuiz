using System;
using System.IO;
using System.Collections.Generic;

/**
 * IQuizRepository implementation. Creates an IQuizConnection connection with a DatabaseFilePath set to the Quiz.sqlite
 */
namespace AcmeQuizzes
{
    public class QuizRespository : IQuizRepository
    {
        IQuizConnection quizConnection;

        public QuizRespository() : this(new QuizConnection(DatabaseFilePath)) { }

        public QuizRespository(IQuizConnection iQuizConnection)
        {
            quizConnection = iQuizConnection;
        }

        /**
         * File path of the Quiz DB that should be installed on the users device
         */
        static string DatabaseFilePath
        {
            get
            {
                string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                string path = Path.Combine(FolderPath, "Quiz.sqlite");
                return path;
            }
        }

        /**
         * Returns the entire list of questions installed in the sqlite DB
         * @return List<Question>
         */
        public List<Question> GetAllQuestions()
        {
            return quizConnection.GetAllQuestions();
        }

        /**
         * Returns a single question based of a questionID
         * @param int questionID
         * @return Question
         */
        public Question GetQuestion(int questionID)
        {
            return quizConnection.GetQuestion(questionID);
        }

        /**
         * Save a brand new Question to the DB
         * @param Question - Fully qualified Question Object
         */
        public void SaveQuestion(Question question)
        {
            quizConnection.SaveQuestion(question);
        }

        /**
         * Deletes a question from the DB
         * @param Question - Fully qualified Question Object
         */
        public void DeleteQuestion(Question question)
        {
            quizConnection.DeleteQuestion(question);
        }

        /**
         * Edits an existing Question in the DB
         * @param Question - Fully qualified Question Object
         */
        public void EditQuestion(Question question)
        {
            quizConnection.EditQuestion(question);
        }

    }
}
