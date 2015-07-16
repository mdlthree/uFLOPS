using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace uFLOPS_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppViewModel = new ViewModel();
            radioButton_Addition.IsChecked = true;
        }

        #region Constructor
        public ViewModel AppViewModel
        {
            get { return this.DataContext as ViewModel; }
            set { this.DataContext = value; }
        }
        #endregion
        

        //Calculator Events
        private void pressEquals(object sender, RoutedEventArgs e) 
        {
            AppViewModel.pressEquals();
        }

        private void pressNumber(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            AppViewModel.addInputChars(button.Content.ToString());
        }

        private void radio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            string qType = button.Content.ToString();

            switch (qType)
            {
                case "Addition":
                    AppViewModel.QuestionType = Question.Operations.Addition;
                    break;
                case "Subtraction":
                    AppViewModel.QuestionType = Question.Operations.Subtraction;
                    break;
                case "Multiplication":
                    AppViewModel.QuestionType = Question.Operations.Multiplication;
                    break;
                case "Division":
                    AppViewModel.QuestionType = Question.Operations.Division;
                    break;
                default:
                    AppViewModel.QuestionType = Question.Operations.Addition;
                    break;
            }
        }

        private void Button_Click_GenerateExam(object sender, RoutedEventArgs e)
        {
            AppViewModel.buildExam();
        }
    }
}
