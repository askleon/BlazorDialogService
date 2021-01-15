using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorDialogService
{
    public class DialogBuilder<T>
    {
        private readonly Func<DialogParameters<T>, Task<DialogResult>> showDialogFunc;

        internal DialogBuilder(Func<DialogParameters<T>, Task<DialogResult>> showDialogFunc)
        {
            this.Parameters = new DialogParameters<T>();
            this.showDialogFunc = showDialogFunc;
        }

        private DialogParameters<T> Parameters { get; set; }

        public DialogBuilder<T> AddParameter<P>(Expression<Func<T, P>> propertySelector, P value)
        {
            Parameters.Add(propertySelector, value);
            return this;
        }

        public Task<DialogResult> ShowDialog()
        {
            return showDialogFunc(this.Parameters);
        }
    }
}