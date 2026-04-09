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
            Helper.PrintConsole(ConsoleColor.Blue,"select 1 option: ");
            Helper.PrintConsole(ConsoleColor.Green,"1. Create group,\n2. Get Group,\n3.GetAll Group,\n4.Delete Group,\n5.Update Group\n ");
            while (true)
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

