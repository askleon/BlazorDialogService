using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorDialogService
{
    public class DialogModel
    {
        public DialogModel(Type componentType, DialogParametersBase parameters)
        {
            Id = Guid.NewGuid();
            ComponentType = componentType;
            Parameters = parameters;
            TaskSource = new TaskCompletionSource<DialogResult>();
        }

        public Guid Id { get; set; }
        public Type ComponentType { get; set; }
        public DialogParametersBase Parameters { get; }
        public TaskCompletionSource<DialogResult> TaskSource { get; }
        public Task<DialogResult> Task => TaskSource.Task;
        public RenderFragment Content => new RenderFragment(x =>
        {
            var seq = 0;
            x.OpenComponent(seq++, ComponentType);
            foreach (var (key, value) in Parameters ?? new Dictionary<string, object>())
            {
                x.AddAttribute(seq++, key, value);
            }
            x.CloseComponent();
        });
    }
}