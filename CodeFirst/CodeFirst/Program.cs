using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CodeFirst.DataTables;
using CodeFirst.TableConfiguration;

namespace CodeFirst
{
    public class CodeFirst : DbContext
    {
        public CodeFirst() : base("Hospital") { Database.SetInitializer<CodeFirst>(null); }

        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Proced> Proced { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProcedConfig());
        }
    }
    public class Program
    {
        public static List<object> AlterDataTables(string tableName,string op, List<Equipment> eq, List<Medicine> med, List<Staff> st, List<Patient> pat, List<Proced> proc)
        {
            using (var dbContext = new CodeFirst())
            {
                switch (tableName)
                {
                    case "dbo.Equipment":
                        if (op == "add") // Визначаємо операцію
                        {
                            Console.WriteLine("Введіть Назву, К-сть та виробника");

                            var temp = new Equipment();
                            try
                            {
                                temp.IDeq = dbContext.Equipment.Count() + 1;
                                temp.Name = Console.ReadLine();
                                temp.Quantity = Convert.ToInt32(Console.ReadLine());
                                temp.Manufacturer = Console.ReadLine();


                                dbContext.Equipment.Add(temp);

                                dbContext.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Помилка, {ex}, дані не пройшли валідацію");
                            }
                        }
                        if (op == "alter")
                        {
                            Console.WriteLine("Виберіть рядок, що будете редагувати: ");
                            int index = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введіть Назву, К-сть та виробника");

                            var temp = new Equipment();
                            try
                            {
                                temp.Name = Console.ReadLine();
                                temp.Quantity = Convert.ToInt32(Console.ReadLine());
                                temp.Manufacturer = Console.ReadLine();

                                var pick = dbContext.Equipment.FirstOrDefault(eq => eq.IDeq == index);

                                if (pick != null)
                                {
                                    pick.Name = temp.Name;
                                    pick.Quantity = temp.Quantity;
                                    pick.Manufacturer = temp.Manufacturer;

                                    dbContext.SaveChanges();
                                }
                                else
                                {
                                    Console.WriteLine("Такого рядка не існує!");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Помилка, {ex}, дані не пройшли валідацію");
                            }
                        }
                        if (op == "delete")
                        {
                            Console.WriteLine("Виберіть рядок, що будете видаляти: ");
                            int index = Convert.ToInt32(Console.ReadLine());

                            var pick = dbContext.Equipment.FirstOrDefault(eq => eq.IDeq == index);

                            if (pick != null)
                            {
                                var procedRecordsToDelete = dbContext.Proced.Where(p => p.Equipment_ID == index);
                                dbContext.Proced.RemoveRange(procedRecordsToDelete);

                                eq.Remove(pick);
                                dbContext.Equipment.Remove(pick);
                                

                                dbContext.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Такого рядка не існує!");
                            }
                        }
                        eq = dbContext.Equipment.ToList();
                        return dbContext.Equipment.ToList<object>();
                        break;
                    case "dbo.Medicine":
                        if (op == "add") // Визначаємо операцію
                        {
                            Console.WriteLine("Введіть Назву, Виробника та дату Створення/Термін придатності: ");

                            var temp = new Medicine();
                            try
                            {
                                temp.Name = Console.ReadLine();
                                temp.Producer = Console.ReadLine();

                                string dateInput = Console.ReadLine();

                                string date2Input = Console.ReadLine();

                                if (DateOnly.TryParse(dateInput, out DateOnly createdDate) && DateOnly.TryParse(date2Input, out DateOnly expDate))
                                {
                                    temp.Created_Date = createdDate;
                                    temp.Expiration_Date = expDate;
                                }
                                else
                                {
                                    Console.WriteLine("Дата не пройшла валідацію, використовуйте рік-місяць-день");
                                }

                                dbContext.Medicine.Add(temp);

                                dbContext.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Помилка, {ex}, дані не пройшли валідацію");
                            }
                        }
                        if (op == "alter")
                        {
                            Console.WriteLine("Введіть рядок що буде змінено");
                            int index = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введіть Назву, Виробника та дату Створення/Термін придатності: ");

                            var pick = dbContext.Medicine.FirstOrDefault(med => med.IDmed == index);
                            if (pick != null)
                            {
                                try
                                {
                                    pick.Name = Console.ReadLine();
                                    pick.Producer = Console.ReadLine();

                                    string dateInput = Console.ReadLine();

                                    string date2Input = Console.ReadLine();

                                    if (DateOnly.TryParse(dateInput, out DateOnly createdDate) && DateOnly.TryParse(date2Input, out DateOnly expDate))
                                    {
                                        pick.Created_Date = createdDate;
                                        pick.Expiration_Date = expDate;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Дата не пройшла валідацію, використовуйте рік-місяць-день");
                                    }

                                    dbContext.SaveChanges();
                                }
                                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                            }
                        }
                        if (op == "delete")
                        {
                            Console.WriteLine("Виберіть рядок, що будете видаляти: ");

                            int index = Convert.ToInt32(Console.ReadLine());

                            var pick = med.FirstOrDefault(med => med.IDmed == index);

                            if (pick != null)
                            {
                                var procedRecordsToDelete = dbContext.Proced.Where(p => p.Medicine_ID == index);

                                dbContext.Proced.RemoveRange(procedRecordsToDelete);

                                dbContext.Medicine.Remove(pick);

                                dbContext.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Такого рядка не існує!");
                            }
                        }
                        med = dbContext.Medicine.ToList();
                        return dbContext.Medicine.ToList<object>();
                        break;
                    case "dbo.Patient":
                        if (op == "add")
                        {
                            Console.WriteLine("Введіть ПІБ, Вік, Хворобу, Дати прибуття/виписки у форматі рік-місяць-число, а також відділення ");
                            try
                            {
                                var temp = new Patient();
                                temp.Last_Name = Console.ReadLine();
                                temp.First_Name = Console.ReadLine();
                                temp.Middle_Name = Console.ReadLine();
                                temp.Age = Convert.ToInt32(Console.ReadLine());
                                temp.Disease = Console.ReadLine();
                                string Arrival_Date = Console.ReadLine();
                                string Discharge_Date = Console.ReadLine();
                                temp.Ward = Console.ReadLine();

                                if (DateOnly.TryParse(Arrival_Date, out DateOnly createdDate) && DateOnly.TryParse(Discharge_Date, out DateOnly expDate))
                                {
                                    temp.Arrival_Date = createdDate;
                                    temp.Discharge_Date = expDate;
                                }
                                else
                                {
                                    Console.WriteLine("Дата не пройшла валідацію, використовуйте рік-місяць-день");
                                }
                                dbContext.Patient.Add(temp);

                                dbContext.SaveChanges();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }

                        }
                        if (op == "alter")
                        {
                            Console.WriteLine("Введіть id рядка, що буде змінений");

                            int index = Convert.ToInt32(Console.ReadLine());
                            var temp = dbContext.Patient.FirstOrDefault(pat => pat.IDpat == index);

                            Console.WriteLine("Введіть ПІБ, Вік, Хворобу, Дати прибуття/виписки у форматі рік-місяць-число, а також відділення ");
                            if (temp != null)
                            {
                                try
                                {
                                    temp.Last_Name = Console.ReadLine();
                                    temp.First_Name = Console.ReadLine();
                                    temp.Middle_Name = Console.ReadLine();
                                    temp.Age = Convert.ToInt32(Console.ReadLine());
                                    temp.Disease = Console.ReadLine();
                                    string Arrival_Date = Console.ReadLine();
                                    string Discharge_Date = Console.ReadLine();
                                    temp.Ward = Console.ReadLine();

                                    if (DateOnly.TryParse(Arrival_Date, out DateOnly createdDate) && DateOnly.TryParse(Discharge_Date, out DateOnly expDate))
                                    {
                                        temp.Arrival_Date = createdDate;
                                        temp.Discharge_Date = expDate;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Дата не пройшла валідацію, використовуйте рік-місяць-день");
                                    }
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            else { Console.WriteLine("Такого рядка не існує!"); }
                        }
                        if (op == "delete")
                        {
                            Console.WriteLine("Введіть id рядка, що буде видалений");

                            int index = Convert.ToInt32(Console.ReadLine());

                            var temp = dbContext.Patient.FirstOrDefault(pat => pat.IDpat == index);
                            if (temp != null)
                            {
                                var procedRecordsToDelete = dbContext.Proced.Where(p => p.Patient_ID == index);
                                dbContext.Proced.RemoveRange(procedRecordsToDelete);

                                dbContext.Patient.Remove(temp);

                                dbContext.SaveChanges();
                            }
                            else { Console.WriteLine("Такого рядка не існує"); }
                        }
                        pat = dbContext.Patient.ToList();
                        return pat.ToList<object>();
                        break;
                    case "dbo.Staff":
                        if (op == "add")
                        {
                            var temp = new Staff();

                            Console.WriteLine("Введіть ПІБ, Вік і Посаду ");
                            try
                            {
                                temp.Last_Name = Console.ReadLine();
                                temp.Name = Console.ReadLine();
                                temp.Middle_Name = Console.ReadLine();
                                temp.Age = Convert.ToInt32(Console.ReadLine());
                                temp.Position = Console.ReadLine();

                                dbContext.Staff.Add(temp);

                                dbContext.SaveChanges();
                            }
                            catch (Exception ex) { Console.WriteLine("Валідація не пройдена"); }
                        }
                        if (op == "alter")
                        {

                            Console.WriteLine("Введіть id рядка, що буде змінений");
                            int index = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Введіть ПІБ, Вік і Посаду ");
                            var pick = dbContext.Staff.FirstOrDefault(st => st.IDstaff == index);
                            if (pick != null)
                            {
                                try
                                {
                                    pick.Last_Name = Console.ReadLine();
                                    pick.Name = Console.ReadLine();
                                    pick.Middle_Name = Console.ReadLine();
                                    pick.Age = Convert.ToInt32(Console.ReadLine());
                                    pick.Position = Console.ReadLine();

                                    dbContext.SaveChanges();
                                }
                                catch (Exception ex) { Console.WriteLine("Валідація не пройдена"); }
                            }
                        }
                        if (op == "delete")
                        {
                            Console.WriteLine("Введіть id рядка, що буде видалений");

                            int index = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Введіть ПІБ, Вік і Посаду ");
                            var pick = dbContext.Staff.FirstOrDefault(st => st.IDstaff == index);

                            if (pick != null)
                            {
                                var procedRecordsToDelete = dbContext.Proced.Where(p => p.Staff_ID == index);
                                dbContext.Proced.RemoveRange(procedRecordsToDelete);

                                dbContext.Staff.Remove(pick);

                                dbContext.SaveChanges();

                                st = dbContext.Staff.ToList();
                            }
                            else { Console.WriteLine("Такого рядка не існує!"); }
                        }
                        st = dbContext.Staff.ToList();
                        return st.ToList<object>();
                        break;
                    case "dbo.Proced":
                        if (op == "add")
                        {
                            Console.WriteLine("Введіть ID пацієнта, спорядження, персоналу та ліків, назву та ціну процедури: ");

                            var temp = new Proced();

                            try
                            {
                                temp.Patient_ID = Convert.ToInt32(Console.ReadLine());
                                temp.Equipment_ID = Convert.ToInt32(Console.ReadLine());
                                temp.Staff_ID = Convert.ToInt32(Console.ReadLine());
                                temp.Medicine_ID = Convert.ToInt32(Console.ReadLine());
                                temp.Name = Console.ReadLine();
                                temp.Price = Convert.ToInt32(Console.ReadLine());

                                dbContext.Proced.Add(temp);

                                dbContext.SaveChanges();
                            }
                            catch (Exception ex) { Console.WriteLine(ex.Message); }
                        }
                        if (op == "alter")
                        {
                            Console.WriteLine("Введіть id рядка, що буде змінений");

                            int index = Convert.ToInt32(Console.ReadLine());

                            var pick = dbContext.Proced.FirstOrDefault(proc => proc.IDproc == index);
                            if (pick != null)
                            {
                                try
                                {
                                    Console.WriteLine("Введіть ID пацієнта, спорядження, персоналу та ліків, назву та ціну процедури: ");

                                    pick.Patient_ID = Convert.ToInt32(Console.ReadLine());
                                    pick.Equipment_ID = Convert.ToInt32(Console.ReadLine());
                                    pick.Staff_ID = Convert.ToInt32(Console.ReadLine());
                                    pick.Medicine_ID = Convert.ToInt32(Console.ReadLine());
                                    pick.Name = Console.ReadLine();
                                    pick.Price = Convert.ToInt32(Console.ReadLine());

                                    dbContext.SaveChanges();
                                }
                                catch (Exception ex) { Console.WriteLine(ex.Message); }
                            }
                            else Console.WriteLine("Такого рядка не існує!");
                        }
                        if (op == "delete")
                        {
                            Console.WriteLine("Введіть id рядка, що буде видалений");

                            int index = Convert.ToInt32(Console.ReadLine());
                            var pick = dbContext.Proced.FirstOrDefault(proc => proc.IDproc == index);

                            if (pick != null)
                            {
                                dbContext.Proced.Remove(pick);

                                dbContext.SaveChanges();
                            }
                            else Console.WriteLine("Такого рядка не існує!");
                        }
                        proc = dbContext.Proced.ToList();
                        return proc.ToList<object>();
                        break;
                    default:
                        Console.WriteLine("Такої таблиці не існує!"); Console.Clear(); throw new ArgumentException("Invalid entity name"); break;
                }
            }
        }

        public static void Display(string tablename,List<Equipment> eq, List<Medicine> med, List<Staff> st, List<Patient> pat, List<Proced> proc)
        {
            if(tablename == "dbo.Patient")
            {
                Console.WriteLine("Таблиця Пацієнти:");
                foreach (var p in pat)
                {
                    Console.WriteLine($"ID: {p.IDpat}");
                    Console.WriteLine($"ПІБ: {p.Last_Name} {p.First_Name} {p.Middle_Name}");
                    Console.WriteLine($"Вік: {p.Age}");
                    Console.WriteLine($"Хвороба: {p.Disease}");
                    Console.WriteLine($"Відділення: {p.Ward}");
                    Console.WriteLine($"Дата прибуття: {p.Arrival_Date}");
                    Console.WriteLine($"Дата виписки: {p.Discharge_Date}");
                    Console.WriteLine();
                }
            }
            if(tablename == "dbo.Equipment")
            {
                Console.WriteLine("Таблиця Спорядження:");
                foreach(var e in eq)
                {
                    Console.WriteLine($"ID: {e.IDeq}");
                    Console.WriteLine($"Назва: {e.Name}");
                    Console.WriteLine($"Виробник: {e.Manufacturer}");
                    Console.WriteLine($"Кількість: {e.Quantity}");
                    Console.WriteLine();
                }
            }
            if(tablename == "dbo.Medicine")
            {
                Console.WriteLine("Таблиця Ліки:");
                foreach(var m in med)
                {
                    Console.WriteLine($"ID: {m.IDmed}");
                    Console.WriteLine($"Назва: {m.Name}");
                    Console.WriteLine($"Виробник: {m.Producer}");
                    Console.WriteLine($"Дата виготовлення: {m.Created_Date}");
                    Console.WriteLine($"Термін придатності: до {m.Expiration_Date}");
                    Console.WriteLine();
                }
            }
            if(tablename == "dbo.Staff")
            {
                Console.WriteLine("Таблиця Персонал:");
                foreach(var s in st)
                {
                    Console.WriteLine($"ID: {s.IDstaff}");
                    Console.WriteLine($"ПІБ: {s.Last_Name} {s.Name} {s.Middle_Name}");
                    Console.WriteLine($"Вік: {s.Age}");
                    Console.WriteLine($"Посада: {s.Position}");
                    Console.WriteLine();
                }
            }
            if(tablename =="dbo.Proc")
            {
                Console.WriteLine("Таблиця процедури:");
                foreach(var p in proc)
                {
                    Console.WriteLine($"ID: {p.IDproc}");
                    Console.WriteLine($"Пацієнт: {p.Patient.Last_Name} {p.Patient.First_Name} {p.Patient.Middle_Name}");
                    Console.WriteLine($"Тип процедури: {p.Name}");
                    Console.WriteLine($"Спорядження для процедури: {p.Equipment.Name}");
                    Console.WriteLine($"Ліки для процедури: {p.Medicine.Name}");
                    Console.WriteLine($"Процедуру проводив: {p.Staff.Last_Name} {p.Staff.Name} {p.Staff.Middle_Name}");
                    Console.WriteLine($"Ціна: {p.Price}");
                    Console.WriteLine();
                }
            }
        }
        public static List<object> Menu_operations(string tableName, List<Equipment> eq, List<Medicine> med, List<Staff> st, List<Patient> pat, List<Proced> proc) // Меню операцій
        {
            Console.WriteLine("Виберіть наступну операцію:\n1)Додати дані до таблиці\n2)Видалити дані з таблиці\n3)Редагувати стрічку таблиці\n4)Повернутись");

            string a = Console.ReadLine();
            List<object> obj = new List<object>();
            switch (a)
            {
                case "1":
                    obj = AlterDataTables(tableName, "add", eq, med, st, pat, proc); // таблиця і "вид" операції
                    return obj;
                    break;

                case "2":
                    obj = AlterDataTables(tableName, "delete", eq, med, st, pat, proc);
                    return obj;
                    break;

                case "3":
                    obj = AlterDataTables(tableName, "alter", eq, med, st, pat, proc);
                    return obj;
                    break;

                case "4":
                    Menu_pickTables(eq, med, st, pat, proc);
                    throw new ArgumentException("Неправильний ввід");
                    break;

                default:
                    Console.Clear();
                    throw new ArgumentException("Немає такої операції!");
                    break;
            }
            pat = obj.OfType<Patient>().ToList();
            eq = obj.OfType<Equipment>().ToList();
            st = obj.OfType<Staff>().ToList();
            med = obj.OfType<Medicine>().ToList();
            proc = obj.OfType<Proced>().ToList();
            return obj;
        }
        public static List<object> Menu_pickTables(List<Equipment> eq, List<Medicine> med, List<Staff> st, List<Patient> pat, List<Proced> proc) // Меню вибору таблиць
        {
            Console.WriteLine("Введіть назву таблиці\n1)dbo.Equipment \n2)dbo.Medicine \n3)dbo.Staff \n4)dbo.Patient \n5)dbo.Proced\n6)Повернутись");

            string tableName = Console.ReadLine();
            List<object> obj = new List<object>();
            switch (tableName)
            {
                case "1":
                    tableName = "dbo.Equipment";
                    Display(tableName, eq, med, st, pat, proc);
                    obj = Menu_operations(tableName, eq, med, st, pat, proc);
                    return obj;
                    break;
                case "2":
                    tableName = "dbo.Medicine";
                    Display(tableName, eq, med, st, pat, proc);
                    obj = Menu_operations(tableName, eq, med, st, pat, proc);
                    return obj;
                    break;
                case "3":
                    tableName = "dbo.Staff";
                    Display(tableName, eq, med, st, pat, proc);
                    obj = Menu_operations(tableName, eq, med, st, pat, proc);
                    return obj; 
                    break;
                case "4":
                    tableName = "dbo.Patient";
                    Display(tableName, eq, med, st, pat, proc);
                    obj = Menu_operations(tableName, eq, med, st, pat, proc);
                    return obj;
                    break;
                case "5":
                    tableName = "dbo.Proced";
                    Display(tableName, eq, med, st, pat, proc);
                    obj = Menu_operations(tableName, eq, med, st, pat, proc);
                    return obj;
                    break;
                case "6":
                    Console.Clear();
                    Menu_main(eq, med, st, pat, proc);
                    return obj;
                    break;
                default:
                    Console.WriteLine("No such choice");
                    Console.Clear();
                    break;
            }
            return obj;
        }
        public static void Menu_main(List<Equipment> eq, List<Medicine> med, List<Staff> st, List<Patient> pat, List<Proced> proc) // Головне меню
        {
            bool men = true;
            while (men = true)
            {
                Console.WriteLine("1)Робота з таблицями\n2)Вихід");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Menu_pickTables(eq, med, st, pat, proc);
                        break;

                    case "2":
                        men = false;
                        return;
                        break;

                    default:
                        Console.WriteLine("No such choice");
                        men = false;
                        break;
                }
            }
        }
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            using (var dbContext = new CodeFirst())
            {
                var pat = dbContext.Patient.ToList();
                var eq = dbContext.Equipment.ToList();
                var med = dbContext.Medicine.ToList();
                var st = dbContext.Staff.ToList();
                var proc = dbContext.Proced.ToList();
                Menu_main(eq,med,st,pat,proc);
            }
        }
    }
}
