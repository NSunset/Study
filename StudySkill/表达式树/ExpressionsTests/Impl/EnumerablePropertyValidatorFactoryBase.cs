using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace ExpressionsTests
{
    public abstract class EnumerablePropertyValidatorFactoryBase : IPropertyValidatorFactory
    {
        public virtual IEnumerable<Expression> CreateExpression(CreatePropertyValidatorInput input)
        {
            Type propertyType = input.Property.PropertyType;
            if (propertyType == typeof(string))
            {
                return Enumerable.Empty<Expression>();
            }

            var interfaces = GetAllInterfaceIncludingSelf(propertyType).FirstOrDefault(x => x.Name == "IEnumerable`1");

            IEnumerable<Type> GetAllInterfaceIncludingSelf(Type type)
            {
                foreach (var i in type.GetInterfaces())
                {
                    yield return i;
                }
                yield return type;
            }
            if (interfaces == null)
            {
                return Enumerable.Empty<Expression>();
            }
            return CreateExpressionCore(input, interfaces);
        }

        protected abstract IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input, Type @iEnumerable);


    }
}
