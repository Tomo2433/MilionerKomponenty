using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilionerKomponenty
{
    public class GameState : IGameState 
    {
        int answercount;
        DummyGenerator g;
        Response r;

        public GameState() { 
            answercount = 4;
            g = new DummyGenerator();
            g.SetSubject("world war");
            g.SetAnswerCount(answercount);
            r = new Response("", "", "", new List<string>{""});
        }

        public void FetchAndUpdate() {
            g.Generate();
            while(g.IsGenerating()) {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine(".");
            r = g.FetchResponse();
        }

        public string GetQuestion() {
            return r.Question;
        }
        public List<string> GetAnswers() {
            return (List<string>) r.Answers;
        }
        public string GetCorrectAnswer() {
            return r.CorrectAnswer;
        }

    }
}