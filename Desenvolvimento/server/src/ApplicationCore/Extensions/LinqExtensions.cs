using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using ApplicationCore.Helpers.Pagination;

namespace ApplicationCore.Extensions
{
    public static class LinqExtensions
    {

        public static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
                else
                {
                    return source.OrderBy(x => 1);
                }
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }


        private static object GetDefaultValue(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// Returns a sortable expression based on a column and direction.
        /// </summary>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, string direction)
        {
            string methodName = string.Format("{0}", direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending");

            return ApplyOrder(query, sortColumn, methodName);
        }


        /// <summary>
        /// Filters an expression based on jqGrid arguments.
        /// </summary>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter where)
        {
            if (where == null)
                return query;

            if (where.groupOp == GroupOperation.AND)
            {
                foreach (var rule in where.rules)
                {
                    query = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                }
            }
            else
            {
                var temp = (new List<T>()).AsQueryable();
                foreach (var rule in where.rules)
                {
                    var t = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                    temp = temp.Concat(t);
                }

                query = temp.Distinct();
            }

            return query;
        }

        public static IQueryable<T> Where<T>(this IQueryable<T> query, GridSettings postGrid, out int totalCount)
        {
            var newQuery = query.Where(postGrid.filters, out int newTotalItens, postGrid.page, postGrid.rows, postGrid.sidx, postGrid.sord);
            totalCount = newTotalItens;
            return newQuery;
        }
        /// <summary>
        /// Filters an expression based on jqGrid arguments.
        /// </summary>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, Filter where, out int totalCount, int start = 0, int limit = 0, string sortColumn = "", string direction = "")
        {
            if (where != null)
            {
                if (where.groupOp == GroupOperation.AND)
                {
                    if (where.rules != null && where.rules.Any())
                    {
                        foreach (var rule in where.rules)
                        {
                            if (rule.op != "nn" && rule.data != "undefined")
                            {
                                if (rule.data != null)
                                {
                                    query = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                                }
                            }
                            else
                            {
                                query = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                            }
                        }
                    }
                }
                else
                {
                    var temp = (new List<T>()).AsQueryable();

                    if (where.rules != null && where.rules.Any())
                    {
                        foreach (var rule in where.rules)
                        {
                            var t = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                            temp = temp.Concat(t);
                        }
                        //TODO: MELHORAR Distinct
                        if (temp.Any())
                        {
                            if (temp.First().GetType().GetProperty("Id") == null)
                            {
                                throw new ArgumentException($"LinqExtensions error: Não existe propriedade 'Id' na entidade");
                            }
                            var groupById = temp.GroupBy(t => t.GetType().GetProperty("Id").GetValue(t));
                            temp = groupById.Select(g => g.First());
                        }

                        query = temp.Distinct();
                    }
                }

                if (where.groups != null && where.groups.Count > 0)
                {

                    foreach (var group in where.groups)
                    {

                        if (group.groupOp == GroupOperation.AND)
                        {
                            if (group.rules != null && group.rules.Any())
                            {
                                foreach (var rule in group.rules)
                                {
                                    if (rule.op != "nn" && rule.data != "undefined")
                                    {
                                        if (rule.data != null)
                                        {
                                            query = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                                        }
                                    }
                                    else
                                    {
                                        query = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                                    }
                                }
                            }
                        }
                        else
                        {
                            var temp = (new List<T>()).AsQueryable();

                            if (group.rules != null && group.rules.Any())
                            {
                                foreach (var rule in group.rules)
                                {
                                    var t = query.Where(rule.field, rule.data, StringEnum.Parse<WhereOperation>(rule.op));
                                    temp = temp.Concat(t);
                                }
                                //TODO: MELHORAR Distinct
                                if (temp.Any())
                                {
                                    if (temp.First().GetType().GetProperty("Id") == null)
                                    {
                                        throw new ArgumentException($"LinqExtensions error: Não existe propriedade 'Id' na entidade");
                                    }
                                    var groupById = temp.GroupBy(t => t.GetType().GetProperty("Id").GetValue(t));
                                    temp = groupById.Select(g => g.First());
                                }

                                query = temp.Distinct();
                            }
                        }

                    }
                }

            }

            if (!string.IsNullOrEmpty(sortColumn))
            {

                query = OrderBy(query, sortColumn, (direction == null || direction == string.Empty ? "asc" : direction));
            }

            totalCount = query.Count();

            //if ((start + limit) > totalCount)
            //{
            //    limit = totalCount;
            //}

            start = (start - 1) * limit;

            query = (start < 0 || limit < 0) ? query : query.Skip(start).Take(limit);


            return query;
        }

        /// <summary>
        /// Filters an expression based on jqGrid arguments.
        /// </summary>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, string column, string value, WhereOperation operation)
        {
            if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value))
                return query;

            //value = value != null ? value.ToLower().RemoverAcentos() : value;

            value = value != null ? value.ToLower() : value;


            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;

            //Verifica se exite a propriedade(property) no objeto
            try
            {
                foreach (var property in column.Split('.'))
                    memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);
            }
            catch (System.ArgumentException)
            {
                return query;
            }


