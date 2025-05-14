public interface IExamService {

    List<ExamModel> ListAllExams(string sortBy, string order, int page);
    ExamModel GetAExam(long id);
    ExamModel AddExam(ExamDto examDto);
    void DeleteExam(long id);
}