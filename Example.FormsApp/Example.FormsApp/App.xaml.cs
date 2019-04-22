namespace Example.FormsApp
{
    using System.Reflection;

    using Example.FormsApp.Modules;

    using Smart.Forms.Resolver;
    using Smart.Navigation;
    using Smart.Resolver;

    using XamarinFormsComponents;
    using XamarinFormsComponents.Popup;

    public partial class App
    {
        private readonly Navigator navigator;

        public App()
        {
            InitializeComponent();

            // Config Resolver
            var resolver = CreateResolver();
            ResolveProvider.Default.UseSmartResolver(resolver);

            // Config Navigator
            navigator = new NavigatorConfig()
                .UseFormsNavigationProvider()
                .UseResolver(resolver)
                .UseIdViewMapper(m => m.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes))
                .ToNavigator();
            navigator.Navigated += (sender, args) =>
            {
                // for debug
                System.Diagnostics.Debug.WriteLine(
                    $"Navigated: [{args.Context.FromId}]->[{args.Context.ToId}] : stacked=[{navigator.StackedCount}]");
            };

            // Popup Navigator
            var popupNavigator = resolver.Get<IPopupNavigator>();
            popupNavigator.AutoRegister(Assembly.GetExecutingAssembly().ExportedTypes);

            // Show MainWindow
            MainPage = resolver.Get<MainPage>();
        }

        private SmartResolver CreateResolver()
        {
            var config = new ResolverConfig()
                .UseAutoBinding()
                .UseArrayBinding()
                .UseAssignableBinding()
                .UsePropertyInjector();

            config.UseXamarinFormsComponents(adapter =>
            {
                adapter.AddDialogs();
                adapter.AddPopupNavigator();
                adapter.AddLocationManager();
                adapter.AddJsonSerializer();
                adapter.AddSettings();
            });

            config.Bind<INavigator>().ToMethod(kernel => navigator).InSingletonScope();

            config.Bind<ApplicationState>().ToSelf().InSingletonScope();

            return config.ToResolver();
        }

        protected override void OnStart()
        {
            navigator.Forward(ViewId.Menu);
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
