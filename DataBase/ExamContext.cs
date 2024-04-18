using Microsoft.EntityFrameworkCore;

public class ExamContext : DbContext {

  public DbSet<ExamModel> Exams {get; set;} = null!;
  public ExamContext(DbContextOptions<ExamContext> options) : base(options) {

  }
}