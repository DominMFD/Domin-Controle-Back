using Microsoft.EntityFrameworkCore;

public class ControlContext : DbContext {

  public DbSet<ExamModel> Exams {get; set;}
  public DbSet<OxygenationModel> Oxygenation {get; set;}
  public ControlContext(DbContextOptions<ControlContext> options) : base(options) {

  }
}