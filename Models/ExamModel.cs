using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Exams")]
public class ExamModel {

  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public long Id {get; set;}

  [Required]
  [DataType(DataType.DateTime)]
  public DateTime Date {get; set;}
  public double Hematocrito {get; set;}
  public double Rni {get; set;}
  [Required]
  public required string Marevan {get; set;}
}