using ExamApplication.Models;

namespace ExamApplication.Repository
{
    public interface IInstructor
    {
        void insertInstructor(Instructor instructor);
        void updateInstructor(Instructor instructor);
        void deleteInstructor (Instructor instructor);
        List<Instructor> getInstructors();
        Instructor getInstructorById(int id);
        Instructor getInstructorByEmail(String email);
    }
}
