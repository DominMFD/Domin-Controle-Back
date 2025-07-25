using Microsoft.EntityFrameworkCore;

public class ExamService  : IExamService{

    private ControlContext context;
    public ExamService(ControlContext context) {
        this.context = context;
    }

    public List<ExamModel> ListAllExams(string sortBy, string order, int page) {
        if(page < 1) page = 1;

        int limit = 10;
        int offset = (page - 1) * limit;

        if(sortBy.ToLower() == "date") {
            if(order.ToLower() == "desc") {
                return context.Exams.OrderByDescending(e => e.Date).Skip(offset).Take(limit).ToList();
            }
            
        }

        if(sortBy.ToLower() == "hema") {
            if(order.ToLower() == "desc") {
                return context.Exams.OrderByDescending(e => e.Hematocrito).Skip(offset).Take(limit).ToList();
            }
            return context.Exams.OrderBy(e => e.Hematocrito).Skip(offset).Take(limit).ToList();
        }

         if(sortBy.ToLower() == "rni") {
            if(order.ToLower() == "desc") {
                return context.Exams.OrderByDescending(e => e.Rni).Skip(offset).Take(limit).ToList();
            }
            return context.Exams.OrderBy(e => e.Rni).Skip(offset).Take(limit).ToList();
        }

        return context.Exams.OrderBy(e => e.Date).Skip(offset).Take(limit).ToList();
    }

    public ExamModel GetAExam(long id) {
        var exam = context.Exams.Find(id) ?? throw new ExamsError("Exam not found");
    return exam;
    }


    public ExamModel AddExam(ExamDto examDto) {

        if(examDto.Date == DateTime.MinValue) throw new ExamsError("Invalid Date");

        ExamModel exam = new ExamModel{
            Date = examDto.Date,
            Hematocrito = examDto.Hematocrito,
            Rni = examDto.Rni,
            Marevan = examDto.Marevan,
        };

        context.Exams.Add(exam);
        context.SaveChanges();

        return exam;
    }

    public void DeleteExam(long id)
    {
       int affected = context.Exams
                      .Where(e => e.Id == id)
                      .ExecuteDelete();

if (affected == 0)
    throw new ExamsError("Exam not found");
    }
}