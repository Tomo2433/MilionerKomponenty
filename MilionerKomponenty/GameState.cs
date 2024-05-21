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
        IQuestionGenerator g;
        bool generating;
        Response r;

        // progi pieniezne
        int[] rewards;
        int current_reward_ix;
        GameProgress game_progress;
        // kola ratunkowe

        public GameState(IQuestionGenerator generator) { 
            answercount = 4;
            g = generator;
            g.SetSubject("world war");
            g.SetAnswerCount(answercount);
            r = new Response("", "", "", new List<string>{""});
            generating = true;

            rewards = new int[]{
                500,
                1000,
                2000,
                5000,
                10000,
                20000,
                40000,
                75000,
                125000,
                250000,
                500000,
                1000000
            };
            current_reward_ix = 0;
            game_progress = GameProgress.GAME_IN_PROGRESS;
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

        public void ResetGame() {
            game_progress = GameProgress.GAME_IN_PROGRESS;
            current_reward_ix = 0;
        }

        public GameProgress GetGameProgress() {
            return game_progress;
        }

        public void PlayerResponded(bool is_response_correct) {
            if(is_response_correct) {
                if(current_reward_ix < rewards.Length) {
                    current_reward_ix++;
                }
                else {
                    game_progress = GameProgress.GAME_OVER_WIN;
                }
            }
            else {
                game_progress = GameProgress.GAME_OVER_LOSS;
            }
        }

        public int GetCurrentReward() {
            return rewards[current_reward_ix];
        }
    }
}