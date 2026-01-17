using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("medicine")]
public class MedicineController : ControllerBase {
  private IMedicineService service;

  public MedicineController(IMedicineService service) {
    this.service = service;
  }

  [HttpPost]
  public IActionResult AddMedicine([FromForm] MedicineDto medicineDto) {
    return Ok(medicineDto);
  }
}