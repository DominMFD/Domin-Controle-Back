public class ExamService  : IExamService{

    private ExamContext _db;
    public ExamService(ExamContext db) {
        _db = db;
    }

    public List<ExamModel> Lista(int page) {

        if(page < 1) page = 1;

        int limit = 10;
        int offset = (page - 1) * limit;

        return _db.Exams.Skip(offset).Take(limit).ToList();
    }

    public ExamModel Incluir(ExamDto examDto) {

        if(examDto.Date == DateTime.MinValue) throw new ExamsError("Data inválida");

        ExamModel exam = new ExamModel{
            Date = examDto.Date,
            Hematocrito = examDto.Hematocrito,
            Rni = examDto.Rni,
        };

        _db.Exams.Add(exam);
        _db.SaveChanges();

        return exam;
    }
}