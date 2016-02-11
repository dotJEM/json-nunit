using DotJEM.NUnit.Json.Constraints;
using DotJEM.NUnit.Json.Extensions;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace DotJEM.NUnit.Json
{
    public static class JsonHas
    {
        public static IResolveConstraint Properties(object expected)
        {
            return new HasJsonPropertiesConstraint(expected.TryJObject());
        }
    }
}