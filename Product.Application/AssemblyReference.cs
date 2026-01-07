using System.Reflection;

namespace Product.Application
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly; 
    }
}
