public interface IExamService {

    List<ExamModel> ListAllExams(string sortBy, string order, int page);
    ExamModel getAExam(long id);
    ExamModel AddExam(ExamDto examDto);
}