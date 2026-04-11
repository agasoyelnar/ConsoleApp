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
        var existGroup = GetById(id);
        if (existGroup is null)
            throw new Exception("Yenilənəcək qrup tapılmadı!");

        group.Id = id;
        _groupRepository.Update(group);
        return group;
    }

    public void Delete(int id)
    {
        var group = GetById(id);
        if (group is null)
            throw new Exception("Silinəcək qrup tapılmadı!");

        _groupRepository.Delete(group);
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
    public List<Group> GetAllByTeacher(string teacher)
    {
        if (string.IsNullOrWhiteSpace(teacher))
            throw new Exception("Müəllim adı boş ola bilməz!");

        var groups = _groupRepository.GetAll(g => g.Teacher == teacher);
        if (groups.Count == 0)
            throw new Exception("Bu müəllimə aid qrup tapılmadı!");

        return groups;
    }

    public List<Group> GetAllByRoom(int roomCount)
    {
        if (roomCount <= 0)
            throw new Exception("Otaq sayı 0-dan böyük olmalıdır!");

        var groups = _groupRepository.GetAll(g => g.RoomCount == roomCount);
        if (groups.Count == 0)
            throw new Exception("Bu otaq sayına aid qrup tapılmadı!");

        return groups;
    }

    public List<Group> SearchByName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new Exception("Axtarış üçün ad boş ola bilməz!");

        var groups = _groupRepository.GetAll(g => g.Name.Contains(name));
        if (groups.Count == 0)
            throw new Exception("Bu ada uyğun qrup tapılmadı!");

        return groups;
    }

}

