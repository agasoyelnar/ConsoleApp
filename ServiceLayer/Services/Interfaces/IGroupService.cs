using DomainLayer.Entities;
namespace ServiceLayer.Services;

public interface IGroupService
{
    Group Create(Group group);
    Group Update(int id, Group group);
    void Delete(int id);
    Group GetById(int id);
    List<Group> GetAll();
    
}   