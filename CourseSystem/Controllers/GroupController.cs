using CourseSystem.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;

namespace CourseSystem.Controllers;

public class GroupController
{
    GroupService _groupService=new(); 

    public void Create()
    {
        Helper.PrintConsole(ConsoleColor.Blue, "Add Group Name: ");
        string groupName = Console.ReadLine();
        SelectCase:Helper.PrintConsole(ConsoleColor.White,"Add Group Roomcount: ");
        string groupRoomCount = Console.ReadLine();
        int roomCount;
        bool isRoomCount=int.TryParse(groupRoomCount, out roomCount);
        if (isRoomCount)
        {
            Group group = new Group { Name = groupName, RoomCount = roomCount };
            var result =_groupService.Create(group);
                                
            Helper.PrintConsole(ConsoleColor.Green, $"Group Id: {group.Id}, Name: {group.Name}, RoomCount: {group.RoomCount}\n");
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, "Please enter a valid number- ");
            goto SelectCase;
        }
    }

    public void GetById()
    {
        groupId:Helper.PrintConsole(ConsoleColor.Blue, "Add Group Id: ");
        string groupId = Console.ReadLine();
        int id;
        bool isgroupId=int.TryParse(groupId, out id);
        if (isgroupId)
        {
            Group group = _groupService.GetById(id);
            if (group != null)
            {
                Helper.PrintConsole(ConsoleColor.Green, $"Group Id: {group.Id}, Name: {group.Name}, RoomCount: {group.RoomCount}\n");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Red, "Group not found");
                goto groupId;
            }
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, "Add correct id types: ");
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
                Helper.PrintConsole(ConsoleColor.Green, $"Group Id: {group.Id}, Name: {group.Name}, RoomCount: {group.RoomCount}\n");
            }
        }
        else
        {
            Helper.PrintConsole(ConsoleColor.Red, "Please add group");
        }
    }
}