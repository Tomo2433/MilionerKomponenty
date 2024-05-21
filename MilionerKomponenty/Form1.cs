using Timer = System.Windows.Forms.Timer;

namespace MilionerKomponenty
{
    public partial class Form1 : Form
    {
        private GameState gameState;
        private Timer checkGenerationTimer;

        public Form1()
        {
            InitializeComponent();
            gameState = new GameState();
            SetupTimer();
            FetchAndDisplayQuestion();

            button1.Click += AnswerButtonClick;
            button2.Click += AnswerButtonClick;
            button3.Click += AnswerButtonClick;
            button4.Click += AnswerButtonClick;
        }

        private void SetupTimer()
        {
            checkGenerationTimer = new Timer();
            checkGenerationTimer.Interval = 500;
            checkGenerationTimer.Tick += CheckGenerationStatus;
        }

        private async void FetchAndDisplayQuestion()
        {
            pictureBoxLoading.Visible = true;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;

            await gameState.FetchAndUpdate();
            checkGenerationTimer.Start();
        }

        private void CheckGenerationStatus(object sender, EventArgs e)
        {
            if (!gameState.IsGenerating())
            {
                checkGenerationTimer.Stop();
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            pictureBoxLoading.Visible = false;
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;

           
            textBox1.Text = gameState.GetQuestion();

            List<string> answers = gameState.GetAnswers();
            button1.Text = answers[0];
            button2.Text = answers[1];
            button3.Text = answers[2];
            button4.Text = answers[3];

            button1.BackColor = Color.Navy;
            button2.BackColor = Color.Navy;
            button3.BackColor = Color.Navy;
            button4.BackColor = Color.Navy;
        }

        private async void AnswerButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedAnswer = clickedButton.Text;

            if (selectedAnswer == gameState.GetCorrectAnswer())
            {
                clickedButton.BackColor = Color.Green;
                await Task.Delay(2000); 
                FetchAndDisplayQuestion();
            }
            else
            {
                clickedButton.BackColor = Color.Red;
                await Task.Delay(500); 
                MessageBox.Show("Niestety, to nie jest poprawna odpowiedü. Koniec gry.");
                Application.Exit(); 
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}