using PracticeExamV2.Models;
using PracticeExamV2.Utilities;

namespace API.Models.Repositories;


public interface ICoursesRepository
{
    Course? Get(int id);

    IEnumerable<Course> GetAll();


    int Update(int id, Course courses);

}
