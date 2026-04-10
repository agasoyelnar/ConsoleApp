using System;
using CourseSystem.Controllers;
using CourseSystem.Helpers;
using DomainLayer.Entities;
using ServiceLayer.Services.Implementations;

namespace CourseSystem
{
    class Program
    {
        static void Main(string[] args)
        { 
            GroupController groupController=new ();
            Helper.PrintConsole(ConsoleColor.Blue,"Asagidakilardan birini secin: ");
            Helper.PrintConsole(ConsoleColor.Green, text: "1. Qrup yarat,\n2. ID ilə qrup axtar,\n3. Bütün qrupları göstər,\n4. Qrupu sil,\n5. Qrupu yenilə,\n6. Müəllimə görə qrupları göstər,\n7. Otaq sayına görə qrupları göstər");            while (true)
            {
                SelectOption:string selectOption = Console.ReadLine();
                int selectNumber;
                bool isselectOption=int.TryParse(selectOption, out selectNumber);
                if (isselectOption)
                {
                    switch (selectNumber)
                    {
                        case 1:
                            groupController.Create();
                            goto SelectOption;
                        case 2:
                            groupController.GetById();
                            goto SelectOption;
                        case 3:
                            groupController.GetAll();
                            goto SelectOption;
                        case 4:
                            groupController.Delete();
                            goto SelectOption;
                        case 5:
                            groupController.Update();
                            goto SelectOption;
                        case 6:
                            groupController.GetAllByTeacher();   
                            goto SelectOption;
                        case 7:
                            groupController.GetAllByRoom();
                            goto SelectOption;
                                
                    }
                }
                else
                {
                    Helper.PrintConsole(ConsoleColor.Red, "Please enter a option type: ");
                    goto SelectOption;
                }
            }
        }
    }
};

