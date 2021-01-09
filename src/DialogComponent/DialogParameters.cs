using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BlazorDialogService.DialogComponent
{
    public class DialogParameters<T> : DialogParametersBase
    {
        public DialogParameters<T> Add<K>(Expression<Func<T, K>> keySelector, K value)
        {
            var body = keySelector.Body;

            if (body is null)
            {
                throw new Exception($"MemberExpression {nameof(keySelector)}.Body is null.");
            }

            if (body is MemberExpression b)
            {
                var key = b.Member.Name;
                base.Add(key, value);
            }
            else
            {
                throw new Exception($"body is type {body.GetType().Name}. Expected body to be a MemberExpression.");
            }

            return this;
        }
    }
}