using System.Reflection;
using System.Text;
using Homework_Reflection_Atribbutes_;

internal class Program
{
    static void Main(string[] args)
    {
        TestClass testClass = new TestClass(100, "this is text", 100.123m, new char[] { 'a', 'b', 'c', 'd' });
        var result = ObjectToString(testClass);
        Console.WriteLine(result);
    }

    public static TestClass CreateTestClassInstance(
        int i,
        int ii,
        string s,
        decimal d,
        char[] c,
        char[] cc)
    {
        var testClassType = typeof(TestClass);
        var testClass = Activator.CreateInstance(testClassType) as TestClass;

        var testClassTwo = Activator.CreateInstance(
            testClassType,
            new object[] { i });

        var testClassThird = Activator.CreateInstance(
            testClassType,
            new object[] { i, ii, s, d, c, cc });

        return testClassThird as TestClass;
    }

    static string ObjectToString(object o)
    {
        if (o == null)
            return null;

        var type = o.GetType();
        StringBuilder sb = new StringBuilder();
        sb.Append($"Name class: {type.Name}\n");

        var dataClass2 = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var item in dataClass2)
        {
            var resSelectAttribute = item.GetCustomAttributes(typeof(CustomNameAttribute), true).FirstOrDefault() as CustomNameAttribute;
            if (resSelectAttribute != null)
            {
                sb.Append($"({item.PropertyType.ToString().Split('.')[1]}) ");
                sb.Append(resSelectAttribute.Name);

                if (item.PropertyType == typeof(char[]))
                {
                    var ch = item.GetValue(o) as char[];
                    sb.Append(" = ");
                    foreach (var c in ch)
                    {
                        sb.Append(c.ToString() + ' ');
                    }
                    sb.Append("\n");
                }
                else
                {
                    sb.Append($" = {item.GetValue(o)}\n");
                }
            }
        }
        return sb.ToString();
    }

    static object StringToObject(string s, int iiii = 0)
    {
        var values = s.Split("\n");
        var obj = Activator.CreateInstance(values[1], values[0])?.Unwrap();

        if (values.Length > 1 && obj != null)
        {
            var type = obj.GetType();
            Console.WriteLine(type);

            for (int i = 1; i < values.Length; i++)
            {
                var nameAndValue = values[i].Split(":");
                var pi = type.GetProperty(nameAndValue[0]);

                Console.WriteLine($"{nameAndValue[0]}:{pi}");

                if (pi == null)
                    continue;

                if (pi.PropertyType == typeof(int))
                {
                    pi.SetValue(obj, int.Parse(nameAndValue[1]));
                }
                if (pi.PropertyType == typeof(string))
                {
                    pi.SetValue(obj, nameAndValue[1]);
                }
                if (pi.PropertyType == typeof(decimal))
                {
                    pi.SetValue(obj, decimal.Parse(nameAndValue[1]));
                }
                if (pi.PropertyType == typeof(char[]))
                {
                    pi.SetValue(obj, nameAndValue[1].ToCharArray());
                }

            }
        }
        return obj;
    }
}