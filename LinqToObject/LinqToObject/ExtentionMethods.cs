using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObject
{
    static class ExtentionMethods
    {
        public static Object CopyTo(this Object source, Object target)
        {
            var sourceProperties = source.GetType().GetProperties().Where(p=>p.CanRead);
            var targetProperties = target.GetType().GetProperties().Where(p=>p.CanWrite).ToList();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var targetProperty in targetProperties)
                {
                    if (sourceProperty.Name==targetProperty.Name && sourceProperty.PropertyType==targetProperty.PropertyType)
                    {
                        targetProperty.SetValue(target,sourceProperty.GetValue(source));
                    }
                }
            }
            return target;

        }
    }
}
