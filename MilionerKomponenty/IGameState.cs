// string pytanie
// string[] odpowiedzi
// string poprawna

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilionerKomponenty
{
    public enum GameProgress {
        GAME_IN_PROGRESS,
        GAME_OVER_WIN,
        GAME_OVER_LOSS
    };
    public interface IGameState
    {

        string GetQuestion();
        List<string> GetAnswers();
        string GetCorrectAnswer();
        bool IsGenerating();
        Task FetchAndUpdate();

        void ResetGame();

        void PlayerResponded(bool is_response_correct);
        GameProgress GetGameProgress();
        int GetCurrentReward();

    }
}
