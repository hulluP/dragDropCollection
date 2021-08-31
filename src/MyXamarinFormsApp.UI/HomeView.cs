using System;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using MvvmCross.Binding.BindingContext;
using MyXamarinFormsApp.Core.ViewModels.Home;

namespace MyXamarinFormsApp.UI
{

    public class HomeView : MvxContentPage<HomeViewModel>
    {
        private StackLayout BaseLayout;
        private Button showList;
        private Label InitialCounter;
        private Label Counter;
        private Button navigate;

        public HomeView()
        {
            BaseLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Beige,
            };
            Content = BaseLayout;

        }
        protected override void OnViewModelSet()
        {
            InitializeViewControls();
            base.OnViewModelSet();
            var set = this.CreateBindingSet<HomeView, HomeViewModel>();
            set.Bind(navigate).For("Clicked").To(vm => vm.NavigationCommand);

            set.Apply();
        }
        private void InitializeViewControls()
        {
            showList = new Button()
            {
                Text = "clean Up"
            };
            InitialCounter = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "---"
            };
            Counter = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "---"
            };
            showList.Clicked += ShowList_ClickedAsync;
            BaseLayout.Children.Add(showList);
            BaseLayout.Children.Add(InitialCounter);
            BaseLayout.Children.Add(Counter);
            navigate = new Button()
            {
                Text = "DragEventArgs"
            };

            BaseLayout.Children.Add(navigate);

        }
        private void ShowList_ClickedAsync(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Counter.Text = "Total Memory:" + GC.GetTotalMemory(false);
            if (InitialCounter.Text.Length < 5)
            {
                InitialCounter.Text = "inital Memory:" + GC.GetTotalMemory(false);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Counter.Text = "Total Memory:" + GC.GetTotalMemory(false);
            if (InitialCounter.Text.Length < 5)
            {
                InitialCounter.Text = "inital Memory:" + GC.GetTotalMemory(false);
            }

        }
    }
}
