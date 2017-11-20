using System;
using System.Collections.Generic;

namespace AcmeQuizzes
{
    /**
     * Class to manage a quiz session. Is designed to keep track of questions that have been answered by the user, 
     * keeps a list of all questions to be aksed in a session and which question should be asked next.
     */
    public class QuizManager
    {
        // Used to interact with the SQLite DB
        QuizRespository quizRepository = new QuizRespository();

        // Stores the questions to ask the user in a session
        List<Question> LimitedQuestions = new List<Question>();

        // Used to keep track of which question to ask next and when the end of the session will be.
        int QuestionCount = 1;

        // Keeps track of the quetions answered by the user and if they were correct. Is static so that 
        // the list can persist across activities.
        public static Dictionary<int, bool> AnsweredQuestions = new Dictionary<int, bool>();

        public QuizManager() { }

        /**
         * Method that should be used to initialise a quiz session.
         * Resets the AnsweredQuestions Dictionary. Fetches all questions
         * randomises the list to make sure that the user does not get the same 
         * questions everytime. Finally adds to LimitedQuestions List the 
         * number of questions that the user requested.
         * 
         * @param int NumberOfQuestions - The number of questions the user wants to answer
         */
        public void InitialseQuestions(int NumberOfQuestions)
        {
            AnsweredQuestions = new Dictionary<int, bool>();

            List<Question> AllQuestions = quizRepository.GetAllQuestions();
            AllQuestions.Randomise();
            for (int i = 1; i <= NumberOfQuestions; i++)
            {
                LimitedQuestions.Add(AllQuestions[i]);
            }
        }

        /**
         * Fetches the next question for the user from the LimitedQuestions List
         * Increases QuestionCount in order to keep track of the next question
         * to fetch.
         * 
         * @return Fully Qualified Question Object
         */
        public Question GetNextQuestion()
        {
            Question NextQuestion = LimitedQuestions[QuestionCount - 1];
            QuestionCount++;
            return NextQuestion;
        }

        /**
         * Method to check if the user has answered all of the questions that they 
         * requested. Compares the QuestionCount with LimitedQuestions.Count.
         * As long as QuestionCount is less than or equal to LimitedQuestions.Count
         * there will be another question.
         * 
         * @return bool - If the user has another question.
         */
        public bool HasNextQuestion()
        {
            return QuestionCount <= LimitedQuestions.Count;
        }

        /**
         * Method used to record an answer to a question. Works out if the answer the 
         * user gave as equal to the correct answer.
         * 
         * @param int QuestionID - The Id of the question that has just been answered
         * @param int Answer - The numeric value that the user selected as the answer between 1 and 5
         * @param string CorrectAnswer - The actual option that is the correct answer to the question between 1 and 5
         */
        public void AnswerQuestion(int QuestionID, int Answer, string CorrectAnswer)
        {
            Answer++;
            bool IsCorrect = Answer.ToString().Equals(CorrectAnswer);
            AnsweredQuestions.Add(QuestionID, IsCorrect);
        }
    }
}
