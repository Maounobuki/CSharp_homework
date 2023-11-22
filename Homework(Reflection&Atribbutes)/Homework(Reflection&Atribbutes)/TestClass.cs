namespace Homework_Reflection_Atribbutes_
{
    internal class TestClass
    {
        [CustomNameAttribute("CustomFieldName:I")]
        public int I { get; set; }
        [CustomNameAttribute("CustomFieldName:S")]
        private string? S { get; set; }
        [CustomNameAttribute("CustomFieldName:D")]
        public decimal D { get; set; }
        [CustomNameAttribute("CustomFieldName:C")]
        public char[]? C { get; set; }
        public string? Line { get; }


        public TestClass() { }
        public TestClass(int i) => this.I = i;
        public TestClass(int i, string s, decimal d, char[] c) : this(i)
        {
            this.S = s;
            this.D = d;
            this.C = c;
            this.Line = "Error!";
        }
    }
}
