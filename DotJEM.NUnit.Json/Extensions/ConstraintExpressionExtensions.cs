using System;
using System.Linq.Expressions;
using DotJEM.NUnit.Json.Helpers;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;

namespace DotJEM.NUnit.Json.Extensions
{
    public static class ConstraintExpressionExtensions
    {
        /// <summary/>
        public static ResolvableConstraintExpression Property<T>(this ConstraintExpression self, Expression<Func<T, object>> property)
        {
            return self.Property(property.GetPropertyInfo().Name);
        }

        /// <summary/>
        public static Constraint Matches<T>(this ConstraintExpression self, Predicate<T> predicate)
        {
            return self.Matches(predicate);
        }

        /// <summary/>
        public static Constraint That(this ConstraintExpression self, IResolveConstraint constraint)
        {
            return self.Matches(constraint.Resolve());
        }
    }

    public static class ObjectExtionsions
    {
        public static JToken TryJToken(this object value)
        {
            //Note: If expected was not a JToken, Most likely used with an anonomous type...
            //      but this also means we can allow for actual business objects to be passed in directly.
            string str = value as string;
            return str != null ? JToken.Parse(str)
                : (value as JToken ?? JToken.FromObject(value));
        }

        public static JObject TryJObject(this object value)
        {
            //Note: If expected was not a JObject, Most likely used with an anonomous type...
            //      but this also means we can allow for actual business objects to be passed in directly.
            string str = value as string;
            return str != null ? JObject.Parse(str)
                : (value as JObject ?? JObject.FromObject(value));
        }

        public static JArray TryJArray(this object value)
        {
            //Note: If expected was not a JArray, Most likely used with an anonomous type...
            //      but this also means we can allow for actual business objects to be passed in directly.
            string str = value as string;
            return str != null ? JArray.Parse(str)
                : (value as JArray ?? JArray.FromObject(value));
        }
    }
}