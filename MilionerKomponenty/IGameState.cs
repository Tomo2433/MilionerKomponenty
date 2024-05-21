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
    public interface IGameState
    {
        string GetQuestion();
        List<string> GetAnswers();
        string GetCorrectAnswer();
        bool IsGenerating();
        Task FetchAndUpdate();
    }
}
