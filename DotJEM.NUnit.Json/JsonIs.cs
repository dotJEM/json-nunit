using DotJEM.NUnit.Json.Constraints;
using DotJEM.NUnit.Json.Extensions;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace DotJEM.NUnit.Json
{
    public static class JsonIs
    {
        public static IResolveConstraint EqualTo(object expected)
        {
            return new JsonEqualsConstraint(expected.TryJToken());
        }
    }
}