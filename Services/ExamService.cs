using Microsoft.AspNetCore.Razor.Language;

public class ExamService  : IExamService{

    private ControlContext context;
    public ExamService(ControlContext context) {
        this.context = context;
    }

    public List<ExamModel> ListAllExams(int page) {
        if(page < 1) page = 1;

        int limit = 10;
        int offset = (page - 1) * limit;

        return context.Exams.Skip(offset).Take(limit).ToList();
    }

    public ExamModel getAExam(long id) {
        var exam = context.Exams.Find(id);

        if (exam == null) throw new ExamsError("Exam not found");

        return exam;
    }

    public List<ExamModel> getAllExamsInAlphabeticalOrder(int page) {
        if(page < 1) page = 1;

        int limit = 10;
        int offset = (page - 1) * limit;

        return context.Exams.OrderBy(i => i.Date).Skip(offset).Take(limit).ToList();
    }

    public ExamModel AddExam(ExamDto examDto) {

        if(examDto.Date == DateTime.MinValue) throw new ExamsError("Invalid Date");

        ExamModel exam = new ExamModel{
            Date = examDto.Date,
            Hematocrito = examDto.Hematocrito,
            Rni = examDto.Rni,
        };

        context.Exams.Add(exam);
        context.SaveChanges();

        return exam;
    }
}