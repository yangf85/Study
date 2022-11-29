using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DynamicGroup
{
    internal class ExpressionGroup
    {
        public static Expression<Func<T, dynamic>> Builder<T>(IEnumerable<string> propertyNames)
        {
            var properties = propertyNames.Select(n => typeof(T).GetProperty(n)).ToList();
            var propertyTypes = properties.Select(p => p.PropertyType).ToArray();

            var tupleDef = typeof(Tuple).Assembly.GetType("System.Tuple`" + properties.Count);
            var tupleType = tupleDef.MakeGenericType(propertyTypes);
            var construct = tupleType.GetConstructor(propertyTypes);

            var param = Expression.Parameter(typeof(T), "obj");
            var exp = Expression.New(construct, properties.Select(p => Expression.Property(param, p)));
            return Expression.Lambda<Func<T, dynamic>>(exp, param);
        }
    }
}