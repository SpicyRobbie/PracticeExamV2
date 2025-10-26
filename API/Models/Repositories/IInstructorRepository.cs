using PracticeExamV2.Models;
using PracticeExamV2.Utilities;


namespace API.Models.Repositories;

public interface IInstructorRepository
{
    IEnumerable<Instructor> GetAll();

    Instructor? Get(int id);

    int Update(int id, Instructor instructors);

}
