using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MODotNetCore.RestApiWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private async Task<LatHtaukBayDinModel> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("LatHtaukBayDinData.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDinModel>(jsonStr);
            return model;
        }

        // api/LatHtaukBayDin/questions
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestion()
        {
            var model = await GetDataAsync();
            return Ok(model.questions);
        }

        // api/LatHtaukBayDin/numbers
        [HttpGet("numbers")]
        public async Task<IActionResult> GetNumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.numberList);
        }

        // api/LatHtaukBayDin/{questionNo}/{answerNo}
        [HttpGet("{questionNo}/{answerNo}")] 
        public async Task<IActionResult> GetAnswer(int questionNo, int answerNo)
        {
            var model = await GetDataAsync();
            return Ok(model.answers.FirstOrDefault(x => x.questionNo == questionNo && x.answerNo == answerNo));
        }
    }
}

public class LatHtaukBayDinModel
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
