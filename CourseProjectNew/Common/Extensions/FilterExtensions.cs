using CourseProjectNew.Common.Filters;
using System.Linq.Expressions;

namespace CourseProjectNew.Common.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<T> FilterByDictionary<T>(this IQueryable<T> query, Dictionary<string, Filter> filters)
        {

            var expression = CreateFilterExpression<T>(filters);

            return query.Where(expression);
        }
        private static Expression<Func<T,bool>> CreateFilterExpression<T>(Dictionary<string, Filter> filters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            HashSet<Expression> expressions = new HashSet<Expression>();
            foreach (var key in filters.Keys)
            {
                var filter = filters[key];
                var parts = key.Split('.');
                Expression property = parameter;
                foreach (var part in parts)
                {
                    property = Expression.Property(property, part);
                }
                var filterExpression = CreateFilterExpression<T>(property, filter);
                expressions.Add(filterExpression);
            }
            var body = expressions.Aggregate(Expression.AndAlso);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
        private static Expression CreateFilterExpression<T>(Expression property, Filter filter)
        {
            HashSet<Expression> expressions = new HashSet<Expression>();
            var values = filter.FilterValues;
            foreach (var value in values)
            {
                Expression body;
                var filterValue = value.Value;
                var converted = Convert.ChangeType(filterValue, property.Type);
                var constant = Expression.Constant(converted);
                var operation = value.Operation;
                switch (operation)
                {
                    case FilterOperation.Equal:
                        body = Expression.Equal(property, constant);
                        break;
                    case FilterOperation.NotEqual:
                        body = Expression.NotEqual(property, constant);
                        break;
                    case FilterOperation.Contains:
                        body = Expression.Call(property, "Contains", null, constant);
                        break;
                    case FilterOperation.GreaterThan:
                        body = Expression.GreaterThan(property, constant);
                        break;
                    case FilterOperation.GreaterThanOrEqual:
                        body = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case FilterOperation.LessThan:
                        body = Expression.LessThan(property, constant);
                        break;
                    case FilterOperation.LessThanOrEqual:
                        body = Expression.LessThanOrEqual(property, constant);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                expressions.Add(body);
            }
            return filter.UseAndOperator ? expressions.Aggregate(Expression.AndAlso) : expressions.Aggregate(Expression.Or);
            
        }
    }
}
