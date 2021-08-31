using System;
using MvvmCross.Base;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using MvvmCross.Binding.BindingContext;
using Xamarin.Forms;
using MyXamarinFormsApp.Core.ViewModels.Home;

namespace MyDragFormsApp.UI
{
    public class DragDrop : MvxContentPage<DragDropModel>
    {
        private Grid CoreMatrixLayout;
        //private CustomMatrixGlyph DaMatrix;
        private CollectionView DropZone;
        private CollectionView dragZone2;
        private DropGestureRecognizer dropRecognizer;
        private Button resetButton;

        private void OnInteractionRequested(object sender, MvxValueEventArgs<ViewCommand> refreshCommand)
        {
            //testImage.relo

        }
        public DragDrop()
        {
            CoreMatrixLayout = new Grid()
            {
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 0,
                ColumnSpacing = 0
            };
            //DaGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            CoreMatrixLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CoreMatrixLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CoreMatrixLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            CoreMatrixLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(75, GridUnitType.Absolute) });
            CoreMatrixLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(75, GridUnitType.Absolute) });
            CoreMatrixLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(75, GridUnitType.Absolute) });


            //CoreMatrixLayout.Children.Add(DaTitleBar, 0, 3, 0, 1);

            DropZone = new CollectionView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                SelectionMode = SelectionMode.Single,
                BackgroundColor = Color.Blue,
                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
                {
                    ItemSpacing = 5,
                },
                ItemTemplate = WordTemplate(),
                Margin = new Thickness(5)
                //ItemTemplate = WordTemplate(),
            };
            dropRecognizer = new DropGestureRecognizer()
            {
                AllowDrop = true
            };
            dropRecognizer.DragOver += DropGesture_DragOver;

            //dropRecognizer.SetBinding(DragGestureRecognizer.DropCompletedCommandProperty, nameof(DragDropModel.DropOverCommand));

            DropZone.GestureRecognizers.Add(dropRecognizer);
            CoreMatrixLayout.Children.Add(DropZone, 0, 3, 1, 2);
            dragZone2 = new CollectionView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                SelectionMode = SelectionMode.Single,
                BackgroundColor = Color.LimeGreen,
                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
                {
                    ItemSpacing = 5,
                },
                ItemTemplate = WordTemplate(),
                Margin = new Thickness(5)
                //ItemTemplate = WordTemplate(),
            };

            CoreMatrixLayout.Children.Add(dragZone2, 0, 3, 2, 3);

            resetButton = new Button
            {
                Text = "reset",
                BackgroundColor = Color.DarkGray
            };
            CoreMatrixLayout.Children.Add(resetButton, 1, 2, 0, 1);
            resetButton.GestureRecognizers.Add(dropRecognizer);
            Content = CoreMatrixLayout;
        }

        private void DropGesture_DragOver(object sender, DragEventArgs e)
        {
            return;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }


        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            var set = this.CreateBindingSet<DragDrop, DragDropModel>();

            set.Bind(dropRecognizer).For(v => v.DropCommand).To(vm => vm.DropOverCommand).TwoWay();
            //set.Bind(dragZone1).For(v => v.ItemsSource).To(vm => vm.Words).OneWay();
            set.Bind(dragZone2).For(v => v.ItemsSource).To(vm => vm.Words).OneWay();
            set.Bind(DropZone).For(v => v.ItemsSource).To(vm => vm.NewWords).OneWay();
            set.Bind(resetButton).For("ItemClick").To(vm => vm.ResetCommand).OneWayToSource();

            set.Apply();

        }
        public static DataTemplate WordTemplate()
        {
            return new DataTemplate(() =>
            {


                //cardTextStack.Children.Add(challangeHint);
                var wordLabel = new Label()
                {
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center
                };
                wordLabel.SetBinding(Label.TextProperty, nameof(WordTile.Title), BindingMode.OneWay, null);
                var frame = new Frame
                {
                    BorderColor = Color.Chocolate,
                    BackgroundColor = Color.LightGray,
                    HasShadow = false,
                    HorizontalOptions = LayoutOptions.Start,
                    //VerticalOptions = LayoutOptions.FillAndExpand,
                    Content = wordLabel,
                    //Margin = new Thickness(EltepiGlobal.PADDING_NORMAL),
                    CornerRadius = 12
                };


                //set.Bind(dragZone2).For(v => v.ItemsSource).To(vm => vm.Words).OneWay();
                var dragRecognizer = new DragGestureRecognizer()
                {
                    //CanDrag = true
                };
                dragRecognizer.SetBinding(DragGestureRecognizer.DragStartingCommandParameterProperty, new Binding("."));
                dragRecognizer.SetBinding(DragGestureRecognizer.DragStartingCommandProperty, nameof(WordTile.DragCommand), BindingMode.TwoWay, null);
                dragRecognizer.SetBinding(DragGestureRecognizer.DropCompletedCommandParameterProperty, new Binding("."));
                dragRecognizer.SetBinding(DragGestureRecognizer.DropCompletedCommandProperty, nameof(WordTile.DropCommand), BindingMode.TwoWay, null);
                dragRecognizer.SetBinding(DragGestureRecognizer.CanDragProperty, nameof(WordTile.Dragable), BindingMode.OneWay, null);

                //dragRecognizer.DragStarting += (_, args) =>
                //{
                //    DraggingColor = Color.Purple;
                //    OnPropertyChanged(nameof(DraggingColor));

                //};


                frame.GestureRecognizers.Add(dragRecognizer);

                return frame;
            });
        }


    }
    public class ViewCommand
    {
        public enum RefreshCommand
        {
            Empty = 0,
            Refresh = 1,
            ShowCourses = 2,
            ShowGoals = 4,
            ShowSustainSessions = 8,
            ShowBlankHomeScreen = 16,
            ShowUnits = 32,
            ShowCards = 64,
            ShowProgress = 128,
            ShowQuiz = 256,
            ShowSearchResult = 512,
            ShowSearchBar = 1024,
            ShowFlags = 2048,
            ShowAdvancedControls = 4096,
            ShowChildren = 8192,
            StopTimer = 16384,
            ShowLogon = 32768,
            StartTimer = 65536

        }
        public Action OnRefresh { get; set; }
        public RefreshCommand RefreshState;
        public int CurrentIndex;
    }
}
