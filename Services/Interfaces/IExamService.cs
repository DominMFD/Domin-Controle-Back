public interface IExamService {

    List<ExamModel> Lista(int page);
    ExamModel Incluir(ExamDto examDto);
}