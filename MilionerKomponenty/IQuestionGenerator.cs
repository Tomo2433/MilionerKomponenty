using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilionerKomponenty
{
    public interface IQuestionGenerator
    {
        void SetSubject(string subject);

        void SetAnswerCount(int count);

        int GetAnswerCount();

        string GetSubject();

        //throws error if subject or answer count is not set
        //throws error if server not responded after some time
        void Generate();

        void GenerateHelp(Response question);

        // returns true if backend didn't finish with response
        bool IsGenerating();

        //throws error if used before it gets full response.
        //throws shiny error if somehow LLM generate < answerCount
        Response FetchResponse();

        string GetHelp();

        //shows full response from server. needs to finish first
        void ShowDebug();


    }
}
