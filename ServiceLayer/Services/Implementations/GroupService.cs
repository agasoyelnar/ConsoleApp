using RepositoryLayer.Repositories.Implementations;
using DomainLayer.Entities;
namespace ServiceLayer.Services.Implementations;

public class GroupService:IGroupService
{
    private GroupRepository _groupRepository;
    private int _count = 1;

    public GroupService()
    {
        _groupRepository = new GroupRepository();
    }
    
    public Group Create(Group group)
    {
        group.Id = _count;
        _groupRepository.Create(group);
        _count++;
        return group;
    }

    public Group Update(int id, Group group)
    {
        throw new NotImplementedException();
        
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Group GetById(int id)
    {
        Group group = _groupRepository.Get(l=>l.Id == id );
        if (group is null) return null;
        return group;
    }

    public List<Group> GetAll()
    {
        return _groupRepository.GetAll();
    }
}

