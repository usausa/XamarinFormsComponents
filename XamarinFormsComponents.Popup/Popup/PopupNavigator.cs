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
        private readonly IServiceProvider serviceProvider;

        private readonly IPopupPageFactory pageFactory;

        private readonly Dictionary<object, Type> popupTypes = new();

        public PopupNavigator(IServiceProvider serviceProvider, IPopupPageFactory pageFactory)
        {
            this.serviceProvider = serviceProvider;
            this.pageFactory = pageFactory;
        }

        public void Register(object id, Type type)
        {
            popupTypes[id] = type;
        }

        public async ValueTask<TResult> PopupAsync<TResult>(object id)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)serviceProvider.GetService(type);

            if (content.BindingContext is IPopupNavigatorAware aware)
            {
                aware.PopupNavigator = this;
            }

            var popup = pageFactory.Create(content);

            var cts = new TaskCompletionSource<TResult>();
            popup.Disappearing += (sender, _) =>
            {
                var page = (PopupPage)sender;

                if (((PopupPage)sender).Content.BindingContext is IPopupResult<TResult> result)
                {
                    cts.SetResult(result.Result);
                }
                else
                {
                    cts.SetResult(default!);
                }

                Cleanup(page);
                (sender as IDisposable)?.Dispose();
                (page.BindingContext as IDisposable)?.Dispose();
                page.BindingContext = null;
            };

            await PopupNavigation.Instance.PushAsync(popup, false).ConfigureAwait(false);

            return await cts.Task.ConfigureAwait(false);
        }

        public async ValueTask<TResult> PopupAsync<TParameter, TResult>(object id, TParameter parameter)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)serviceProvider.GetService(type);

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
                await initializeAsync.Initialize(parameter).ConfigureAwait(false);
            }

            var popup = pageFactory.Create(content);

            var cts = new TaskCompletionSource<TResult>();
            popup.Disappearing += (sender, _) =>
            {
                var page = (PopupPage)sender;

                if (page.Content.BindingContext is IPopupResult<TResult> result)
                {
                    cts.SetResult(result.Result);
                }
                else
                {
                    cts.SetResult(default!);
                }

                Cleanup(page);
                (sender as IDisposable)?.Dispose();
                (page.BindingContext as IDisposable)?.Dispose();
                page.BindingContext = null;
            };

            await PopupNavigation.Instance.PushAsync(popup, false).ConfigureAwait(false);

            return await cts.Task.ConfigureAwait(false);
        }

        public async ValueTask PopupAsync(object id)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)serviceProvider.GetService(type);

            if (content.BindingContext is IPopupNavigatorAware aware)
            {
                aware.PopupNavigator = this;
            }

            var popup = pageFactory.Create(content);

            var cts = new TaskCompletionSource<object>();
            popup.Disappearing += (sender, _) =>
            {
                var page = (PopupPage)sender;

                cts.SetResult(default!);

                Cleanup(page);
                (sender as IDisposable)?.Dispose();
                (page.BindingContext as IDisposable)?.Dispose();
                page.BindingContext = null;
            };

            await PopupNavigation.Instance.PushAsync(popup, false).ConfigureAwait(false);

            await cts.Task.ConfigureAwait(false);
        }

        public async ValueTask PopupAsync<TParameter>(object id, TParameter parameter)
        {
            if (!popupTypes.TryGetValue(id, out var type))
            {
                throw new ArgumentException($"Invalid id=[{id}]", nameof(id));
            }

            var content = (View)serviceProvider.GetService(type);

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
                await initializeAsync.Initialize(parameter).ConfigureAwait(false);
            }

            var popup = pageFactory.Create(content);

            var cts = new TaskCompletionSource<object>();
            popup.Disappearing += (sender, _) =>
            {
                var page = (PopupPage)sender;

                cts.SetResult(default!);

                Cleanup(page);
                (sender as IDisposable)?.Dispose();
                (page.BindingContext as IDisposable)?.Dispose();
                page.BindingContext = null;
            };

            await PopupNavigation.Instance.PushAsync(popup, false).ConfigureAwait(false);

            await cts.Task.ConfigureAwait(false);
        }

        public async ValueTask PopAsync()
        {
            await PopupNavigation.Instance.PopAsync(false).ConfigureAwait(false);
        }

        private static void Cleanup(Element parent)
        {
            if (parent is VisualElement visualElement)
            {
                visualElement.Behaviors.Clear();
                visualElement.Triggers.Clear();
            }

            foreach (var child in parent.LogicalChildren)
            {
                Cleanup(child);
            }
        }
    }
}
