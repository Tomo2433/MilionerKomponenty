using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MilionerKomponenty
{
    public class DummyGenerator : IQuestionGenerator
    {
        private Task? task;
        private string subject = string.Empty;
        private int answerCount;
        private string result = string.Empty;


        public void SetSubject(string subject)
        {
            this.subject = subject;
        }


        public void SetAnswerCount(int count)
        {
            answerCount = count;
        }


        public int GetAnswerCount()
        {
            return answerCount;
        }

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
            task = Start();
        }

        public void GenerateInstant()
        {
            if (answerCount == 0 || subject == string.Empty)
            {
                throw new Exception("Subject or answer count incorrect");
            }
            result = "instant";
        }

        public Response FetchResponse()
        {
            if (result == string.Empty) { throw new Exception("Result not processed"); }
            Random random = new Random();
            List<Response> responses8 = new List<Response>
            {
                new Response("Famous Actors", "Who played the character Neo in the movie 'The Matrix'?", "Keanu Reeves", new List<string> { "Tom Cruise", "Brad Pitt", "Leonardo DiCaprio", "Will Smith", "Keanu Reeves", "Johnny Depp", "Robert Downey Jr.", "Hugh Jackman" }),
                new Response("Planets", "Which planet is known as the 'Red Planet'?", "Mars", new List<string> { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" }),
                new Response("Languages", "What is the most spoken language in the world?", "Mandarin Chinese", new List<string> { "Spanish", "English", "Hindi", "Arabic", "Portuguese", "Russian", "Japanese", "Mandarin Chinese" }),
                new Response("Animals", "What is the largest mammal on Earth?", "Blue Whale", new List<string> { "African Elephant", "Giraffe", "Grizzly Bear", "Polar Bear", "Sperm Whale", "Blue Whale", "Killer Whale", "Hippopotamus" }),
                new Response("Sports", "Which sport is played on a court called a 'pitch'?", "Soccer", new List<string> { "Basketball", "Soccer", "Tennis", "Cricket", "Rugby", "Volleyball", "Field Hockey", "Badminton" }),
                new Response("Books", "Who is the author of 'To Kill a Mockingbird'?", "Harper Lee", new List<string> { "John Steinbeck", "Ernest Hemingway", "F. Scott Fitzgerald", "Harper Lee", "Mark Twain", "J.D. Salinger", "George Orwell", "Toni Morrison" })
            };
            List<Response> responses4 = new List<Response>
            {
                new Response("Animals", "What is the fastest land animal?", "Cheetah", new List<string> { "Lion", "Elephant", "Gazelle", "Cheetah" }),
                new Response("History", "Which city is considered the birthplace of Poland?", "Gdansk", new List<string> { "Krakow", "Warsaw", "Poznan", "Gdansk" }),
                new Response("Food", "What is the main ingredient in a traditional hummus recipe?", "Chickpeas", new List<string> { "Beans", "Eggs", "Rice", "Chickpeas" }),
                new Response("Religion", "Who is the founder of Buddhism?", "Gautama Buddha", new List<string> { "Jesus Christ", "Moses", "Zoroaster", "Gautama Buddha" }),
                new Response("Colors", "What color is formed by mixing red and yellow?", "Orange", new List<string> { "Purple", "Green", "Orange", "Yellow" }),
                new Response("Countries", "Which country is known as the 'Land of the Rising Sun'?", "Japan", new List<string> { "China", "India", "Japan", "South Korea" }),
                new Response("Musical Instruments", "What instrument is known as the 'King of Instruments'?", "Pipe organ", new List<string> { "Violin", "Piano", "Guitar", "Pipe organ" }),
                new Response("Fruits", "Which fruit is known as the 'King of Fruits'?", "Durian", new List<string> { "Apple", "Banana", "Durian", "Mango" }),
                new Response("Mountains", "Which mountain is known as the 'Roof of the World'?", "Mount Everest", new List<string> { "K2", "Mount Kilimanjaro", "Mount Everest", "Mount Fuji" }),
                new Response("Elements", "Which element has the symbol 'Fe'?", "Iron", new List<string> { "Gold", "Silver", "Copper", "Iron" })
            };

            List<Response> responses2 = new List<Response>
        {
            new Response("American Presidents", "Who was the 16th President of the United States?", "Abraham Lincoln", new List<string> { "George Washington", "Abraham Lincoln" }),
            new Response("Drugs", "What is the primary component of opium?", "Morphine", new List<string> { "Codeine", "Morphine" }),
            new Response("Swords", "Which sword is known as the \"Sword of Allah\"?", "Scimitar", new List<string> { "Katana", "Scimitar" }),
            new Response("Planets", "Which planet is closest to the Sun?", "Mercury", new List<string> { "Venus", "Mercury" }),
            new Response("Oceans", "Which ocean is the largest by area?", "Pacific Ocean", new List<string> { "Atlantic Ocean", "Pacific Ocean" }),
            new Response("Continents", "Which continent is known as the 'Land Down Under'?", "Australia", new List<string> { "North America", "Australia" }),
            new Response("Months", "Which month has 30 days?", "April", new List<string> { "February", "April" }),
            new Response("Seasons", "Which season is characterized by falling leaves?", "Autumn", new List<string> { "Spring", "Autumn" }),
            new Response("Days of the Week", "Which day comes after Saturday?", "Sunday", new List<string> { "Saturday", "Sunday" })
            };
            if (random.Next(100) <= 5) { throw new Exception("Not Enough Distinct Values From LLM"); }
            if (answerCount == 8) { return responses8[random.Next(responses8.Count)];} 
            else if (answerCount == 4) { return responses4[random.Next(responses4.Count)]; }
            else{ return responses2[random.Next(responses2.Count)]; }
        }

        public void ShowDebug()
        {
            Console.WriteLine("No server log in dummy");
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

        private async Task Start()
        {
            Random random = new Random();
            int delayMilliseconds = random.Next(2000, 10001);


            task = Task.Delay(delayMilliseconds);

            await task;
            result = "task";
        }
    }
}
