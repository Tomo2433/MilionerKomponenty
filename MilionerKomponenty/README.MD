
# CHATGPT MI TO NAPISA£
# Generator Pytañ
## **Ogólny Opis**
Obie implementacje s¹ zgodne z interfejsem IQuestionGenerator. Jedna z nich reprezentuje rzeczywisty generator u¿ywany w moim systemie, który komunikuje siê z backendem z LM Studio. Dodatkowo, stworzy³em generator pozorowany, aby u³atwiæ pracê lokalnie. Wa¿ne jest zauwa¿enie, ¿e zachowanie generatora pytañ ró¿ni siê pod wzglêdem czasowym ze wzglêdu na asynchroniczn¹ naturê, poniewa¿ zale¿y to od szybkoœci odpowiedzi z LM.

## **Zachowanie**
Generator pozorowany odzwierciedla to zachowanie poprzez losowe opóŸnienia od 2 do 10 sekund. Istotne jest pamiêtanie, ¿e LLM to nadal LLM; nie zawsze dostarcza dok³adnych wyników. Podczas testów zazwyczaj dzia³a dobrze, ale generator pozorowany zwraca równie¿ przypadki niepoprawnych wyników — o jeden mniej, jeden wiêcej lub powtarza odpowiedŸ. Dlatego konieczne jest zweryfikowanie wyników po swojej stronie.

## **Metody**
Metoda Generate() jest standardowa, obejmuj¹ca opóŸnienie. Jednak¿e, dla szybkiego iteracyjnego testowania, do³¹czy³em GenerateInstant(), który zapewnia natychmiastowe wyniki.

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
