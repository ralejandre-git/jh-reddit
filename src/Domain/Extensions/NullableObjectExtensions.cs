using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class NullableObjectExtensions
    {
        public static T ThrowIfNull<T>(this T parameter, string parameterName) where T : class
        {
            parameterName = parameterName ?? string.Empty;

            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return parameter;
        }
    }
}
