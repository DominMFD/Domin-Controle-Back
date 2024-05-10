public interface IExamService {

    List<ExamModel> ListAllExams(int page);
    ExamModel AddExam(ExamDto examDto);
}