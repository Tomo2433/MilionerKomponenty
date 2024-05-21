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
            checkGenerationTimer.Interval = 500; // Sprawdzaj co 500 ms
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

            // Aktualizacja pola tekstowego z pytaniem
            textBox1.Text = gameState.GetQuestion();

            // Aktualizacja tekstów przycisków z odpowiedziami
            List<string> answers = gameState.GetAnswers();
            button1.Text = answers[0];
            button2.Text = answers[1];
            button3.Text = answers[2];
            button4.Text = answers[3];
        }

        private async void AnswerButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            string selectedAnswer = clickedButton.Text;

            if (selectedAnswer == gameState.GetCorrectAnswer())
            {
                //MessageBox.Show("Poprawna odpowiedŸ! Przechodzisz do kolejnego pytania.");
                //clickedButton.BackColor = Color.Green;
                FetchAndDisplayQuestion();
            }
            else
            {
                //clickedButton.BackColor = Color.Red;
                MessageBox.Show("Niestety, to nie jest poprawna odpowiedŸ. Koniec gry.");
                // Tutaj mo¿esz umieœciæ kod obs³uguj¹cy zakoñczenie gry
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}