using System;
using System.Collections.Generic;

namespace AcmeQuizzes
{
    /**
     * Interface that defines methods to directly interact with an SQLite DB. To only be called by business logic
     */
    public interface IQuizRepository
    {
        Question GetQuestion(int questionID);
        List<Question> GetAllQuestions();
        void SaveQuestion(Question question);
        void DeleteQuestion(Question question);
        void EditQuestion(Question question);
    }
}