            //change param value type
            //necessary to getting bool from string
            //ConstantExpression filter = Expression.Constant(Convert.ChangeType(value, memberAccess.Type));

            Expression condition = null;
            LambdaExpression lambda = null;
            Expression nullCheck_2;
            Expression nullCheck_1 = null;
            Expression toLower = memberAccess.Type == typeof(string) ? Expression.Call(memberAccess, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes)) : null;

            ConstantExpression filter = Expression.Constant(ChangeType(value, memberAccess.Type), memberAccess.Type);

            switch (operation)
            {
                case WhereOperation.Equal:
                    if (toLower != null)
                    {
                        condition = Expression.Equal(toLower ?? memberAccess, filter);

                        //MethodInfo remove = typeof(Filtro).GetMethod("RemoverAcentos", new[] { typeof(string) });
                        //Expression conditionRemover = Expression.Call(null, remove, toLower);

                        //condition = Expression.Equal(conditionRemover, filter);
                    }
                    else
                    {
                        condition = Expression.Equal(memberAccess, filter);
                    }

                    if (value == "0" && (filter.Type == typeof(int) || filter.Type == typeof(decimal) || filter.Type == typeof(double) || filter.Type == typeof(long)))
                    {
                        //Quando valor for 0 e number, n deve remover 0 da consulta: Ex: Buscar por enum com valor 0
                    }
                    else
                    {
                        nullCheck_1 = RetornarExpressaoConformeTipo(memberAccess, filter.Type);
                    }

                    nullCheck_2 = Expression.NotEqual(memberAccess.Expression, Expression.Constant(null, typeof(object)));

                    if (nullCheck_1 != null)
                    {
                        condition = Expression.AndAlso(nullCheck_1, condition);
                    }

                    condition = Expression.AndAlso(nullCheck_2, condition);

                    break;

                case WhereOperation.NotEqual:
                    condition = Expression.NotEqual(toLower ?? memberAccess, filter);
                    break;

                case WhereOperation.Contains:
                    if (toLower != null)
                    {
                        List<Type> types = new List<Type>();
                        types.Add(filter.Type);
                        condition = Expression.Call(toLower ?? memberAccess, typeof(string).GetMethod("Contains", types.ToArray()), Expression.Constant(value));
                        nullCheck_2 = Expression.NotEqual(memberAccess, Expression.Constant(null, typeof(object)));
                        condition = Expression.AndAlso(nullCheck_2, condition);

                        /*MethodInfo methodInfo = typeof(StringExtensions).GetMethod("RemoveDiacritics", new[] { typeof(string) });
                        Expression expressaoParaRemocaoDeAcentos = Expression.Call(null, methodInfo, toLower);

                        condition = Expression.Call(expressaoParaRemocaoDeAcentos, typeof(string).GetMethod("Contains"), Expression.Constant(value));
                        nullCheck_2 = Expression.NotEqual(memberAccess, Expression.Constant(null, typeof(object)));
                        condition = Expression.AndAlso(nullCheck_2, condition);*/
                    }
                    else
                    {
                        List<Type> types = new List<Type>();
                        types.Add(typeof(string));
                        Expression valorDaPropriedadeString = Expression.Call(memberAccess, typeof(object).GetMethod("ToString"));
                        condition = Expression.Call(valorDaPropriedadeString, typeof(string).GetMethod("Contains", types.ToArray()), Expression.Constant(value.ToString()));
                    }
                    break;

                case WhereOperation.NotNull:
                    condition = Expression.NotEqual(toLower ?? memberAccess, Expression.Constant(null, filter.Type));
                    break;

                case WhereOperation.Null:
                    condition = Expression.Equal(toLower ?? memberAccess, Expression.Constant(null, filter.Type));
                    break;

                case WhereOperation.GreaterThan:
                    condition = Expression.GreaterThan(toLower ?? memberAccess, filter);
                    break;

                case WhereOperation.GreaterThanOrEqual:
                    condition = Expression.GreaterThanOrEqual(toLower ?? memberAccess, filter);
                    break;

                case WhereOperation.LessThan:
                    condition = Expression.LessThan(toLower ?? memberAccess, filter);
                    break;

                case WhereOperation.LessThanOrEqual:
                    condition = Expression.LessThanOrEqual(toLower ?? memberAccess, filter);
                    break;

                case WhereOperation.BeginsWith:
                    condition = Expression.Call(toLower,
                        typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
                        Expression.Constant(value));
                    break;

                case WhereOperation.NotBeginsWith:
                    condition = Expression.Call(toLower,
                        typeof(string).GetMethod("StartsWith", new[] { typeof(string) }),
                        Expression.Constant(value));
                    condition = Expression.Not(condition);
                    break;

                case WhereOperation.In:
                    condition = Expression.Call(toLower,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                        Expression.Constant(value));
                    break;

                case WhereOperation.NotIn:
                    condition = Expression.Call(toLower,
                        typeof(string).GetMethod("Contains", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null),
                        Expression.Constant(value));
                    condition = Expression.Not(condition);
                    break;

                case WhereOperation.EndWith:
                    condition = Expression.Call(toLower,
                        typeof(string).GetMethod("EndsWith", new[] { typeof(string) }),
                        Expression.Constant(value));
                    break;

                case WhereOperation.NotEndWith:
                    condition = Expression.Call(toLower,
                        typeof(string).GetMethod("EndsWith", new[] { typeof(string) }),
                        Expression.Constant(value));
                    condition = Expression.Not(condition);
                    break;

                case WhereOperation.NotContains:
                    condition = Expression.Call(toLower,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                        Expression.Constant(value));
                    condition = Expression.Not(condition);
                    nullCheck_2 = Expression.NotEqual(memberAccess, Expression.Constant(null, typeof(object)));
                    condition = Expression.AndAlso(nullCheck_2, condition);
                    break;
            }

            lambda = Expression.Lambda(condition, parameter);
            MethodCallExpression result = Expression.Call(
                  typeof(Queryable), "Where",
                  new[] { query.ElementType },
                  query.Expression,
                  lambda);

            return query.Provider.CreateQuery<T>(result);
        }

        public static Expression RetornarExpressaoConformeTipo(MemberExpression memberAccess, Type tipo)
        {
            if (tipo == typeof(String))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(null, tipo));
            }
            else if (tipo == typeof(Int32) || tipo == typeof(Decimal) || tipo == typeof(Double))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(0, tipo));
            }
            else if (tipo == typeof(object))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(null, typeof(object)));
            }
            else if (tipo == typeof(Int32?) || tipo == typeof(Decimal?) || tipo == typeof(Double?))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(null, tipo));
            }
            else if (tipo == typeof(long))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(0L, tipo));
            }
            else if (tipo == typeof(long?))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(null, tipo));
            }
            else if (tipo == typeof(DateTime))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(default(DateTime), tipo));
            }
            else if (tipo == typeof(DateTime?))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(default(DateTime?), tipo));
            }
            else if (tipo == typeof(bool?))
            {
                return Expression.NotEqual(memberAccess, Expression.Constant(null, tipo));
            }
            else
            {
                return null;
                // TODO: Verrificar necessidade de Exception
                //throw new Exception("Tipo não especificado.");
            }
        }

        public static object ChangeType(object value, Type conversionType)
        {
            // Note: This if block was taken from Convert.ChangeType as is, and is needed here since we're
            // checking properties on conversionType below.
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            } // end if

            // If it's not a nullable type, just pass through the parameters to Convert.ChangeType

            if (conversionType.IsGenericType &&
              conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                // It's a nullable type, so instead of calling Convert.ChangeType directly which would throw a
                // InvalidCastException (per http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx),
                // determine what the underlying type is
                // If it's null, it won't convert to the underlying type, but that's fine since nulls don't really
                // have a type--so just return null
                // Note: We only do this check if we're converting to a nullable type, since doing it outside
                // would diverge from Convert.ChangeType's behavior, which throws an InvalidCastException if
                // value is null and conversionType is a value type.
                if (value == null)
                {
                    return null;
                } // end if

                // It's a nullable type, and not null, so that means it can be converted to its underlying type,
                // so overwrite the passed-in conversion type with this underlying type
                var nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            } // end if

            // Now that we've guaranteed conversionType is something Convert.ChangeType can handle (i.e. not a
            // nullable type), pass the call on to Convert.ChangeType
            if (conversionType == typeof(Double)) value = ((string)value).Replace(".", "");
            return Convert.ChangeType(value, conversionType);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
