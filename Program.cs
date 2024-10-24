using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Practic_Work_6._6
{
    internal class Program
    {
        const string filePath = @"c:\workers.txt";
        static void Main(string[] args)
        {
            StartApplication();
        }
        static void StartApplication()
        {
            Console.Write("Здравствуйте! Вы находитесь в управлении базой данных сотрудников компании ООО 'Разработка'\n" +
                "Нажмите 1 - Вывод данных сотрудников на экран\n" +
                "Нажмите 2 - Заполнить данные и добавить новую запись сотрудника\n" +
                "Сделайте выбор: ");
            try
            {
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        ReadData();
                        break;
                    case 2:
                        Console.Write("Введите идентификатор сотрудника: ");
                        int Id = int.Parse(Console.ReadLine());
                        Console.Write("Введите ФИО сотрудника: ");
                        string FullName = Console.ReadLine();
                        Console.Write("Введите рост сотрудника: ");
                        int Height = int.Parse(Console.ReadLine());
                        var DateOfBirth = inputDoB();
                        Console.Write("Введите место рождения сотрудника: ");
                        string PlaceOfBirth = Console.ReadLine();
                        AddData(Id, FullName, Height, DateOfBirth, PlaceOfBirth);
                        break;
                    default:
                        Console.WriteLine("Выбор неверный! Перезапустите программу");
                        Console.ReadKey();
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message + " Введите любую клавишу для продолжения...");
                StartApplication();
            }
        }
        static void ReadData()
        {
            if (File.Exists(filePath))
            {
                string workers = File.ReadAllText(filePath);
                string[] worker = workers.Split('#');
                foreach(var id in worker)
                {
                    Console.WriteLine(id);
                }

                Console.WriteLine("Введите любую клавишу для продолжения...");
                Console.ReadKey();
                StartApplication();
            }
            else
            {
                Console.WriteLine("Введите любую клавишу для продолжения...");
                Console.ReadKey();
                StartApplication();
            }
        }
        static void AddData(int Id, string name, int height, DateTime dateOfBirth, string placeOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            string splitter = "#";

            StringBuilder sb = new StringBuilder(Id + splitter + DateTime.Now + splitter + name + splitter + age + splitter + height + splitter +
                dateOfBirth.ToString("dd.MM.yyyy") + splitter + placeOfBirth);
            sb.Append("\n");
            File.AppendAllText(filePath, sb.ToString());
            Console.WriteLine("Запись сотрудника добавлена. Введите любую клавишу для продолжения...");
            Console.ReadKey();
            StartApplication();
        }
        static DateTime inputDoB()
        {
            DateTime dob;
            string input;
            do
            {
                Console.WriteLine("Введите дату рождения сотрудника (Формат ДД.ММ.ГГГГ): ");
                input = Console.ReadLine();
            }
            while (!DateTime.TryParseExact(input, "dd.MM.yyyy", null, DateTimeStyles.None, out dob));
            return dob;
        }
    }
}
