using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase {

  [HttpGet]
  public ExamModel BomDia() {

    return new ExamModel {
      Id = 2323242424,
      Date = DateTime.Now,
      Hematocrito = 59.5,
      Rni = 1.5,
    };
  }
}