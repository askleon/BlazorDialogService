# About
- A very generic dialog service for blazor.
- It can take any component, allowing you to choose or style your own dialog markup.
- Dialog markup not included. (Example markup from the sample project: [Link](samples/BlazorServerSample/Pages/CustomDialogs/))

# Installation
- Add package to your project

- Add service to dependencies.
```csharp
// Startup.cs
using Microsoft.Extensions.Hosting;

services.AddBlazorDialogService();
```

- Add namespace to imports.
```csharp
// _Imports.razor
@using BlazorDialogService.DialogComponent
```

- Add dialog container.
```csharp
// Shared/MainLayout.cs

<div class="content px-4">
    <DialogContainer></DialogContainer>
    @Body
</div>
```

# Usage
> For more in-depth examples, see projects in the samples directory

- The dialog service was created for dialogs but can take any blazor component, like in the example below.
- It can also handle component parameters by using `DialogParameter` argument.
   - You can see `DialogParameter` in use here [ConfirmSample.razor](samples/BlazorServerSample/Pages/Samples/ConfirmSample.razor)

```csharp
// BasicSample.razor

@inject DialogService DialogService

<button @onclick="Show">Basic sample</button>

@code {
    async void Show()
    {
        DialogResult result = await DialogService.ShowDialog<HelloWorldComponent>();
    }
}
```

```csharp
// HelloWorldComponent.razor
<h1>Hello world!</h1>
```