using DomainLayer.Entities;
using RepositoryLayer.Data;
using RepositoryLayer.Exceptions;
using RepositoryLayer.Repositories.Interfaces;

namespace RepositoryLayer.Repositories.Implementations;

public class GroupRepository:IRepository<Group>
{
    public void Create(Group data)
    {
        try
        {
            if (data is null)throw new NotFoundException("data not found");
            AppDbContext < Group > .datas.Add(data);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void Update(Group data)
    {
        var group = Get(g => g.Id == data.Id);
        if (group is null)
            throw new Exception("Belə bir qrup tapılmadı!");

        group.Name = data.Name;
        group.RoomCount = data.RoomCount;
    }

    public void Delete(Group data)
    {
        var group = Get(g => g.Id == data.Id);
        if (group is null)
            throw new Exception("Belə bir qrup tapılmadı!");

        AppDbContext<Group>.datas.Remove(group);
    }

    public Group Get(Predicate<Group> predicate)
    {
        return predicate != null ? AppDbContext<Group>.datas.Find(predicate) : null;
        
    }

    public List<Group> GetAll(Predicate<Group> predicate=null)
    {
        return predicate != null ? AppDbContext<Group>.datas.FindAll(predicate) : AppDbContext<Group>.datas;
    }
}