using DomainLayer.Entities;
using RepositoryLayer.Repositories.Implementations;
using ServiceLayer.Services;

namespace ServiceLayer.Services.Implementations;

public class StudentService : IStudentService
{
    private StudentRepository _studentRepository = new();
    private int _count = 1;

    public Student Create(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name))
            throw new Exception("Tələbənin adı boş ola bilməz!");

        if (string.IsNullOrWhiteSpace(student.Surname))
            throw new Exception("Tələbənin soyadı boş ola bilməz!");

        if (student.Age <= 0)
            throw new Exception("Tələbənin yaşı düzgün deyil!");

        if (student.Group is null)
            throw new Exception("Tələbənin qrupu boş ola bilməz!");

        student.Id = _count++;
        _studentRepository.Create(student);
        return student;
    }
    public Student GetById(int id)
    {
        var student = _studentRepository.Get(s => s.Id == id);
        if (student is null)
            throw new Exception("Bu ID-yə aid tələbə tapılmadı!");

        return student;
    }
    public void Delete(int id)
    {
        var student = _studentRepository.Get(s => s.Id == id);
        if (student is null)
            throw new Exception("Silinəcək tələbə tapılmadı!");

        _studentRepository.Delete(student);
    }
    public Student Update(int id, Student student)
    {
        var existStudent = _studentRepository.Get(s => s.Id == id);
        if (existStudent is null)
            throw new Exception("Yenilənəcək tələbə tapılmadı!");

        existStudent.Name = student.Name;
        existStudent.Surname = student.Surname;
        existStudent.Age = student.Age;
        existStudent.Group = student.Group;

        return existStudent;
    }

    public List<Student> GetAllByAge(int age)
    {
        if (age <= 0)
            throw new Exception("Yaş 0-dan böyük olmalıdır!");

        var students = _studentRepository.GetAll(s => s.Age == age);
    
        if (students is null || students.Count == 0)
            throw new Exception("Bu yaşda tələbə tapılmadı!");

        return students;
    }

    public List<Student> GetAllByGroupId(int groupId)
    {
        if (groupId <= 0)
            throw new Exception("Qrup ID-si 0-dan böyük olmalıdır!");
        var students=_studentRepository.GetAll(s=>s.Group.Id==groupId);
        if (students.Count == 0)
            throw new Exception("Bu qrupa aid tələbə tapılmadı!");
        return students;
    }
    public List<Student> SearchByNameOrSurname(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
            throw new Exception("Axtarış üçün yer boş ola bilməz!");

        var students = _studentRepository.GetAll(s => 
            s.Name.ToLower().Contains(search.ToLower()) || 
            s.Surname.ToLower().Contains(search.ToLower()));

        if (students.Count == 0)
            throw new Exception("Bu ada və ya soyadа uyğun tələbə tapılmadı!");

        return students;
    }
}