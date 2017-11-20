using System;
using SQLite;
using SQLite.Net.Attributes;

/**
 * Definition of a Question Object
 */
namespace AcmeQuizzes
{
    [Table("Questions")]
    public class Question
    {
        public Question()
        {

        }

        public Question(int questionID,
                        string questionText,
                        string option1,
                        string option2,
                        string option3,
                        string option4,
                        string option5,
                        string correctAnswer)
        {

        }

        /**
         * QuestionID - The unique identifier for every question. Autoincrement by SQLite service.
         * @Type int
         */

        [PrimaryKey, AutoIncrement]
        public int QuestionID
        {
            get;
            set;
        }

        /**
         * QuestionText - The question to ask the user
         * Required
         * @Type string
         */
        public string QuestionText
        {
            get;
            set;
        }

        /**
         * Option1 - 1st option to ask the user
         * Required
         * @Type string
         */
        public string Option1
        {
            get;
            set;
        }

        /**
         * Option2 - 2nd option to ask the user
         * Required
         * @Type string
         */
        public string Option2
        {
            get;
            set;
        }

        /**
         * Option3 - 3rd option to ask the user
         * Required
         * @Type string
         */
        public string Option3
        {
            get;
            set;
        }

        /**
         * Option4 - 4th option to ask the user
         * Required
         * @Type string
         */
        public string Option4
        {
            get;
            set;
        }

        /**
         * Option5 - 5th option to ask the user
         * Optional
         * @Type string
         */
        public string Option5
        {
            get;
            set;
        }

        /**
         * CorrectAnswer - Correct answer. Should be a sting value linked to the correct option. i.e. "1" "2" "3" "4" "5"
         * Required
         * @Type string
         */
        public string CorrectAnswer
        {
            get;
            set;
        }
    }
}
