using System;
using System.Collections.Generic;

namespace AcmeQuizzes
{
    public interface IQuizConnection
    {
        Question GetQuestion(int questionID);
        List<Question> GetAllQuestions();
    }
}
