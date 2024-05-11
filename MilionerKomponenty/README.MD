

# CHATGPT MI TO NAPISA£
# Generator Pytañ
## **Ogólny Opis**

Obie implementacje s¹ zgodne z interfejsem IQuestionGenerator. Jedna z nich to rzeczywisty generator, który komunikuje siê z backendem z LM Studio. Dodatkowo, **stworzy³em dummy**, abyœcie mogli próbowaæ coœ robiæ. Dummy stara sie jak najblizej odwzorowac wlasciwe dzialanie.

## **Zachowanie**
Wa¿ne jest info, ¿e ogó³em **generowanie odpowiedzi  nie jest instant**. zazwyczaj <3s, ale czasami throwuje i po 8s. Dummy ze wzglêdu na to ma async i sleep na tasku na 2-10s. Istotne jest pamiêtanie, ¿e LLM to nadal LLM, wiec czasami odjebuje. Z tego co zauwa¿y³em rzadko kiedy zdarza, ze generuje mniej pytañ ni¿ potrzebne. Rozwi¹za³em to przez generowanie 2 wiêcej ni¿ pytane i potem odrzucanie nadmiarowych co w chuj pomog³o, ale czasami dalej odjebie. No i dalem ze **Dummy ma 5% szans na Exception ze za malo odpowiedzi**, bo wlasciwy komponent czasami tez rzuci Exception jak za malo dostanie

## **Metody**
Metoda Generate() jest standardowa, obejmuj¹ca opóŸnienie. Dla szybkiego testowania, do³¹czy³em GenerateInstant(), który natychmiast zwraca.

## **Interfejs**

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
    //throws shiny error if somehow LLM generate < answerCount
    Response FetchResponse();
    
    //shows full response from server. needs to finish first
    void ShowDebug();

## **Przyk³adowe U¿ycie**
Normalny Generator:


    var x = new QuestionGenerator();
    x.SetSubject("religion");
    x.SetAnswerCount(4);
    x.Generate();
    while (x.IsGenerating())
    {
        Console.WriteLine(".");
        Thread.Sleep(100);
    }
    var res = x.FetchResponse();
    res.Print();

Generator Pozorowany:

    var x = new DummyGenerator();
    x.SetSubject("religion"); // Ignorowane
    x.SetAnswerCount(4);      // Wybierz 8, 4 lub 2
    x.Generate();             // Losowe opóŸnienie od 2 do 10s
    while (x.IsGenerating())
    {
        Console.WriteLine(".");
        Thread.Sleep(100);
    }
    var res = x.FetchResponse(); // Zwraca losow¹ odpowiedŸ z listy dla 8, 4 lub 2
    res.Print();
a to instant:
	

	var x = new DummyGenerator();
    x.SetSubject("religion"); // Ignorowane
    x.SetAnswerCount(4);      // Wybierz 8, 4 lub 2
    x.GenerateInstant();             // Losowe opóŸnienie od 2 do 10s
    var res = x.FetchResponse(); // Zwraca losow¹ odpowiedŸ z listy dla 8, 4 lub 2
    res.Print();
