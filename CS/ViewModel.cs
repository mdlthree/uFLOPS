using System;
using System.ComponentModel;

namespace uFLOPS_Desktop
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _InputValue;
        private string _ExamResults;
        private string _CurrentQuestion;

        private int _NumberOfQuestions;
        private int _HighestNumber;
        private int _LowestNumber;

        private Question.Operations _AnOperation;
        private Exam _AnExam;

        public ViewModel()
        {
            InputValue = "";
            ExamResults = "Welcome To uFLOPS";
            CurrentQuestion = "";
            HighestNumber = "10";
            LowestNumber = "1";
            NumberOfQuestions = "10";
            _AnOperation = Question.Operations.Addition;
        }

        public string InputValue {
            get { return _InputValue; }
            set
            {
                if (_InputValue != value)
                {
                    _InputValue = value;
                    OnPropertyChanged("InputValue");
                }
            }
        }

        public string ExamResults
        {
            get { return _ExamResults; }
            set
            {
                if (_ExamResults != value)
                {
                    _ExamResults = value;
                    OnPropertyChanged("ExamResults");
                }
            }
        }

        public string CurrentQuestion
        {
            get { return _CurrentQuestion; }
            set
            {
                if (_CurrentQuestion != value)
                {
                    _CurrentQuestion = value;
                    OnPropertyChanged("CurrentQuestion");
                }
            }
        }

        public string NumberOfQuestions
        {
            get 
            { 
                return _NumberOfQuestions.ToString(); 
            }
            set
            {
                int t;
                bool result = Int32.TryParse(value, out t);
                if (result)
                {
                    if (_NumberOfQuestions != t)
                    {
                        _NumberOfQuestions = t;
                        OnPropertyChanged("NumberOfQuestions");
                    }
                }
                else
                {
                    ExamResults = String.Format("Somethings is wrong with the number questions, {0}", value);
                }
            }
        }

        public string HighestNumber
        {
            get 
            { 
                return _HighestNumber.ToString();
            }
            set
            {
                int t;
                bool result = Int32.TryParse(value, out t);
                if (result)
                {
                    if (_HighestNumber != t)
                    {
                        _HighestNumber = t;
                        OnPropertyChanged("HighestNumber");
                    }
                }
                else
                {
                    ExamResults = String.Format("Somethings is wrong with the highest number, {0}", value);
                }
            }
        }

        public string LowestNumber
        {
            get { return _LowestNumber.ToString(); }
            set
            {
                int t;
                bool result = Int32.TryParse(value, out t);
                if (result)
                {
                    if (_LowestNumber != t)
                    {
                        _LowestNumber = t;
                        OnPropertyChanged("LowestNumber");
                    }
                }
                else
                {
                    ExamResults = String.Format("Somethings is wrong with the lowest number, {0}", value);
                }
            }
        }

        public Question.Operations QuestionType
        {
            set { _AnOperation = value; }
        }

        public void buildExam()
        {
            _AnExam = new Exam(_NumberOfQuestions, _HighestNumber, _LowestNumber, _AnOperation);
            CurrentQuestion = _AnExam.getCurrentQuestion();
            ExamResults = "New Quiz Running...";
        }
        
        public void addInputChars(string aButtonInput)
        {
            InputValue += aButtonInput;
        }

        public void pressEquals()
        {
            int t;
            bool result = Int32.TryParse(InputValue, out t);
            if (result)
            {
                _AnExam.AnswerQuestion(t);
                if (!_AnExam.IsComplete) 
                {
                    CurrentQuestion = _AnExam.getCurrentQuestion();
                }
                else
                {
                    ExamResults = _AnExam.GetExamResults();
                }
            }
            else
            {
                ExamResults = String.Format("Something went wrong with the equals button, {0} \nPlease try again.", InputValue);
            }
            InputValue = "";
            //
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
