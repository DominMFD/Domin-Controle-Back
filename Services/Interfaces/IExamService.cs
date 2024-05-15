public interface IExamService {

    List<ExamModel> ListAllExams(int page);
    ExamModel getAExam(long id);
    List<ExamModel> getAllExamsInAlphabeticalOrder(int page);
    ExamModel AddExam(ExamDto examDto);
}