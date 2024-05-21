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
        bool generating;

        public GameState() { 
            answercount = 4;
            g = new DummyGenerator();
            g.SetSubject("world war");
            g.SetAnswerCount(answercount);
            r = new Response("", "", "", new List<string>{""});
            generating = true;
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
        public bool IsGenerating() {
            return generating;
        }

        public async Task FetchAndUpdate()
        {
            generating = true;
            await Task.Run(async () =>
            {
                g.Generate();
                while(g.IsGenerating()) {
                    // spinlock
                }
                r = g.FetchResponse();
                generating = false;
            });
        }

    }
}