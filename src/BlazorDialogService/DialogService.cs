using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorDialogService
{
    public class DialogService
    {
        public event Action Changed;
        private List<DialogModel> dialogModels = new List<DialogModel>();
        public IEnumerable<DialogModel> DialogModels => dialogModels;

        public DialogBuilder<T> BuildDialog<T>()
        {
            return new DialogBuilder<T>(x => ShowDialog<T>(x));
        }

        public Task<DialogResult> ShowDialog<T>()
        {
            return ShowDialog<T>(new DialogParameters<T>());
        }

        public Task<DialogResult> ShowDialog<T>(DialogParametersBase parameters)
        {
            return ShowDialog(typeof(T), parameters);
        }

        public Task<DialogResult> ShowDialog(Type dialogComponentType, DialogParametersBase parameters)
        {
            ThrowIfNotComponent(dialogComponentType);

            var dialog = new DialogModel(dialogComponentType, parameters);
            dialogModels.Add(dialog);

            OnChanged();

            return dialog.Task;
        }

        public void Close(Guid dialogId, DialogResult result)
        {
            var modelIndex = dialogModels.FindIndex(x => x.Id == dialogId);
            var model = dialogModels[modelIndex];
            dialogModels.RemoveAt(modelIndex);
            model.TaskSource.SetResult(result);
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