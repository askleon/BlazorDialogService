using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace BlazorDialogService
{
    public class DialogParameters<T> : DialogParametersBase
    {
        public DialogParameters<T> Add<P>(Expression<Func<T, P>> propertySelector, P value)
        {
            var body = propertySelector.Body;

            var propertyType = typeof(P);
            var valueType = value.GetType();

            if (propertyType != valueType)
                throw new Exception($"Type of value does not match property type. "
                    + $"Expected {propertyType.Name}, instead recieved {valueType.Name}.");

            if (body is null)
            {
                throw new Exception($"MemberExpression {nameof(propertySelector)}.Body is null.");
            }

            if (body is MemberExpression b)
            {
                var propertyName = b.Member.Name;
                
                var isParameter = b
                    .Member
                    .GetCustomAttributes(typeof(ParameterAttribute), false)
                    .Count() > 0;

                if (!isParameter)
                    throw new Exception($"{propertyName} is not a parameter. " 
                        + $"Expected {propertyName} to have [Parameter] attribute.");

                base.Add(propertyName, value);
            }
            else
            {
                throw new Exception($"body is type {body.GetType().Name}. "
                    + $"Expected body to be a MemberExpression.");
            }

            return this;
        }
    }
}