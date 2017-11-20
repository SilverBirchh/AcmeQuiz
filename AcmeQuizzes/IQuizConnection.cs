using System;
using System.Collections.Generic;

namespace AcmeQuizzes
{
    /**
     * Interface for UI Clients to use to request Questions and to perform CRUD operations
     */
    public interface IQuizConnection
    {
        Question GetQuestion(int questionID);
        List<Question> GetAllQuestions();
        void SaveQuestion(Question question);
        void DeleteQuestion(Question question);
        void EditQuestion(Question question);
    }
}
