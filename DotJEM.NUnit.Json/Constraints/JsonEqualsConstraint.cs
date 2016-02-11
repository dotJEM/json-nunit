using System.Collections.Generic;
using DotJEM.NUnit.Json.Helpers;
using Newtonsoft.Json.Linq;

// ReSharper disable ParameterHidesMember
namespace DotJEM.NUnit.Json.Constraints
{


    // ReSharper disable InconsistentNaming

    public class JsonEqualsConstraint : AbstractConstraint
    {
        private readonly JToken expectedJson;

        public JsonEqualsConstraint(JToken expectedJson)
        {
            this.expectedJson = expectedJson;
        }
        protected override void DoMatches(object actual)
        {
            JToken actualJson = actual as JToken;
            if (actualJson == null)
            {
                FailWithMessage("Object was not a JToken");
                return;
            }

            if (JToken.DeepEquals(actualJson, expectedJson))
                return;

            QuickDiff(actualJson, expectedJson);
        }

        private void QuickDiff(JToken actual, JToken expected, string path = "")
        {
            if(actual == null && expected == null)
                return;

            if (actual == null)
            {
                FailWithMessage("Actual object did not contain property '{0}'", path);
                return;
            }

            if (expected == null)
            {
                FailWithMessage("Actual object did contain an unexpected property '{0}'", path);
                return;
            }

            if (actual.Type != expected.Type)
            {
                FailWithMessage("'{0}' was expected to be of type '{1}' but was of type '{2}'.", path, actual.Type, expected.Type);
                return;
            }

            JObject obj = expected as JObject;
            if (obj != null)
            {
                //Note: We compared types above, so we know they should pass for both in this case.
                QuickDiffObject(obj, (JObject)actual, path);
            }

            JArray array = expected as JArray;
            if (array != null)
            {
                //Note: We compared types above, so we know they should pass for both in this case.
                CompareJArray(array, (JArray)actual, path);
            }

            JValue value = expected as JValue;
            if (value != null)
            {
                //Note: We compared types above, so we know they should pass for both in this case.
                if (!value.Equals((JValue)actual))
                {
                    FailWithMessage("'{0}' was expected to be '{1}' but was '{2}'.",
                        path, expected, actual);
                }
            }
        }

        private void QuickDiffObject(JObject actual, JObject expected, string path = "")
        {
            foreach (string key in UnionKeys(actual, expected))
            {
                string propertyPath = string.IsNullOrEmpty(path) ? key : path + "." + key;

                QuickDiff(actual[key], expected[key], propertyPath);
            }
        }

        private void CompareJArray(JArray expectedArr, JArray actualArr, string propertyPath)
        {
            if (expectedArr.Count != actualArr.Count)
                FailWithMessage("'{0}' was expected to have '{1}' elements but had '{2}'.", propertyPath, expectedArr.Count, actualArr.Count);

            for (int i = 0; i < expectedArr.Count; i++)
            {
                string itemPath = propertyPath + "[" + i + "]";
                JToken expectedToken = expectedArr[i];
                JToken actualToken = actualArr[i];
                QuickDiff(actualToken, expectedToken, itemPath);

            }
        }

        private IEnumerable<string> UnionKeys(IDictionary<string, JToken> update, IDictionary<string, JToken> other)
        {
            HashSet<string> keys = new HashSet<string>(update.Keys);
            keys.UnionWith(other.Keys);
            return keys;
        }
    }
}
// ReSharper restore ParameterHidesMember
