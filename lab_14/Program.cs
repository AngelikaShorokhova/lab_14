using System;
using PersonLibrary;
using System.Collections.Generic;
using System.Linq;

namespace lab_14
{
    class Program
    {
        static void WorkersOfFirstWorkshop(List<List<Person>> people)
        {
            var workers = from f in people
                          from p in f
                          where p is Worker && ((Worker)p).Workshop == "первый"
                          select p;
            if(workers.Count()==0)
                Console.WriteLine("Рабочих первого цеха нет");
            foreach (var item in workers)
                item.Show();
            Console.WriteLine();
        }

        static void EmplOfThisProfession(List<List<Person>> people)
        {
            var employees = people.Where(f => f is List<Person>).SelectMany(f => f).Where(p => p is Employee && ((Employee)p).Profession == "секретарь").Select(p => p);
            
            if (employees.Count() == 0)
                Console.WriteLine("Служащих-секретарей нет");
            foreach (var item in employees)
                item.Show();
            Console.WriteLine();
        }

        static void NumberOfEmpl(List<List<Person>> people)
        {
            int n = (from f in people
                     from p in f
                     where p is Employee && ((Employee)p).Experience > 10
                     select p).Count();

            Console.WriteLine(n);
            Console.WriteLine();
        }

        static void Intersection(List<Person> factory1, List<Person> factory2)
        {
            var k = from f in factory1.Intersect(factory2)
                    select f;

            foreach (var item in k)
                item.Show();
            Console.WriteLine();
        }

        static void MaxMin(List<List<Person>> people)
        {
            var max = from f in people
                     from p in f
                     where p.Age == ((from f in people
                                      from p in f
                                      select p.Age).Max())
                     select p;

            foreach (var item in max)
                item.Show();
            Console.WriteLine();

            int min = people.Where(f => f is List<Person>).SelectMany(f => f).Where(p => p is Person).Min(p => p.Age);
            var minp = people.Where(f => f is List<Person>).SelectMany(f => f).Where(p => p is Person && p.Age == min).Select(p => p);
            foreach (var item in minp)
                item.Show();
            Console.WriteLine();
        }

        static void Group(List<List<Person>> people)
        {
            var gr = from f in people
                    from p in f
                    where p is Worker
                    group p by ((Worker)p).Workshop;
            foreach (IGrouping<string, Person> item in gr)
            {
                Console.WriteLine(item.Key);
                foreach (var t in item)
                    ((Worker)t).Show();
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            List<Person> factory1 = new List<Person>();
            List<Person> factory2 = new List<Person>();
            List<Person> factory3 = new List<Person>();

            #region Заполнение коллекций
            for (int i = 0; i < 5; i++)
            {
                Random random = new Random();
                if (random.Next(0, 3) == 1)
                    factory1.Add(new Employee());
                if (random.Next(0, 3) == 2)
                    factory1.Add(new Worker());
                if (random.Next(0, 3) == 0)
                    factory1.Add(new Engineer());
            }
            factory1.Add(new Engineer("Victor Petrov", 40, 1));
            for (int i = 0; i < 5; i++)
            {
                Random random = new Random();
                if (random.Next(0, 3) == 1)
                    factory2.Add(new Employee());
                if (random.Next(0, 3) == 2)
                    factory2.Add(new Worker());
                if (random.Next(0, 3) == 0)
                    factory2.Add(new Engineer());
            }
            factory2.Add(new Engineer("Victor Petrov", 40, 1));
            for (int i = 0; i < 5; i++)
            {
                Random random = new Random();
                if (random.Next(0, 3) == 1)
                    factory3.Add(new Employee());
                if (random.Next(0, 3) == 2)
                    factory3.Add(new Worker());
                if (random.Next(0, 3) == 0)
                    factory3.Add(new Engineer());
            }
            #endregion

            List<List<Person>> people = new List<List<Person>>();
            people.Add(factory1); people.Add(factory2); people.Add(factory3);

            var items = from f in people
                        from p in f
                        select p;

            foreach (var item in items)
                item.Show();
            Console.WriteLine();

            Console.WriteLine("Рабочие первого цеха:");
            WorkersOfFirstWorkshop(people);

            Console.WriteLine("Служащие-секретари:");
            EmplOfThisProfession(people);

            Console.WriteLine("Количество служащих со стажем более 10 лет:");
            NumberOfEmpl(people);

            Console.WriteLine("Пересечение:");
            Intersection(factory1, factory2);

            Console.WriteLine("Самые взрослые и самые молодые работники:");
            MaxMin(people);

            Console.WriteLine("Группировка рабочих:");
            Group(people);
        }
    }
}
