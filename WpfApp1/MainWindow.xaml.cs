using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // array to store random phrases for the bot to say
        private string[] botPhrases = { "that's a great question!", "wow!!!! so cool", "can you explain further?", "interesting point!" };

        // random object used to pick a random index from the array
        private Random rnd = new Random();

        public MainWindow()
        {
            // links the c# code to the xaml design
            InitializeComponent();

            // testing
        }

        // runs when the user clicks inside the textbox
        private void UserInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserInput.Text == "Click here to enter message")
            {
                UserInput.Text = "";
                UserInput.Foreground = Brushes.Black;
            }
        }

        // runs when the user clicks away from the textbox
        private void UserInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserInput.Text))
            {
                UserInput.Text = "Click here to enter message";
                UserInput.Foreground = Brushes.Gray;
            }
        }

        // helper method to dynamically create chat bubbles
        private void AddBubble(string text, bool isUser)
        {
            Border bubble = new Border

            {
                Padding = new Thickness(12, 8, 12, 8),
                Margin = new Thickness(0, 5, 0, 5),
                MaxWidth = 350,

                // condition ? valueiftrue : valueiffalse
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,

                Background = isUser ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ABB7FF")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0E0E0")),

                CornerRadius = isUser ? new CornerRadius(15, 15, 0, 15) : new CornerRadius(15, 15, 15, 0)

            };

            // creating a textbox to hold the message

            TextBlock content = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Foreground = isUser ? Brushes.White : Brushes.Black

            };

            bubble.Child = content;

            ChatStack.Children.Add(bubble);

            // scrolls the view to the latest message automatically

            ChatStack.BringIntoView();
        }

        // async allows the use of 'await' so the ui doesn't freeze during delays
        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string message = UserInput.Text; // Console.Readline();

            if (!string.IsNullOrWhiteSpace(message) && message != "Click here to enter message")
            {
                AddBubble(message, true);

                UserInput.Clear();

                TxtStatus.Text = "chatbot is typing...";
                TxtStatus.Visibility = Visibility.Visible;

                await Task.Delay(1500);

                TxtStatus.Visibility = Visibility.Hidden;

                string response = botPhrases[rnd.Next(botPhrases.Length)];

                AddBubble(response, false); // Console.Writeline();

            }
        }
                
        // clears the entire conversation history
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ChatStack.Children.Clear();

            AddBubble("Chat cleared! How can I help you now? :)", false);

            TxtStatus.Text = "chatbot is ready...";
        }

       
    }
}