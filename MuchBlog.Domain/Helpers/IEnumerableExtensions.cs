using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace MuchBlog.Domain.Helpers
{
    public static class IEnumerableExtensions
    {
        //ExpandoObject类型的对象可以在程序运行时动态地添加删除成员
        public static IEnumerable<ExpandoObject> ShapeData<TSource>
            (this IEnumerable<TSource> source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var sourceList = source.ToList();
            var expandoObjectList = new List<ExpandoObject>(sourceList.Count());
            var propertyInfoList = new List<PropertyInfo>();
            //若fields为空则返回所有的属性
            if (string.IsNullOrWhiteSpace(fields))
            {
                //通过反射获取属性,获取的条件是属性是公共的且包括实例成员
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            //若fields不为空，则将fields所有的属性信息通过反射从类型中取出，并添加到propertyInfoList中
            else
            {
                var fieldsAfterSplit = fields.Split(",");
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property:{propertyName}没有找到：{typeof(TSource)}");
                    }
                    propertyInfoList.Add(propertyInfo);
                }
            }
            //通过属性信息列表propertyInfoList和源列表sourceList来获取特定的属性的值，propertyInfoList指定了属性名，sourceList指定了属性值
            //然后再把他们通过字典的方式添加到shapedObj中，通过循环就能得到fields指定属性名的值的源对象列表
            foreach (TSource obj in sourceList)
            {
                var shapedObj = new ExpandoObject();
                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(obj);
                    ((IDictionary<string, object>)shapedObj).Add(propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add(shapedObj);
            }

            return expandoObjectList;
        }

    }
}