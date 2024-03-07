using ExamApplication.Models;

namespace ExamApplication.Repository
{
    public interface IStudent
    {
        void insertStudent(Student student);
        void updateStudent(Student student);
        void deleteStudent(Student student);
        List<Student> getStudents();
        Student getStudentById(int id);
        Student getStudentByEmail(String email);
    }
}
