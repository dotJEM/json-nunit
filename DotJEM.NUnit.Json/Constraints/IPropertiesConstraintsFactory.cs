namespace DotJEM.NUnit.Json.Constraints
{
    public interface IPropertiesConstraintsFactory
    {
        ObjectPropertiesEqualsConstraint<T> EqualTo<T>(T expected);
        ObjectPropertiesNotEqualsConstraint<T> NotEqualTo<T>(T expected);
    }

    internal class PropertiesConstraintsFactory : IPropertiesConstraintsFactory
    {
        public ObjectPropertiesEqualsConstraint<T> EqualTo<T>(T expected)
        {
            return new ObjectPropertiesEqualsConstraint<T>(expected);
        }

        public ObjectPropertiesNotEqualsConstraint<T> NotEqualTo<T>(T expected)
        {
            return new ObjectPropertiesNotEqualsConstraint<T>(expected);
        }
    }
}
