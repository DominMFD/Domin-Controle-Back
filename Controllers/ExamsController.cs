using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/exams")]
public class ExamsController : ControllerBase {

  private IExamService _service;

  public ExamsController(IExamService service) {
    _service = service;
  }

  [HttpGet]
  public IActionResult List(int page = 1) {

    var exams = _service.Lista(page);
    return StatusCode(200, exams);

  }

  [HttpPost]
  public IActionResult Create([FromBody] ExamDto examDto) {

    try {

      var exam = _service.Incluir(examDto);
      return StatusCode(201, exam);

    } catch(ExamsError error) {
      return StatusCode(400, new ExamsError(error.Message));
    }

    

    

  }
}