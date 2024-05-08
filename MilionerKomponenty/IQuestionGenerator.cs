﻿using System;
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

        // returns true if backend didn't finish with response
        bool IsGenerating();

        //throws error if used before it gets full response.
        // Answers field can contain more or less answers. It depends on whims of LLM. you need to do sanity check first
        Response FetchResponse();

        //shows full response from server. needs to finish first
        void ShowDebug();


    }
}
