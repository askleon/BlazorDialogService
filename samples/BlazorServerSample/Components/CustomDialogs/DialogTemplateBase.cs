using BlazorDialogService;
using Microsoft.AspNetCore.Components;

namespace BlazorServerSample.Components.CustomDialogs
{
    public abstract class DialogTemplateBase : ComponentBase
    {
        [CascadingParameter] public Dialog Dialog { get; private set; }
        [Parameter] public virtual string Title { get; set; }
        [Parameter] public bool HideBackdrop { get; set; }
    }
}