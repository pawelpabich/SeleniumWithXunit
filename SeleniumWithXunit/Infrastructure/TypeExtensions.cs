using System;

namespace SeleniumWithXunit.Infrastructure
{
    public static class TypeExtensions
    {
        public static bool Implements<TTypeToImplemenet>(this Type type)
        {
            var typeToImplement = typeof(TTypeToImplemenet);
            return Implements(type, typeToImplement);
        }

        public static bool Implements(this Type type, Type typeToImplement)
        {
            return typeToImplement.IsAssignableFrom(type) && type != typeToImplement;
        }
    }
}