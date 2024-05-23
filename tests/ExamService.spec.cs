using Xunit;

public class ExamServiceTest {

    [Fact]
    public async Task AddExamInDataBase() {

        //Arrange
        var examDate = DateTime.Now;
        var examHematocrito = 56.5;
        var examRni = 2.5;

        var examDto = new ExamDto{
            Date = examDate,
            Hematocrito = examHematocrito,
            Rni = examRni,
        };

        await using var context = new MockDb().CreateDbContext();
        var examService = new ExamService(context);

        //Att
        var result = examService.AddExam(examDto);

        //Assert
        Assert.Collection(context.Exams, exam => {
            Assert.Equal(examDate, exam.Date);
            Assert.Equal(examHematocrito, exam.Hematocrito);
            Assert.Equal(examRni, exam.Rni);
        });
    }

    [Fact]
    public async Task GetAExam() {

        //Arrange
        var examId = 1;
        var examDate = DateTime.Now;
        var examHematocrito = 56.5;
        var examRni = 2.5;

        var examDto = new ExamDto{
            Date = examDate,
            Hematocrito = examHematocrito,
            Rni = examRni,
        };
        var examDto2 = new ExamDto{
            Date = examDate,
            Hematocrito = examHematocrito,
            Rni = examRni,
        };

        await using var context = new MockDb().CreateDbContext();
        var examService = new ExamService(context);

        //Att
        examService.AddExam(examDto);
        examService.AddExam(examDto2);

        var result = examService.getAExam(examId);

        //Assert
        Assert.Equal(result.Id, examId);
    }   

    [Fact]
    public async Task GetAllExams() {
         //Arrange
        var sortBy = "date";
        var order = "asc";
        var page = 1;

        var examDate = DateTime.Now;
        var examHematocrito = 56.5;
        var examRni = 2.5;

        var examDate2 = DateTime.Now;
        var examHematocrito2 = 55.5;
        var examRni2 = 1.5;

        var examDto = new ExamDto{
            Date = examDate,
            Hematocrito = examHematocrito,
            Rni = examRni,
        };
        var examDto2 = new ExamDto{
            Date = examDate2,
            Hematocrito = examHematocrito2,
            Rni = examRni2,
        };
        
        await using var context = new MockDb().CreateDbContext();
        var examService = new ExamService(context);

        var exam1 = examService.AddExam(examDto);
        var exam2 = examService.AddExam(examDto2);

        List<ExamModel> exams = [exam1, exam2];

        var result = examService.ListAllExams(sortBy, order, page);
        var ascHema = examService.ListAllExams("hema", "asc", 1);
        var descHema = examService.ListAllExams("hema", "desc", 1);
        var ascRni = examService.ListAllExams("rni", "asc", 1);
        var descRni = examService.ListAllExams("rni", "desc", 1);

        //Assert
        Assert.Equal(result, exams);
        Assert.Equal(2, result.Count);
        Assert.True(ascHema.SequenceEqual(exams.OrderBy(e => e.Hematocrito)));
        Assert.True(descHema.SequenceEqual(exams.OrderByDescending(e => e.Hematocrito)));
        Assert.True(ascRni.SequenceEqual(exams.OrderBy(e => e.Rni)));
        Assert.True(descRni.SequenceEqual(exams.OrderByDescending(e => e.Rni)));
    }
}