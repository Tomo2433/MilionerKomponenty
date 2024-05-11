using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace MilionerKomponenty
{
    public class Response
    {
        public string Category { get; }
        public string Question { get; }
        public string CorrectAnswer { get; }
        public IReadOnlyList<string> Answers { get; }

        public void Print()
        {
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Question: {Question}");
            Console.WriteLine($"Correct Answer: {CorrectAnswer}");
            Console.WriteLine("All Answers:");
            foreach (string answer in Answers)
            {
                Console.WriteLine(answer);
            }
        }

        public Response(string category, string question, string correctAnswer, List<string> answers)
        {
            this.Category = category;
            this.Question = question;
            this.CorrectAnswer = correctAnswer;
            this.Answers = answers;
        }

    }
}
