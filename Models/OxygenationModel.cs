using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Oxygenation")]
public class OxygenationModel {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id {get; set;}

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date {get; set;}

    [Required]
    public int Value {get; set;}

}