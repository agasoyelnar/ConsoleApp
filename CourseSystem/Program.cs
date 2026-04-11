using System;
using CourseSystem.Controllers;
using CourseSystem.Helpers;
using Spectre.Console;

namespace CourseSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Helper.PrintConsole(ConsoleColor.Cyan, @"
  ____ ___  _   _ ____  ____  _____      _    ____  ____  
 / ___/ _ \| | | |  _ \/ ___|| ____|    / \  |  _ \|  _ \ 

| |  | | | | | | | |_) \___ \|  _|     / _ \ | |_) | |_) |
| |__| |_| | |_| |  _ < ___) | |___   / ___ \|  __/|  __/ 
 \____\___/ \___/|_| \_\____/|_____| /_/   \_\_|   |_|    
");

            GroupController groupController = new();
            StudentController studentController = new();

            while (true)
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText("COURSE APP").Centered().Color(Color.Cyan1));
                Helper.PrintConsole(ConsoleColor.DarkGray, "===========================================================");
                Helper.PrintConsole(ConsoleColor.Yellow, "       TƏLƏBƏ VƏ QRUP İDARƏETMƏ SİSTEMİ ");
                Helper.PrintConsole(ConsoleColor.DarkGray, "===========================================================");
                Console.WriteLine();

                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Əsas menyu:[/]")
                        .AddChoices(new[]
                        {
                            "1. Qrup Əməliyyatları",
                            "2. Qrup Axtarış və Filtr (Get)",
                            "3. Tələbə Əməliyyatları",
                            "4. Tələbə Axtarış və Filtr (Get)",
                            "Çıxış"
                        }));

                if (mainChoice == "Çıxış") break;

                if (mainChoice == "1. Qrup Əməliyyatları")
                {
                    var groupChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Qrup əməliyyatını seçin:[/]")
                            .AddChoices(new[] { "Qrup yarat", "Qrupu sil", "Qrupu yenilə", "Geri" }));

                    if (groupChoice == "Geri") continue;

                    switch (groupChoice)
                    {
                        case "Qrup yarat": groupController.Create(); break;
                        case "Qrupu sil": groupController.Delete(); break;
                        case "Qrupu yenilə": groupController.Update(); break;
                    }
                }
                else if (mainChoice == "2. Qrup Axtarış və Filtr (Get)")
                {
                    var getChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]Hansı növ axtarış etmək istəyirsiniz?[/]")
                            .AddChoices(new[]
                            {
                                "ID-yə görə",
                                "Müəllimə görə",
                                "Otaq sayına görə",
                                "Ada görə qrupu axtar",
                                "Hamısını göstər",
                                "Geri"
                            }));

                    if (getChoice == "Geri") continue;

                    switch (getChoice)
                    {
                        case "ID-yə görə": groupController.GetById(); break;
                        case "Müəllimə görə": groupController.GetAllByTeacher(); break;
                        case "Otaq sayına görə": groupController.GetAllByRoom(); break;
                        case "Ada görə qrupu axtar": groupController.SearchByName(); break;  // ← düzəldildi
                        case "Hamısını göstər": groupController.GetAll(); break;
                    }
                }
                else if (mainChoice == "3. Tələbə Əməliyyatları")
                {
                    var studentChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[yellow]Tələbə əməliyyatını seçin:[/]")
                            .AddChoices(new[] 
                                { 
                                    "Tələbə yarat", 
                                    "Tələbəni sil", 
                                    "Tələbəni yenilə",
                                    "Geri" }));

                    if (studentChoice == "Geri") continue;

                    switch (studentChoice)
                    {
                        case "Tələbə yarat": studentController.Create(); break;
                        case "Tələbəni sil": studentController.Delete(); break;  
                        case "Tələbəni yenilə": studentController.Update(); break;
                    }
                }
                else if (mainChoice == "4. Tələbə Axtarış və Filtr (Get)")
                {
                    var getChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]Hansı növ axtarış etmək istəyirsiniz?[/]")
                            .AddChoices(new[]
                            {
                                "ID-yə görə",
                                "Yaşa görə",
                                "Geri"
                            }));

                    if (getChoice == "Geri") continue;

                    switch (getChoice)
                    {
                        case "ID-yə görə": studentController.GetById(); break; 
                        case "Yaşa görə": studentController.GetAllByAge(); break;
                    }
                }

                AnsiConsole.MarkupLine("\n[grey]Davam etmək üçün hər hansı düyməyə basın...[/]");
                Console.ReadKey(true);
            }
        }
    }
}