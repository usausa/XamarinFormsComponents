namespace XamarinFormsComponents.Popup
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;

    using Xamarin.Forms;

    public sealed class PopupNavigator : IPopupNavigator
    {
        private readonly IActivator activator;

        private readonly Dictionary<object, Type> popupTypes = new Dictionary<object, Type>();

        public PopupNavigator(IActivator activator)
        {
            this.activator = activator;
        }

        public void Register(object id, Type type)
        {
            popupTypes[id] = type;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async ValueTask<TResult> PopupAsync<TResult>(object id)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)activator.Create(type);

            if (content.BindingContext is IPopupNavigatorAware aware)
            {
                aware.PopupNavigator = this;
            }

            var popup = new PopupPage
            {
                Content = content,
                CloseWhenBackgroundIsClicked = false,
                HasSystemPadding = true,
                Padding = PopupProperty.GetThickness(content)
            };

            var cts = new TaskCompletionSource<TResult>();
            popup.Disappearing += (sender, args) =>
            {
                if (((PopupPage)sender).Content.BindingContext is IPopupResult<TResult> result)
                {
                    cts.SetResult(result.Result);
                }
                else
                {
                    cts.SetResult(default);
                }
            };

            await PopupNavigation.Instance.PushAsync(popup, false);

            return await cts.Task;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async ValueTask<TResult> PopupAsync<TParameter, TResult>(object id, TParameter parameter)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)activator.Create(type);

            if (content.BindingContext is IPopupNavigatorAware aware)
            {
                aware.PopupNavigator = this;
            }

            if (content.BindingContext is IPopupInitialize<TParameter> initialize)
            {
                initialize.Initialize(parameter);
            }

            if (content.BindingContext is IPopupInitializeAsync<TParameter> initializeAsync)
            {
                await initializeAsync.Initialize(parameter);
            }

            var popup = new PopupPage
            {
                Content = content,
                CloseWhenBackgroundIsClicked = false,
                HasSystemPadding = true,
                Padding = PopupProperty.GetThickness(content)
            };

            var cts = new TaskCompletionSource<TResult>();
            popup.Disappearing += (sender, args) =>
            {
                if (((PopupPage)sender).Content.BindingContext is IPopupResult<TResult> result)
                {
                    cts.SetResult(result.Result);
                }
                else
                {
                    cts.SetResult(default);
                }
            };

            await PopupNavigation.Instance.PushAsync(popup, false);

            return await cts.Task;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async ValueTask PopupAsync(object id)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)activator.Create(type);

            if (content.BindingContext is IPopupNavigatorAware aware)
            {
                aware.PopupNavigator = this;
            }

            var popup = new PopupPage
            {
                Content = content,
                CloseWhenBackgroundIsClicked = false,
                HasSystemPadding = true,
                Padding = PopupProperty.GetThickness(content)
            };

            var cts = new TaskCompletionSource<object>();
            popup.Disappearing += (sender, args) =>
            {
                cts.SetResult(default);
            };

            await PopupNavigation.Instance.PushAsync(popup, false);

            await cts.Task;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async ValueTask PopupAsync<TParameter>(object id, TParameter parameter)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)activator.Create(type);

            if (content.BindingContext is IPopupNavigatorAware aware)
            {
                aware.PopupNavigator = this;
            }

            if (content.BindingContext is IPopupInitialize<TParameter> initialize)
            {
                initialize.Initialize(parameter);
            }

            if (content.BindingContext is IPopupInitializeAsync<TParameter> initializeAsync)
            {
                await initializeAsync.Initialize(parameter);
            }

            var popup = new PopupPage
            {
                Content = content,
                CloseWhenBackgroundIsClicked = false,
                HasSystemPadding = true,
                Padding = PopupProperty.GetThickness(content)
            };

            var cts = new TaskCompletionSource<object>();
            popup.Disappearing += (sender, args) =>
            {
                cts.SetResult(default);
            };

            await PopupNavigation.Instance.PushAsync(popup, false);

            await cts.Task;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2007:DoNotDirectlyAwaitATask", Justification = "Ignore")]
        public async ValueTask PopAsync()
        {
            await PopupNavigation.Instance.PopAsync(false);
        }
    }
}
