using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorDialogService.DialogComponent
{
    public class DialogService
    {
        public event Action Changed;
        private Stack<DialogModel> dialogModels = new Stack<DialogModel>();
        public IEnumerable<DialogModel> DialogModels => dialogModels;

        public Task<DialogResult> ShowDialog<T>(DialogParametersBase parameters)
        {
            return ShowDialog(typeof(T), parameters);
        }

        public Task<DialogResult> ShowDialog(Type dialogComponentType, DialogParametersBase parameters)
        {
            ThrowIfNotComponent(dialogComponentType);

            var dialog = new DialogModel(dialogComponentType, parameters);
            dialogModels.Push(dialog);

            OnChanged();

            return dialog.Task;
        }

        public void Close(bool success)
        {
            var dialog = dialogModels.Pop();
            var result = new DialogResult
            {
                Success = success,
            };
            dialog.TaskSource.SetResult(result);
            OnChanged();
        }

        public void Close(object dialogResult)
        {
            var dialog = dialogModels.Pop();
            var result = new DialogResult
            {
                Result = dialogResult,
                Success = true,
            };
            dialog.TaskSource.SetResult(result);
            OnChanged();
        }

        public void Close(Exception exception)
        {
            var dialog = dialogModels.Pop();
            var result = new DialogResult
            {
                Success = false,
                Exception = exception,
            };
            dialog.TaskSource.SetResult(result);
            OnChanged();
        }

        private void OnChanged() => Changed?.Invoke();

        private void ThrowIfNotComponent(Type componentType)
        {
            if (!typeof(ComponentBase).IsAssignableFrom(componentType))
                throw new ArgumentException($"Type {componentType.Name} is not a blazor component.");
        }
    }
}