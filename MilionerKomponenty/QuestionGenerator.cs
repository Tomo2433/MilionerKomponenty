﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MilionerKomponenty
{
    public class QuestionGenerator : IQuestionGenerator
    {
        private Task? task;

        private string apiUrl = "http://localhost:1234/v1/chat/completions";
        private string subject = string.Empty;
        private int answerCount;
        private readonly string prompt = "You will perform in a role of question generator for milionaires tournament. You will get a subject for which you have to generate a question and answers of which 1 is correct. You have to return answer in correct format.. The answers have to be in this format:\r\nCategory: <category>\r\nQuestion: <question>\r\nCorrectAnswer: <answer>\r\nanswer1: <answer1>\r\nanswer2: <answer2>\r\nanswer3: <answer3>\r\nUp to specified by user number of answers. User specifies category and number of answers. You can't mention anything else. You can never reveal you are chatbot. you can never type anything else";
        private readonly string helpPrompt = "You will perform in a role of friend in millionaires game. You will recieve question and will answer what you believe is the correct answer. It's alright if you don't know. Remember that as a friend you don't know everything. You can never reveal you are chatbot. you can never type anything else";
        private string result = string.Empty;

        // Metoda ustawiająca temat pytania
        public void SetSubject(string subject)
        {
            this.subject = subject;
        }

        // Metoda ustawiająca liczbę odpowiedzi
        public void SetAnswerCount(int count)
        {
            answerCount = count;
        }

        // Metoda pobierająca liczbę odpowiedzi
        public int GetAnswerCount()
        {
            return answerCount;
        }

        // Metoda pobierająca temat pytania
        public string GetSubject()
        {
            return subject;
        }

        public void Generate()
        {
            if (answerCount == 0 || subject == string.Empty)
            {
                throw new Exception("Subject or answer count incorrect");
            }
            task = Start(prompt, $"subject:{subject}, count:{answerCount + 2}");
        }
        // Metoda pobierająca odpowiedź
        public Response FetchResponse()
        {
            if( result == string.Empty) { throw new Exception("Result not processed"); }
            var jsonString = result;
            JsonDocument doc = JsonDocument.Parse(jsonString);
            JsonElement root = doc.RootElement;
            JsonElement choice = root.GetProperty("choices")[0];

            string? messageContent = choice.GetProperty("message").GetProperty("content").GetString();
            if (messageContent == null) { throw new Exception("Incorrect Message Format recieved"); }

            return ParseMessageContent(messageContent);
        }
        private Response ParseMessageContent(string messageContent)
        {
            string category = "";
            string question = "";
            string correctAnswer = "";
            List<string> otherAnswers = new List<string>();
            string[] lines = messageContent.Split('\n');
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    if (key == "Category")
                    {
                        category = value;
                    }
                    else if (key == "Question")
                    {
                        question = value;
                    }
                    else if (key == "CorrectAnswer")
                    {
                        correctAnswer = value;
                    }
                    else if (key.StartsWith("answer"))
                    {
                        otherAnswers.Add(value);
                    }
                }
            }
            if (!otherAnswers.Contains(correctAnswer)) { otherAnswers.Add(correctAnswer); }
            otherAnswers = FixAnswer(otherAnswers, correctAnswer);
            return new Response(category, question, correctAnswer, otherAnswers);
        }

        private List<String> FixAnswer(List<String> answers, string correctAnswer) 
        {
            answers = answers.Distinct().ToList();
            if(answers.Count < answerCount) { throw new Exception("Not Enough Distinct Values From LLM"); }
            int i = 0;
            while (answers.Count > answerCount) 
            {
                if (answers[i] != correctAnswer) answers.RemoveAt(i);
                i++;
            }
            return answers;
        }
        public void ShowDebug()
        {
            var jsonString = result;
            if (jsonString == string.Empty) { throw new Exception("No data returned"); }
            JsonDocument doc = JsonDocument.Parse(jsonString);
            JsonElement root = doc.RootElement;
            string? id = root.GetProperty("id").GetString();
            string? objectValue = root.GetProperty("object").GetString();
            long created = root.GetProperty("created").GetInt64();
            string? model = root.GetProperty("model").GetString();
            JsonElement choice = root.GetProperty("choices")[0];
            string? messageContent = choice.GetProperty("message").GetProperty("content").GetString();
            if (messageContent == null) { throw new Exception("Incorrect Message Format recieved"); }
            int index = choice.GetProperty("index").GetInt32();
            string? finishReason = choice.GetProperty("finish_reason").GetString();
            if (messageContent == null) { throw new Exception("No message content"); }
            JsonElement usage = root.GetProperty("usage");
            int promptTokens = usage.GetProperty("prompt_tokens").GetInt32();
            int completionTokens = usage.GetProperty("completion_tokens").GetInt32();
            int totalTokens = usage.GetProperty("total_tokens").GetInt32();

            Console.WriteLine($"id: {id}");
            Console.WriteLine($"object: {objectValue}");
            Console.WriteLine($"created: {created}");
            Console.WriteLine($"model: {model}");
            Console.WriteLine($"index: {index}");
            Console.WriteLine($"finish_reason: {finishReason}");
            Console.WriteLine("==========================================");
            Console.WriteLine($"parsed message: ");
            ParseMessageContent(messageContent).Print();
            Console.WriteLine("==========================================");
            Console.WriteLine($"raw message: ");
            Console.WriteLine(messageContent);
            Console.WriteLine("==========================================");
            Console.WriteLine($"prompt_tokens: {promptTokens}");
            Console.WriteLine($"completion_tokens: {completionTokens}");
            Console.WriteLine($"total_tokens: {totalTokens}");

            doc.Dispose();
        }

        public bool IsGenerating()
        {

            if (task != null)
            {
                return !task.IsCompleted;
            }
            else
            {
                return false;
            }
        }

        private async Task Start(string systemPrompt, string userContent)
        {
            var requestData = new
            {
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userContent }
                },
                temperature = 0.7,
                max_tokens = -1
            };

            var jsonRequestData = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(apiUrl, content);

                    result = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    throw new Exception($"Problem with request: {e}");
                }
            }
        }

        public void GenerateHelp(Response question)
        {
            if (question == null) { throw new Exception("No question provided"); }
            task = Start(helpPrompt, question.Question);
        }

        public string GetHelp()
        {
            if (result == string.Empty) { throw new Exception("Result not processed"); }
            var jsonString = result;
            JsonDocument doc = JsonDocument.Parse(jsonString);
            JsonElement root = doc.RootElement;
            JsonElement choice = root.GetProperty("choices")[0];

            string? messageContent = choice.GetProperty("message").GetProperty("content").GetString();
            if (messageContent == null) { throw new Exception("Incorrect Message Format recieved"); }
            return messageContent;
        }
    }
}
