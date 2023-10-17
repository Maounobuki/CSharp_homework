namespace Homework_OOP_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var husband = new FamilyMember("Олег", DateTime.Parse("20.08.1992"));
            var wife = new FamilyMember("Мария", DateTime.Parse("06.06.1993"));
            var wiwesfather = new FamilyMember("Сергей", DateTime.Parse("25.03.1976"));
            var wiwesmother = new FamilyMember("Елена", DateTime.Parse("29.02.1980"));
            var husbandsfather = new FamilyMember("Андрей", DateTime.Parse("21.09.1975"));
            var husbandsmother = new FamilyMember("Нина", DateTime.Parse("30.05.1978"));
            var son = new FamilyMember("Евгений", DateTime.Parse("17.01.2014"));
            var dauther = new FamilyMember("Ангелина", DateTime.Parse("26.07.2018"));

            husband.AddFamilyInfo(wife, husbandsfather, husbandsmother, son, dauther);
            wife.AddFamilyInfo(husband, wiwesfather, wiwesmother, son, dauther);
            husband.PrintFamilyInfo();
            wife.PrintFamilyInfo();
            Console.ReadLine();
        }

        
    }
}