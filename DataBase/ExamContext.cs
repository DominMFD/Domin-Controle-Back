using Microsoft.EntityFrameworkCore;

public class ExamContext : DbContext {

  public DbSet<ExamModel> Exams {get; set;}
  public ExamContext(DbContextOptions<ExamContext> options) : base(options) {

  }
}