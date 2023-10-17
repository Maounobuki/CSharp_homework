using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_OOP_
{
    internal class FamilyMember
    {
        public string Name;
        public DateTime Birthday;
        public FamilyMember Spouse = null;
        public FamilyMember Father = null;
        public FamilyMember Mother = null;
        public FamilyMember[] Children = null;
      

        public FamilyMember(string name, DateTime birthday)
        {
            Name = name;
            Birthday = birthday;
        }
      
        void Print()
        {
            Console.WriteLine($"Имя: {Name}, дата рождения = {Birthday.ToString("dd.mm.yyyy")} ");
        }

        public void AddFamilyInfo(FamilyMember spouse, FamilyMember father, FamilyMember mother, params FamilyMember[] children)
        {
            Spouse = spouse;
            Father = father;
            Mother = mother;
            Children = children;
        }

        public void PrintFamilyInfo()
        {
            if ( Spouse != null )
            {
                Console.Write($"Супруг(а): ");
                Spouse.Print();
            }
            if ( Father != null )
            {
                Console.Write($"Отец: ");
                Father.Print();
            }
            if( Mother != null )
            {
                Console.Write($"Мать: ");
                Mother.Print();
            }
            if( Children != null )
            {
                Console.WriteLine($"Дети: ");
                foreach ( var child in Children )
                {
                    child.Print();
                }
            }
        }
    }

  
}
