using System;
using System.Collections.Generic;
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
            Console.Write("Здравствуйте! Вы находитесь в управлении базой данных сотрудников компании ООО 'Разработка'\n" +
                "Нажмите 1 - Вывод данных сотрудников на экран\n" +
                "Нажмите 2 - Заполнить данные и добавить новую запись сотрудника\n" +
                "Сделайте выбор: ");
            try
            {
                int choose = int.Parse(Console.ReadLine());
                switch(choose)
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
                        Console.Write("Введите дату рождения сотрудника (Формат ДД.ММ.ГГГГ): ");
                        string DateOfBirth = Console.ReadLine();
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
            catch(FormatException ex) 
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
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

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("База данных с сотрудниками компании ещё не создана!");
                Console.ReadKey();
            }
        }
        static void AddData(int Id, string name, int height, string dateOfBirth, string placeOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - DateTime.Parse(dateOfBirth).Year;
            if (DateTime.Parse(dateOfBirth).Date > today.AddYears(-age)) age--;

            File.AppendAllText(filePath, Id + "#" + 
                DateTime.Now + "#" + 
                name + "#" + 
                age + "#" +
                height + "#" +
                dateOfBirth + "#" +
                placeOfBirth + "\n");
            Console.WriteLine("Запись сотрудника добавлена");
            Console.ReadKey();
        }
    }
}
