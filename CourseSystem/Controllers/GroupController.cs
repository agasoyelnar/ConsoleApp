using CourseSystem.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;

namespace CourseSystem.Controllers;

public class GroupController
{
    GroupService _groupService=new(); 

    public void Create()
    {
        Helper.PrintConsole(ConsoleColor.Blue, text: "Qrupun adını daxil edin: ");
        string groupName = Console.ReadLine();

        Helper.PrintConsole(ConsoleColor.Blue, text: "Müəllimin adını daxil edin: ");
        string teacher = Console.ReadLine();

        SelectCase:
        Helper.PrintConsole(ConsoleColor.White, text: "Otaq sayını daxil edin: ");
        string groupRoomCount = Console.ReadLine();
        int roomCount;
        bool isRoomCount = int.TryParse(groupRoomCount, out roomCount);
        if (isRoomCount)
        {
            try
            {
                Group group = new Group { Name = groupName, Teacher = teacher, RoomCount = roomCount };
                var result = _groupService.Create(group);
                Helper.PrintConsole(ConsoleColor.Green, text: $"Qrup uğurla yaradıldı! Id: {result.Id}, Ad: {result.Name}, Müəllim: {result.Teacher}, Otaq sayı: {result.RoomCount}");
            }
            catch (Exception ex)
            {
                Helper.PrintConsole(ConsoleColor.Red, text: ex.Message);
            }
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, text: "Zəhmət olmasa düzgün rəqəm daxil edin!");
            goto SelectCase;
        }
    }

    public void GetById()
    {
        groupId:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Qrupun ID-sini daxil edin: ");
        string groupId = Console.ReadLine();
        int id;
        bool isgroupId = int.TryParse(groupId, out id);
        if (isgroupId)
        {
            Group group = _groupService.GetById(id);
            if (group != null)
            {
                Helper.PrintConsole(ConsoleColor.Green, text: $"Qrup Id: {group.Id}, Ad: {group.Name}, Otaq sayı: {group.RoomCount}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, text: "Qrup tapılmadı!");
                goto groupId;
            }
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, text: "Zəhmət olmasa düzgün ID daxil edin!");
            goto groupId;
        }
        
    }

    public void GetAll()
    {
        List<Group> groups = _groupService.GetAll();
        if (groups.Count != 0)
        {
            foreach (var group in groups)
            {
                Helper.PrintConsole(ConsoleColor.Green, $"Group Id: {group.Id}, Ad: {group.Name}, Otaq sayi: {group.RoomCount}\n");
            }
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, "Ad daxil edin");
        }
    }
    public void Delete()
    {
        Helper.PrintConsole(ConsoleColor.Blue, text: "Silinəcək qrupun ID-sini daxil edin: ");
        string groupId = Console.ReadLine();
        int id;
        bool isId = int.TryParse(groupId, out id);
        if (isId)
        {
            try
            {
                _groupService.Delete(id);
                Helper.PrintConsole(ConsoleColor.Green, text: $"Qrup uğurla silindi!");
            }
            catch (Exception ex)
            {
                Helper.PrintConsole(ConsoleColor.Red, text: ex.Message);
            }
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, text: "Zəhmət olmasa düzgün ID daxil edin!");
        }
    }
    public void Update()
    {
        Helper.PrintConsole(ConsoleColor.Blue, text: "Yenilənəcək qrupun ID-sini daxil edin: ");
        string groupId = Console.ReadLine();
        int id;
        bool isId = int.TryParse(groupId, out id);
        if (isId)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Yeni qrup adını daxil edin: ");
            string groupName = Console.ReadLine();

            SelectCase:
            Helper.PrintConsole(ConsoleColor.White, text: "Yeni otaq sayını daxil edin: ");
            string groupRoomCount = Console.ReadLine();
            int roomCount;
            bool isRoomCount = int.TryParse(groupRoomCount, out roomCount);
            if (isRoomCount)
            {
                try
                {
                    Group group = new Group { Name = groupName, RoomCount = roomCount };
                    var result = _groupService.Update(id, group);
                    Helper.PrintConsole(ConsoleColor.Green, text: $"Qrup uğurla yeniləndi! Id: {result.Id}, Ad: {result.Name}");
                }
                catch (Exception ex)
                {
                    Helper.PrintConsole(ConsoleColor.Red, text: ex.Message);
                }
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, text: "Zəhmət olmasa düzgün rəqəm daxil edin!");
                goto SelectCase;
            }
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, text: "Zəhmət olmasa düzgün ID daxil edin!");
        }
    }

}