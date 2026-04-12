using CourseSystem.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;
using Spectre.Console;

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
       
        AnsiConsole.Status()
            .Start("Qrup bazaya əlavə edilir...", ctx => 
            {
                ctx.Spinner(Spinner.Known.Dots);
                Thread.Sleep(1500); 
            });
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
                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("[yellow]ID[/]");
                table.AddColumn("[cyan]Qrup Adı[/]");
                table.AddColumn("[green]Müəllim[/]");
                table.AddColumn("[magenta]Otaq Sayı[/]");
                table.AddRow(group.Id.ToString(), group.Name, group.Teacher, group.RoomCount.ToString());
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Qrup tapılmadı![/]");
                goto groupId;
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün ID daxil edin![/]");
            goto groupId;
        }
    }
    
    public void GetAll()
    {
        List<Group> groups = _groupService.GetAll();
        if (groups.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Sistemdə heç bir qrup yoxdur![/]");
            return;
        }

        var table = new Table().Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[cyan]Qrup Adı[/]");
        table.AddColumn("[green]Müəllim[/]");
        table.AddColumn("[magenta]Otaq Sayı[/]");

        foreach (var item in groups)
        {
            table.AddRow(item.Id.ToString(), item.Name, item.Teacher, item.RoomCount.ToString());
        }

        AnsiConsole.Write(table);

    }
    public void Delete()
    {
        deleteId:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Silinəcək qrupun ID-sini daxil edin: ");
        string groupId = Console.ReadLine();
        int id;
        bool isId = int.TryParse(groupId, out id);
        if (isId)
        {
            try
            {
                _groupService.Delete(id);
                AnsiConsole.Status()
                    .Start("Qrup silinir...", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Dots);
                        Thread.Sleep(1000);
                    });
                AnsiConsole.MarkupLine("[green]Qrup uğurla silindi![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                goto deleteId;  
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün ID daxil edin![/]");
            goto deleteId;
        }
    }

    public void Update()
    {
        updateId:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Yenilənəcək qrupun ID-sini daxil edin: ");
        string groupId = Console.ReadLine();
        int id;
        bool isId = int.TryParse(groupId, out id);
        if (isId)
        {
            Helper.PrintConsole(ConsoleColor.Blue, text: "Yeni qrup adını daxil edin: ");
            string groupName = Console.ReadLine();

            Helper.PrintConsole(ConsoleColor.Blue, text: "Yeni müəllimin adını daxil edin: ");
            string teacher = Console.ReadLine();

            SelectCase:
            Helper.PrintConsole(ConsoleColor.White, text: "Yeni otaq sayını daxil edin: ");
            string groupRoomCount = Console.ReadLine();
            int roomCount;
            bool isRoomCount = int.TryParse(groupRoomCount, out roomCount);
            if (isRoomCount)
            {
                try
                {
                    Group group = new Group { Name = groupName, Teacher = teacher, RoomCount = roomCount };
                    var result = _groupService.Update(id, group);
                    AnsiConsole.MarkupLine($"[green]Qrup uğurla yeniləndi! Id: {result.Id}, Ad: {result.Name}, Müəllim: {result.Teacher}, Otaq sayı: {result.RoomCount}[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                    goto updateId; 
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün rəqəm daxil edin![/]");
                goto SelectCase;
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün ID daxil edin![/]");
            goto updateId;
        }
    }
    public void GetAllByTeacher()
    {
        teacherName:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Müəllimin adını daxil edin: ");
        string teacher = Console.ReadLine();

        try
        {
            var groups = _groupService.GetAllByTeacher(teacher);

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[cyan]Qrup Adı[/]");
            table.AddColumn("[green]Müəllim[/]");
            table.AddColumn("[magenta]Otaq Sayı[/]");

            foreach (var group in groups)
            {
                table.AddRow(group.Id.ToString(), group.Name, group.Teacher, group.RoomCount.ToString());
            }

            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            goto teacherName;  
        }
    }
    public void GetAllByRoom()
    {
        SelectCase:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Otaq sayını daxil edin: ");
        string roomCountStr = Console.ReadLine();
        int roomCount;
        bool isRoomCount = int.TryParse(roomCountStr, out roomCount);
        if (isRoomCount)
        {
            try
            {
                var groups = _groupService.GetAllByRoom(roomCount);

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("[yellow]ID[/]");
                table.AddColumn("[cyan]Qrup Adı[/]");
                table.AddColumn("[green]Müəllim[/]");
                table.AddColumn("[magenta]Otaq Sayı[/]");

                foreach (var group in groups)
                {
                    table.AddRow(group.Id.ToString(), group.Name, group.Teacher, group.RoomCount.ToString());
                }

                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                goto SelectCase;  
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün rəqəm daxil edin![/]");
            goto SelectCase;
        }
    }

    public void SearchByName()
    {
        searchName:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Axtarılacaq qrup adını daxil edin: ");
        string name = Console.ReadLine();

        try
        {
            var groups = _groupService.SearchByName(name);

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[cyan]Qrup Adı[/]");
            table.AddColumn("[green]Müəllim[/]");
            table.AddColumn("[magenta]Otaq Sayı[/]");

            foreach (var group in groups)
            {
                table.AddRow(group.Id.ToString(), group.Name, group.Teacher, group.RoomCount.ToString());
            }

            AnsiConsole.Write(table);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            goto searchName;  
        }
    }
}