using System;
using System.Linq;
using System.Collections.Generic;

namespace uFLOPS_Desktop
{
    public class Exam
    {
        private List<Question> _ExamQuestions = new List<Question>();
        private Question.Operations _anOperation;

        private int _NumberOfQuestions = 0;
        private int _HighNumber = 0;
        private int _LowNumber = 0;
        private int _CurrentQuestion = 0;

        private bool _IsCompleted = false;

        private int _NumberCorrect = 0;
        private int _ExamDuration = 0;
        private int _UFLOPS = 0;
        
        private string _ExamResults;

        public Exam(int numOfQuestions, int highNumber, int lowNumber, Question.Operations anOperation) 
        {
            _NumberOfQuestions = numOfQuestions;
            _HighNumber = highNumber;
            _LowNumber = lowNumber;
            _anOperation = anOperation;

            generateQuestions();
        }

        private void generateQuestions()
        {
            for (int i = 0; i < _NumberOfQuestions; i++)
            {
                _ExamQuestions.Add(new Question(_HighNumber, _LowNumber, _anOperation));
            }
        }

        public string getCurrentQuestion()
        {
            return _ExamQuestions[_CurrentQuestion].QuestionText;
        }

        public string GetExamResults()
        {
            if (_ExamResults != null)
            {
                return _ExamResults;
            }
            else
            {
                return String.Format("Something went wrong marking the exam");
            }
        }

        public bool IsComplete
        {
            get { return _IsCompleted; }
        }

        public void AnswerQuestion(int anAttempt)
        {
            _ExamQuestions[_CurrentQuestion].Attempt = anAttempt;
            if (_CurrentQuestion < _NumberOfQuestions - 1)
            {
                _CurrentQuestion++;
            }
            else
            {
                markExam();
                makeExamResults();
                _IsCompleted = true;
            }
        }

        private void markExam()
        {
            //find count of correct answers
            _NumberCorrect = _ExamQuestions.Count(n => n.IsCorrect);

            //find total time to take exam
            DateTime minTime = _ExamQuestions.Min(n => n.AnswerTime);
            DateTime maxTime = _ExamQuestions.Max(n => n.AnswerTime);
            TimeSpan timeDiff = maxTime.Subtract(minTime);
            _ExamDuration = (int) timeDiff.TotalSeconds;
            
            //calculate the famous uFLOPS statistic
            _UFLOPS = (int) (_NumberCorrect / timeDiff.TotalSeconds * 1000);
        }

        private void makeExamResults()
        {
            List<string> s = new List<string>();
            s.Add(String.Format("Congrats you got {0} / {1} questions correct!", _NumberCorrect, _NumberOfQuestions));
            s.Add(String.Format("This test took you {0} seconds", _ExamDuration));
            s.Add(String.Format("Your uFLOPS rating is {0}", _UFLOPS));
            _ExamResults = string.Join("\n\n", s);
        }
    }
}
