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



            //
            // Helper.PrintConsole(ConsoleColor.Blue, "Zəhmət olmasa aşağıdakılardan birini seçin: ");
            // Helper.PrintConsole(ConsoleColor.Green,
            //     "1. Qrup yarat\n2. ID ilə qrup axtar\n3. Bütün qrupları göstər\n4. Qrupu sil\n5. Qrupu yenilə\n6. Müəllimə görə qrupları göstər\n7. Otaq sayına görə qrupları göstər");
            // Console.WriteLine("-----------------------------------------------------------");
            
            
            GroupController groupController = new();

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
                            "2. Axtarış və Filtr (Get)",
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
                else if (mainChoice == "2. Axtarış və Filtr (Get)")
                {
                    var getChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[green]Hansı növ axtarış etmək istəyirsiniz?[/]")
                            .AddChoices(new[]
                            {
                                "ID-yə görə",
                                "Müəllimə görə",
                                "Otaq sayına görə",
                                "Group adina gore axtar",
                                "Hamısını göstər",
                                "Geri"
                            }));

                    if (getChoice == "Geri") continue;

                    switch (getChoice)
                    {
                        case "ID-yə görə": groupController.GetById(); break;
                        case "Müəllimə görə": groupController.GetAllByTeacher(); break;
                        case "Otaq sayına görə": groupController.GetAllByRoom(); break;
                        case "Group adina gore axtar": groupController.SearchByName(); break;
                        case "Hamısını göstər": groupController.GetAll(); break;
                    }
                }

                AnsiConsole.MarkupLine("\n[grey]Davam etmək üçün hər hansı düyməyə basın...[/]");
                Console.ReadKey(true);
            }
        }
    }
}

