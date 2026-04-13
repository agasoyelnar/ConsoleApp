using CourseSystem.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;
using Spectre.Console;

namespace CourseSystem.Controllers;

public class StudentController
{
    private static StudentService _studentService = new();
    private static GroupService _groupService = new();
    public StudentController(GroupService groupService)
    {
        _groupService = groupService;
    }

    public void Create()
    {
    studentName:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Tələbənin adını daxil edin: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            AnsiConsole.MarkupLine("[red]Ad boş ola bilməz![/]");
            goto studentName;
        }
        if (name.Any(char.IsDigit))
        {
            AnsiConsole.MarkupLine("[red]Ad rəqəm ehtiva edə bilməz![/]");
            goto studentName;
        }

    studentSurname:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Tələbənin soyadını daxil edin: ");
        string surname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(surname))
        {
            AnsiConsole.MarkupLine("[red]Soyad boş ola bilməz![/]");
            goto studentSurname;
        }
        if (surname.Any(char.IsDigit))
        {
            AnsiConsole.MarkupLine("[red]Soyad rəqəm ehtiva edə bilməz![/]");
            goto studentSurname;
        }

    SelectAge:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Tələbənin yaşını daxil edin: ");
        string ageStr = Console.ReadLine();
        int age;
        bool isAge = int.TryParse(ageStr, out age);
        if (!isAge)
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün yaş daxil edin![/]");
            goto SelectAge;
        }
        if (age <= 17 || age >= 100)
        {
            AnsiConsole.MarkupLine("[red]Yaş 18-dən yuxarı olmalıdır![/]");
            goto SelectAge;
        }

        var allGroups = _groupService.GetAll();
        if (allGroups.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Sistemdə heç bir qrup yoxdur! Əvvəlcə qrup yaradın.[/]");
            return;
        }

        var groupChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[blue]Qrupu seçin:[/]")
                .AddChoices(allGroups.Select(g => g.Name).ToList()));

        var selectedGroup = allGroups.FirstOrDefault(g => g.Name == groupChoice);

        try
        {
            Student student = new Student { Name = name, Surname = surname, Age = age, Group = selectedGroup };
            var result = _studentService.Create(student);

            AnsiConsole.Status()
                .Start("Tələbə bazaya əlavə edilir...", ctx =>
                {
                    ctx.Spinner(Spinner.Known.Dots);
                    Thread.Sleep(1500);
                });

            AnsiConsole.MarkupLine($"[green]Tələbə uğurla yaradıldı! Id: {result.Id}, Ad: {result.Name}, Soyad: {result.Surname}, Yaş: {result.Age}, Qrup: {result.Group.Name}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
    }

    public void GetById()
    {
        studentId:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Tələbənin ID-sini daxil edin: ");
        string studentId = Console.ReadLine();
        int id;
        bool isId = int.TryParse(studentId, out id);
        if (isId)
        {
            try
            {
                Student student = _studentService.GetById(id);

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("[yellow]ID[/]");
                table.AddColumn("[cyan]Ad[/]");
                table.AddColumn("[cyan]Soyad[/]");
                table.AddColumn("[green]Yaş[/]");
                table.AddColumn("[magenta]Qrup[/]");

                table.AddRow(
                    student.Id.ToString(),
                    student.Name,
                    student.Surname,
                    student.Age.ToString(),
                    student.Group.Name
                );

                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                goto studentId; 
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün ID daxil edin![/]");
            goto studentId;
        }
    }

    public void Delete()
    {
        Helper.PrintConsole(ConsoleColor.Blue, text: "Silinəcək tələbənin ID-sini daxil edin: ");
        string studentId = Console.ReadLine();
        int id;
        bool isId = int.TryParse(studentId, out id);
        if (isId)
        {
            try
            {
                _studentService.Delete(id);
                AnsiConsole.Status()
                    .Start("Tələbə silinir...", ctx =>
                    {
                        ctx.Spinner(Spinner.Known.Dots);
                        Thread.Sleep(1000);
                    });
                AnsiConsole.MarkupLine("[green]Tələbə uğurla silindi![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün ID daxil edin![/]");
        }
    }

    public void Update()
    {
    updateId:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Yenilənəcək tələbənin ID-sini daxil edin: ");
        string studentId = Console.ReadLine();
        int id;
        bool isId = int.TryParse(studentId, out id);
        if (!isId)
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün ID daxil edin![/]");
            goto updateId;
        }

    studentName:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Tələbənin adını daxil edin: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            AnsiConsole.MarkupLine("[red]Ad boş ola bilməz![/]");
            goto studentName;
        }
        if (name.Any(char.IsDigit))
        {
            AnsiConsole.MarkupLine("[red]Ad rəqəm ehtiva edə bilməz![/]");
            goto studentName;
        }

    studentSurname:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Tələbənin soyadını daxil edin: ");
        string surname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(surname))
        {
            AnsiConsole.MarkupLine("[red]Soyad boş ola bilməz![/]");
            goto studentSurname;
        }
        if (surname.Any(char.IsDigit))
        {
            AnsiConsole.MarkupLine("[red]Soyad rəqəm ehtiva edə bilməz![/]");
            goto studentSurname;
        }

    SelectAge:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Yeni yaşı daxil edin: ");
        string ageStr = Console.ReadLine();
        int age;
        bool isAge = int.TryParse(ageStr, out age);
        if (!isAge)
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün yaş daxil edin![/]");
            goto SelectAge;
        }

        GroupService groupService = new();
        var allGroups = groupService.GetAll();
        if (allGroups.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Sistemdə heç bir qrup yoxdur![/]");
            return;
        }

        var groupChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[blue]Qrupu seçin:[/]")
                .AddChoices(allGroups.Select(g => g.Name).ToList()));

        var selectedGroup = allGroups.FirstOrDefault(g => g.Name == groupChoice);

        try
        {
            Student student = new Student { Name = name, Surname = surname, Age = age, Group = selectedGroup };
            var result = _studentService.Update(id, student);

            AnsiConsole.Status()
                .Start("Tələbə yenilənir...", ctx =>
                {
                    ctx.Spinner(Spinner.Known.Dots);
                    Thread.Sleep(1000);
                });

            AnsiConsole.MarkupLine($"[green]Tələbə uğurla yeniləndi! Id: {result.Id}, Ad: {result.Name}, Soyad: {result.Surname}, Yaş: {result.Age}, Qrup: {result.Group.Name}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            goto updateId;
        }
    }

    public void GetAllByAge()
    {
        SelectAge:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Yaşı daxil edin: ");
        string ageStr = Console.ReadLine();
        int age;
        bool isAge = int.TryParse(ageStr, out age);
        if (isAge)
        {
            try
            {
                var students = _studentService.GetAllByAge(age);

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("[yellow]ID[/]");
                table.AddColumn("[cyan]Ad[/]");
                table.AddColumn("[cyan]Soyad[/]");
                table.AddColumn("[green]Yaş[/]");
                table.AddColumn("[magenta]Qrup[/]");

                foreach (var student in students)
                {
                    table.AddRow(
                        student.Id.ToString(),
                        student.Name,
                        student.Surname,
                        student.Age.ToString(),
                        student.Group.Name
                    );
                }

                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                goto SelectAge;  
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün yaş daxil edin![/]");
            goto SelectAge;
        }
    }

    public void GetAllByGroupId()
    {
        GroupService groupService = new();
        var allGroups = groupService.GetAll();
        if (allGroups.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Sistemdə heç bir qrup yoxdur![/]");
            return;
        }

        var groupTable = new Table().Border(TableBorder.Rounded);
        groupTable.AddColumn("[yellow]ID[/]");
        groupTable.AddColumn("[cyan]Qrup Adı[/]");
        groupTable.AddColumn("[green]Müəllim[/]");
        foreach (var g in allGroups)
        {
            groupTable.AddRow(g.Id.ToString(), g.Name, g.Teacher);
        }
        AnsiConsole.Write(groupTable);

        SelectGroupId:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Qrupun ID-sini daxil edin: ");
        string groupIdStr = Console.ReadLine();
        int groupId;
        bool isGroupId = int.TryParse(groupIdStr, out groupId);
        if (isGroupId)
        {
            try
            {
                var students = _studentService.GetAllByGroupId(groupId);

                var table = new Table().Border(TableBorder.Rounded);
                table.AddColumn("[yellow]ID[/]");
                table.AddColumn("[cyan]Ad[/]");
                table.AddColumn("[cyan]Soyad[/]");
                table.AddColumn("[green]Yaş[/]");
                table.AddColumn("[magenta]Qrup[/]");

                foreach (var student in students)
                {
                    table.AddRow(
                        student.Id.ToString(),
                        student.Name,
                        student.Surname,
                        student.Age.ToString(),
                        student.Group.Name
                    );
                }

                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
                goto SelectGroupId;  
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Zəhmət olmasa düzgün ID daxil edin![/]");
            goto SelectGroupId;
        }
    }

    public void SearchByNameOrSurname()
    {
        searchName:
        Helper.PrintConsole(ConsoleColor.Blue, text: "Ad və ya soyadı daxil edin: ");
        string search = Console.ReadLine();

        try
        {
            var students = _studentService.SearchByNameOrSurname(search);

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[cyan]Ad[/]");
            table.AddColumn("[cyan]Soyad[/]");
            table.AddColumn("[green]Yaş[/]");
            table.AddColumn("[magenta]Qrup[/]");

            foreach (var student in students)
            {
                table.AddRow(
                    student.Id.ToString(),
                    student.Name,
                    student.Surname,
                    student.Age.ToString(),
                    student.Group.Name
                );
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