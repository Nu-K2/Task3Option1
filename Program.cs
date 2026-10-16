using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3Option1;

// === Точка входа ===
class Program
{
    static void Main(string[] args)
    {
        Menu menu = new Menu();
        menu.Run();
    }
}

// === Меню ===
class Menu
{
    private readonly List<PostAddress> addresses = new();

    public void Run()
    {
        while (true)
        {
            PrintMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateFilledAddress(); break;
                case "2": ChangeCountry(); break;
                case "3": ChangeCity(); break;
                case "4": ChangeStreet(); break;
                case "5": ChangeBuilding(); break;
                case "6": ChangeIndex(); break;
                case "7": ShowAddresses(); break;
                case "8": DestroyAddress(); break;
                case "0":
                    Console.WriteLine("Сеанс окончен.\n");
                    return;
                default:
                    Console.WriteLine("Ошибка ввода. Выберите пункт из меню.\n");
                    break;
            }
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine("===== МЕНЮ =====");
        Console.WriteLine("1 - Создать адрес");
        Console.WriteLine("2 - Изменить страну");
        Console.WriteLine("3 - Изменить город");
        Console.WriteLine("4 - Изменить улицу");
        Console.WriteLine("5 - Изменить дом");
        Console.WriteLine("6 - Изменить индекс");
        Console.WriteLine("7 - Показать адреса");
        Console.WriteLine("8 - Удалить адрес");
        Console.WriteLine("0 - Выход");
        Console.Write("Ваш выбор: ");
    }

    // --- Создание ---

    private void CreateFilledAddress()
    {
        PostAddress newAddress = new PostAddress(
            ReadLettersOnly("Введите страну: "),
            ReadLettersOnly("Введите город: "),
            ReadLettersOnly("Введите улицу: "),
            ReadNonEmpty("Введите дом: "),
            ReadDigitsOnly("Введите индекс: ")
        );

        addresses.Add(newAddress);
        Console.WriteLine($"Адрес создан. Присвоен id = {newAddress.Id}.\n");
    }

    // --- Изменение ---

    private void ChangeCountry()
    {
        PostAddress address = SelectAddress();
        if (address == null) return;

        address.ChangeCountry(ReadLettersOnly("Введите новую страну: "));
        Console.WriteLine("Страна изменена.\n");
    }

    private void ChangeCity()
    {
        PostAddress address = SelectAddress();
        if (address == null) return;

        address.ChangeCity(ReadLettersOnly("Введите новый город: "));
        Console.WriteLine("Город изменён.\n");
    }

    private void ChangeStreet()
    {
        PostAddress address = SelectAddress();
        if (address == null) return;

        address.ChangeStreet(ReadLettersOnly("Введите новую улицу: "));
        Console.WriteLine("Улица изменена.\n");
    }

    private void ChangeBuilding()
    {
        PostAddress address = SelectAddress();
        if (address == null) return;

        address.ChangeBuilding(ReadNonEmpty("Введите новый дом: "));
        Console.WriteLine("Дом изменён.\n");
    }

    private void ChangeIndex()
    {
        PostAddress address = SelectAddress();
        if (address == null) return;

        address.ChangeIndex(ReadDigitsOnly("Введите новый индекс: "));
        Console.WriteLine("Индекс изменён.\n");
    }

    // --- Просмотр ---
    private void ShowAddresses()
    {
        if (addresses.Count == 0)
        {
            Console.WriteLine("Список адресов пуст.\n");
            return;
        }

        foreach (PostAddress item in addresses)
            item.PrintAddress();
    }

    // --- Удаление ---
    private void DestroyAddress()
    {
        PostAddress address = SelectAddress();
        if (address == null) return;

        addresses.Remove(address);
        Console.WriteLine("Адрес успешно удалён.\n");
    }

    // --- Вспомогательные ---
    private PostAddress SelectAddress()
    {
        if (addresses.Count == 0)
        {
            Console.WriteLine("Список адресов пуст.\n");
            return null;
        }

        while (true)
        {
            Console.WriteLine("Введите id адреса (или Enter для отмены):");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return null;

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("Некорректный ввод. Введите целое число.");
                continue;
            }

            PostAddress address = addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
            {
                Console.WriteLine($"Адрес с id={id} не найден. Попробуйте снова.");
                continue;
            }

            return address;
        }
    }


    private string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine().Trim();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("Ввод не должен быть пустым. Повторите.");
        }
    }

    private string ReadLettersOnly(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ввод не должен быть пустым. Повторите.");
                continue;
            }

            if (!input.All(char.IsLetter))
            {
                Console.WriteLine("Ввод должен содержать только буквы. Повторите.");
                continue;
            }

            return input.Trim();
        }
    }

    private string ReadDigitsOnly(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ввод не должен быть пустым. Повторите.");
                continue;
            }

            if (!input.All(char.IsDigit))
            {
                Console.WriteLine("Ввод должен содержать только цифры. Повторите.");
                continue;
            }

            return input.Trim();
        }
    }
}

class PostAddress
{
    private static int _idCounter = 0;

    public int Id { get; private set; }
    public string Country { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string Building { get; private set; } = string.Empty;
    public string Index { get; private set; } = string.Empty;

    public PostAddress(string country, string city, string street, string building, string index)
    {
        _idCounter++;
        Id = _idCounter;
        Country = country;
        City = city;
        Street = street;
        Building = building;
        Index = index;
    }


    public void ChangeCountry(string value) => Country = value;
    public void ChangeCity(string value) => City = value;
    public void ChangeStreet(string value) => Street = value;
    public void ChangeBuilding(string value) => Building = value;
    public void ChangeIndex(string value) => Index = value;


    public void PrintAddress()
    {
        Console.WriteLine("\n--- Почтовый адрес ---");
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Страна:  {Country}");
        Console.WriteLine($"Город:   {City}");
        Console.WriteLine($"Улица:   {Street}");
        Console.WriteLine($"Дом:     {Building}");
        Console.WriteLine($"Индекс:  {Index}");
        Console.WriteLine("---------------------\n");
    }
}