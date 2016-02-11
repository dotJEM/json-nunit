using System;
using System.Linq.Expressions;
using DotJEM.NUnit.Json.Constraints;
using DotJEM.NUnit.Json.Helpers;
using NUnit.Framework.Constraints;

namespace DotJEM.NUnit.Json
{
    public static class ObjectHas
    {
        public static ResolvableConstraintExpression Property<T>(Expression<Func<T, object>> property)
        {
            return new ConstraintExpression().Property(property.GetPropertyInfo().Name);
        }

        public static ResolvableConstraintExpression Property<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            return new ConstraintExpression().Property(expression.GetPropertyInfo().Name);
        }

        public static IPropertiesConstraintsFactory Properties => new PropertiesConstraintsFactory();
    }
}