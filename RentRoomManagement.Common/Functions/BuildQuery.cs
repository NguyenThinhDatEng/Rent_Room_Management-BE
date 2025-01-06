﻿using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Query;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Functions
{
    public class BuildQuery
    {
        public static string TableNameMapper<T>()
        {
            var type = typeof(T);
            TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            var name = string.Empty;

            if (tableAttribute != null)
            {
                name = tableAttribute.Name;
            }

            return name;
        }


        public static List<string> BuildCondition(List<FilterItem> filters)
        {
            List<string> conditions = new List<string>();

            foreach (var filter in filters)
            {
                string condition = "";
                switch (filter.Operator)
                {
                    case FilterOperator.Equal:
                        condition = $"{filter.Field} = '{filter.Value}'";
                        break;
                    case FilterOperator.NotEqual:
                        condition = $"{filter.Field} <> '{filter.Value}'";
                        break;
                    case FilterOperator.GreaterThan:
                        condition = $"{filter.Field} > '{filter.Value}'";
                        break;
                    case FilterOperator.LessThan:
                        condition = $"{filter.Field} < '{filter.Value}'";
                        break;
                    case FilterOperator.Contains:
                        condition = $"{filter.Field} LIKE '%{filter.Value}%'";
                        break;
                    case FilterOperator.IN:
                        Type filterValType = filter.Value.GetType();
                        if (filterValType.IsGenericType && filterValType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var genericType = filterValType.GetGenericArguments()[0];
                            var filterVals = new List<string>();
                            if (genericType == typeof(Guid))
                            {
                                var list = filter.Value as List<Guid>;
                                filterVals = list?.Select(x => $"'{x.ToString()}'").ToList();
                            }
                            else if (genericType == typeof(int))
                            {
                                var list = filter.Value as List<int>;
                                filterVals = list?.Select(x => $"'{x.ToString()}'").ToList();
                            }
                            else
                            {
                                var list = filter.Value as List<string>;
                                filterVals = list?.Select(x => $"'{x}'").ToList();
                            }

                            filter.Value = string.Join(",", filterVals);
                        }

                        condition = $"{filter.Field} IN ({filter.Value})";
                        break;
                    default:
                        // Handle other operators if needed
                        break;
                }

                conditions.Add(condition);
            }

            return conditions;
        }

        /// <summary>
        /// Build điều kiện cho câu truy vấn
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="orGroup"></param>
        /// <returns></returns>
        public static string BuildWhereClause(List<FilterItem> filters, List<FilterItem>? orGroup = null)
        {
            if (filters == null || filters.Count == 0)
            {
                return "";
            }

            List<string> andConditions = BuildCondition(filters);
            List<string> orConditions = BuildCondition(orGroup);

            var andCondition = $"({string.Join($" AND ", andConditions)})";
            var conditionStr = andCondition;
            if (orGroup?.Count > 0)
            {
                var orCondition = $"({string.Join($" OR ", orConditions)})";
                conditionStr = $"{conditionStr} AND {orCondition}";
            }

            return $"WHERE {conditionStr}";
        }
    }
}
