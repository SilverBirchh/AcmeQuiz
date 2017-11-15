using System;
using System.Collections.Generic;

namespace AcmeQuizzes
{
    public interface IQuizRepository
    {
        Question GetQuestion(int questionID);
        List<Question> GetAllQuestions();

    }
}
