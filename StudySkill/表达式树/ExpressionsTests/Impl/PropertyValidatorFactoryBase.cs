using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressionsTests
{
    public abstract class PropertyValidatorFactoryBase<TValue> : IPropertyValidatorFactory
    {

        public virtual IEnumerable<Expression> CreateExpression(CreatePropertyValidatorInput input)
        {
            if (input.Property.PropertyType != typeof(TValue))
            {
                return Enumerable.Empty<Expression>();
            }
            return CreateExpressionCore(input);
        }

        protected abstract IEnumerable<Expression> CreateExpressionCore(CreatePropertyValidatorInput input);
    }
}
