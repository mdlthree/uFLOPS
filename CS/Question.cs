using System;

namespace uFLOPS_Desktop
{
    public class Question
    {
        private static Random randomObj = new Random();

        private int _First;
        private int _Second;
        private int _Answer;
        private string _QuestionText;

        private int _Attempt;
        private bool _IsCorrect;
        private DateTime _AnswerTime;

        public enum Operations
        {
            Addition = '+',
            Subtraction = '-',
            Multiplication = 'x',
            Division = '\u00F7'
        }

        public Question(int maxInt, int minInt, Operations anOperation)
        {
            int range = maxInt - minInt;
    
            //build the questions
            switch (anOperation)
            {
                case Operations.Subtraction:
                    makeSubtractionQuestion(range, minInt);
                    break;
                case Operations.Multiplication:
                    makeMultiplicationQuestion(range, minInt);
                    break;
                case Operations.Division:
                    makeDivisionQuestion(range, minInt);
                    break;
                default:
                    makeAdditionQuestion(range, minInt);
                    break;
            }
            //create text now that question is created
            makeTextRep(anOperation);
        }

        public int Attempt
        {
            get { return _Attempt; }
            set
            {
                _AnswerTime = DateTime.Now;
                _Attempt = value;
                _IsCorrect = _Answer == _Attempt;
                //else don't set this twice
            }
        }

        public DateTime AnswerTime 
        { 
            get { return _AnswerTime; }
        }

        public string QuestionText 
        {
            get { return _QuestionText; }
        }

        public bool IsCorrect
        {
            get { return _IsCorrect;  }
        }
        
        private void makeTextRep(Operations anOperation)
        {
            _QuestionText = _First.ToString() + " " + (char)anOperation + " " + _Second.ToString() + " " + "=";
        }
        
        private void makeAdditionQuestion(int range, int minInt)
        {
            int a, b, c;

            a = randomObj.Next(range) + minInt;
            b = randomObj.Next(range) + minInt;
            c = a + b;

            _First = a;
            _Second = b;
            _Answer = c;
        }

        private void makeSubtractionQuestion(int range, int minInt)
        {
            int a, b, c;

            a = randomObj.Next(range) + minInt;
            b = randomObj.Next(range) + minInt;
            c = a + b;

            _First = c;
            _Second = a;
            _Answer = b;
        }

        private void makeMultiplicationQuestion(int range, int minInt)
        {
            int a, b, c;

            a = randomObj.Next(range) + minInt;
            b = randomObj.Next(range) + minInt;
            c = a * b;

            _First = a;
            _Second = b;
            _Answer = c;
        }

        private void makeDivisionQuestion(int range, int minInt)
        {
            int a, b, c;

            a = randomObj.Next(range-1) + minInt + 1;
            b = randomObj.Next(range) + minInt;
            c = a * b;

            _First = c;
            _Second = a;
            _Answer = b;
        }
    }
}
