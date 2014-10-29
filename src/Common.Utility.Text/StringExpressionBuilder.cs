using System;
using System.Text;

namespace Common.Utility.Text
{
    /// <summary>
    /// A helper class for building string expressions
    /// </summary>
    public class StringExpressionBuilder
    {
        private readonly StringBuilder _sb;

        public StringExpressionBuilder()
        {
            _sb = new StringBuilder();
        }

        public StringExpressionBuilder(int capacity)
        {
            _sb = new StringBuilder(capacity);
        }

        public StringExpressionBuilder EqualsParamName(string key, string paramName)
        {
            _sb.Append(" ");
            _sb.Append(key);
            _sb.Append(" = @");
            _sb.Append(paramName);
            _sb.Append(" ");

            return this;
        }

        public StringExpressionBuilder Equals<T>(string key, T value)
        {
            _sb.Append(" ");
            _sb.Append(key);
            _sb.Append(" = ");
            _sb.Append(FormatValueBasedOnType(value));
            _sb.Append(" ");

            return this;
        }

        public StringExpressionBuilder NotEquals<T>(string key, T value)
        {
            _sb.Append(" ");
            _sb.Append(key);
            _sb.Append(" <> ");
            _sb.Append(FormatValueBasedOnType(value));
            _sb.Append(" ");

            return this;
        }

        public StringExpressionBuilder LessThan<T>(string key, T value)
        {
            _sb.Append(" ");
            _sb.Append(key);
            _sb.Append(" < ");
            _sb.Append(FormatValueBasedOnType(value));
            _sb.Append(" ");

            return this;
        }

        public StringExpressionBuilder LessThanOrEquals<T>(string key, T value)
        {
            _sb.Append(" ");
            _sb.Append(key);
            _sb.Append(" <= ");
            _sb.Append(FormatValueBasedOnType(value));
            _sb.Append(" ");

            return this;
        }

        public StringExpressionBuilder GreaterThan<T>(string key, T value)
        {
            _sb.Append(" ");
            _sb.Append(key);
            _sb.Append(" > ");
            _sb.Append(FormatValueBasedOnType(value));
            _sb.Append(" ");

            return this;
        }

        public StringExpressionBuilder GreaterThanOrEquals<T>(string key, T value)
        {
            _sb.Append(" ");
            _sb.Append(key);
            _sb.Append(" >= ");
            _sb.Append(FormatValueBasedOnType(value));
            _sb.Append(" ");

            return this;
        }

        public StringExpressionBuilder And()
        {
            _sb.Append(" AND ");

            return this;
        }

        public StringExpressionBuilder Or()
        {
            _sb.Append(" OR ");

            return this;
        }

        public string Build()
        {
            var expression = _sb.ToString();
            if (expression.EndsWith(" AND "))
            {
                var index = expression.LastIndexOf(" AND ");
                if (index > -1)
                    return expression.Substring(0, index);
            }

            if (expression.EndsWith(" OR "))
            {
                var index = expression.LastIndexOf(" OR ");
                if (index > -1)
                    return expression.Substring(0, index);
            }

            return expression;
        }

        private static string FormatValueBasedOnType<T>(T value)
        {
            Type t = typeof(T);
            if (t == typeof(object))
                t = value.GetType();

            if (t == typeof(int) || t == typeof(long) || t == typeof(decimal) || t == typeof(double) ||
                t == typeof(short) || t == typeof(float))
            {
                return value.ToString();
            }
            else if (t == typeof(DateTime))
            {
                return "'" + Convert.ToDateTime(value).ToLongDateString() + "'";
            }
            else if (t == typeof(string))
            {
                return "'" + value + "'";
            }
            else
            {
                throw new ArgumentOutOfRangeException(t + " type is not supported");
            }
        }
    }
}
