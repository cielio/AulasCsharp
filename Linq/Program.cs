using Linq.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourcePath = @"in.txt";
            List<Employee> list = new List<Employee>();
            Console.WriteLine("Digite Numeros de funcionario para cadastrar");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.Write("Dite o Nome: ");
                string nome = Console.ReadLine();
                Console.Write("Dite o Email: ");
                string email = Console.ReadLine();
                Console.Write("Dite o Salario: ");
                double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                list.Add(new Employee(nome, email, salary));
            }

            try
            {
                using (StreamWriter streamWriter = File.AppendText(sourcePath))
                {
                    foreach (var employee in list)
                    {
                        streamWriter.WriteLine($"{employee.Name},{employee.Email},{employee.Salary}");
                    }
                }

                using StreamReader streamReader = File.OpenText(sourcePath);
                list.Clear();

                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(",");
                    string name = fields[0];
                    string email = fields[1];
                    double salary = double.Parse(fields[2], CultureInfo.InvariantCulture);

                    list.Add(new Employee(name, email, salary));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Erro ao tentar escrever no arquivo" + sourcePath + e);
            }


            //var avg = list.Select(p => p.Salary).DefaultIfEmpty(0.0).Average();
            var result = list.Where(p => p.Email.StartsWith("m")).Sum(p => p.Salary);

            var names = list.OrderBy(p => p.Email);
            foreach (var employe in names)
            {
                Console.WriteLine(employe.Email);
            }
            Console.WriteLine("A soma dos saraios é: " + result.ToString("F2",
                CultureInfo.InvariantCulture));
        }
    }
}
