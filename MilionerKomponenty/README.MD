

# CHATGPT MI TO NAPISA�
# Generator Pyta�
## **Og�lny Opis**

Obie implementacje s� zgodne z interfejsem IQuestionGenerator. Jedna z nich to rzeczywisty generator, kt�ry komunikuje si� z backendem z LM Studio. Dodatkowo, **stworzy�em dummy**, aby�cie mogli pr�bowa� co� robi�. Dummy stara sie jak najblizej odwzorowac wlasciwe dzialanie.

## **Zachowanie**
Wa�ne jest info, �e og�em **generowanie odpowiedzi  nie jest instant**. zazwyczaj <3s, ale czasami throwuje i po 8s. Dummy ze wzgl�du na to ma async i sleep na tasku na 2-10s. Istotne jest pami�tanie, �e LLM to nadal LLM, wiec czasami odjebuje. Z tego co zauwa�y�em rzadko kiedy zdarza, ze generuje mniej pyta� ni� potrzebne. Rozwi�za�em to przez generowanie 2 wi�cej ni� pytane i potem odrzucanie nadmiarowych co w chuj pomog�o, ale czasami dalej odjebie. No i dalem ze **Dummy ma 5% szans na Exception ze za malo odpowiedzi**, bo wlasciwy komponent czasami tez rzuci Exception jak za malo dostanie

## **Metody**
Metoda Generate() jest standardowa, obejmuj�ca op�nienie. Dla szybkiego testowania, do��czy�em GenerateInstant(), kt�ry natychmiast zwraca.

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

## **Przyk�adowe U�ycie**
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
    x.Generate();             // Losowe op�nienie od 2 do 10s
    while (x.IsGenerating())
    {
        Console.WriteLine(".");
        Thread.Sleep(100);
    }
    var res = x.FetchResponse(); // Zwraca losow� odpowied� z listy dla 8, 4 lub 2
    res.Print();
a to instant:
	

	var x = new DummyGenerator();
    x.SetSubject("religion"); // Ignorowane
    x.SetAnswerCount(4);      // Wybierz 8, 4 lub 2
    x.GenerateInstant();             // Losowe op�nienie od 2 do 10s
    var res = x.FetchResponse(); // Zwraca losow� odpowied� z listy dla 8, 4 lub 2
    res.Print();
