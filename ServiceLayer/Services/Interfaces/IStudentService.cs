using DomainLayer.Entities;

namespace ServiceLayer.Services;

public interface IStudentService
{
    Student Create(Student student);
    Student GetById(int id);
    void Delete(int id);
    Student Update(int id, Student student);
    List<Student> GetAllByAge(int age);
    List<Student> GetAllByGroupId(int groupId);
    List<Student> SearchByNameOrSurname(string search);
}