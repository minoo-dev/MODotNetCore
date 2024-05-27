using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonStr = await File.ReadAllTextAsync("data.json");

//JSON to C#
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr);

//Console.WriteLine(jsonStr);

foreach (var question in model.questions)
{
    Console.WriteLine(question.questionNo);
}

Console.ReadLine();


public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}
