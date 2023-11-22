namespace Homework_Reflection_Atribbutes_
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CustomNameAttribute : Attribute
    {
        public string Name { get; }
        public CustomNameAttribute(string name)
        {
            Name = name;
        }
    }
}
