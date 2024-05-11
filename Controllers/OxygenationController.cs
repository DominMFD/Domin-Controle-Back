using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/oxygenation")]
public class OxygenationController : ControllerBase {

    private IOxygenationService service;

    public OxygenationController(IOxygenationService service) {
        this.service = service;
    }

    [HttpGet]
    public IActionResult ListAllOxygenation(int page = 1) {
        var oxygenation = service.ListAllOxygenation(page);

        return StatusCode(200, oxygenation);
    }

    [HttpPost]
    public IActionResult AddOxygenation([FromBody] OxygenationDto oxygenationDto) {
        try {

            var oxygenation = service.AddOxygenation(oxygenationDto);
            return StatusCode(201, oxygenation);

        } catch(OxygenationError error) {
            return StatusCode(400, new OxygenationError(error.Message));
        }
    }
 }