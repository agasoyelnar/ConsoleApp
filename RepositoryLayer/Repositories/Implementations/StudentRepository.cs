using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories.Implementations;

public class StudentRepository : IRepository<Student>
{
    public void Create(Student data)
    {
        AppDbContext<Student>.datas.Add(data);
    }

    public void Update(Student data)
    {
        var student = Get(s => s.Id == data.Id);
        if (student is null)
            throw new Exception("Belə bir tələbə tapılmadı!");

        student.Name = data.Name;
        student.Surname = data.Surname;
        student.Age = data.Age;
        student.Group = data.Group;
    }

    public void Delete(Student data)
    {
        var student = Get(s => s.Id == data.Id);
        if (student is null)
            throw new Exception("Belə bir tələbə tapılmadı!");

        AppDbContext<Student>.datas.Remove(student);
    }

    public Student Get(Predicate<Student> predicate)
    {
        return predicate != null ? AppDbContext<Student>.datas.Find(predicate) : null;
    }

    public List<Student> GetAll(Predicate<Student> predicate = null)
    {
        return predicate != null ? AppDbContext<Student>.datas.FindAll(predicate) : AppDbContext<Student>.datas;
    }
}