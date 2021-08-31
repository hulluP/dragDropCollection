using System;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace MyXamarinFormsApp.Core.ViewModels.Home
{
    public class DragDropModel : BaseViewModel
    {

        public MvxCommand<WordTile> DragStartingCommand { get; private set; }
        public MvxCommand ResetCommand { get; private set; }


        public ICommand DropOverCommand => new Command(() =>
        {
            if (Words.Contains(_dragEvent))
            {
                var newWord = new WordTile(_dragEvent.Title, null, null, _dragEvent.Color, false);
                NewWords.Add(newWord);
                Words.Remove(_dragEvent);
            }
        });


        protected MvxObservableCollection<WordTile> myWords;
        public MvxObservableCollection<WordTile> Words
        {
            get => myWords;
            set
            {
                myWords = value;
                RaisePropertyChanged(() => Words);
            }
        }
        protected MvxObservableCollection<WordTile> myNewWords;
        public MvxObservableCollection<WordTile> NewWords
        {
            get => myNewWords;
            set
            {
                myNewWords = value;
                RaisePropertyChanged(() => NewWords);
            }
        }

        private WordTile _dragEvent;
        public DragDropModel()
        {
            Words = new MvxObservableCollection<WordTile>();
            NewWords = new MvxObservableCollection<WordTile>();
            DragStartingCommand = new MvxCommand<WordTile>(DragStartedFunction);
            ResetCommand = new MvxCommand(ResetCommandFunction);
            ResetCommandFunction();

        }

        private void ResetCommandFunction()
        {
            Words.Clear();
            NewWords.Clear();
            for (int i = 0; i < 2; i++)
            {
                Words.Add(new WordTile("g" + i, DragStartingCommand, DropOverCommand, Color.OrangeRed));
                Words.Add(new WordTile("o" + i, DragStartingCommand, DropOverCommand, Color.OrangeRed));
                Words.Add(new WordTile("a" + i, DragStartingCommand, DropOverCommand, Color.OrangeRed));
                Words.Add(new WordTile("t" + i, DragStartingCommand, DropOverCommand, Color.OrangeRed));
                Words.Add(new WordTile("Watch ", DragStartingCommand, DropOverCommand, Color.LightSkyBlue));
                Words.Add(new WordTile("a", DragStartingCommand, DropOverCommand, Color.LightSkyBlue));
                Words.Add(new WordTile("movie", DragStartingCommand, DropOverCommand, Color.LightSkyBlue));

            }
        }

        private void DragStartedFunction(WordTile param)
        {
            _dragEvent = param;
        }

        public override void Prepare()
        {

        }
    }
    public class WordTile
    {
        public WordTile(string title, ICommand dragCommand, ICommand dropCommand, Color color, bool dragable = true)
        {
            Title = title;
            DragCommand = dragCommand;
            Color = color;
            Dragable = dragable;
        }
        private ICommand myDragCommand;
        public ICommand DragCommand
        {
            get => myDragCommand;
            set
            {
                if (value != myDragCommand)
                {
                    myDragCommand = value;
                }
            }
        }
        private ICommand myDropCommand;
        public ICommand DropCommand
        {
            get => myDropCommand;
            set
            {
                if (value != myDropCommand)
                {
                    DropCommand = value;
                }
            }
        }
        public string Title { get; set; }
        public Color Color { get; }
        public bool Dragable { get; }
    }

}
