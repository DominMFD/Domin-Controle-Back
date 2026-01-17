using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Medicine")]
public class MedicineModel {

  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public long Id {get; set;}

  [Required]
  public required string Image {get; set;}
  [Required]
  public required string Name {get; set;}
  [Required]
  public required string Dosage {get; set;}
  [Required]
  public required string Description {get; set;}
}