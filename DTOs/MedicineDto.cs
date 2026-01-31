public class MedicineDto
{
    public required string Name { get; set; }
    public required string Dosage { get; set; }
    public required string Description { get; set; }
    public required IFormFile Image { get; set; }
}