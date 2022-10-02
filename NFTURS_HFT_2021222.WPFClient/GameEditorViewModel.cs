using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NFTURS_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NFTURS_HFT_2021222.WPFClient
{
    public class GameEditorViewModel : ObservableRecipient
    {
        public RestCollection<Game> Games { get; set; }

        private Game selectedGame;

        public Game SelectedGame
        {
            get { return selectedGame; }
            set
            {

                //if (value == null) return;
                
                if (value != null)
                {
                    selectedGame = new Game()
                    {
                        Name = value.Name,
                        GameId = value.GameId
                    };

                    OnPropertyChanged();
                    (DeleteGameCommand as RelayCommand).NotifyCanExecuteChanged();

                    
                }
                //SetProperty(ref selectedGame, value);
            }
        }


        public ICommand CreateGameCommand { get; set; }
        public ICommand UpdateGameCommand { get; set; }
        public ICommand DeleteGameCommand { get; set; }

        public bool DesignMode { 
            get 
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }  
        }

        public GameEditorViewModel()
        {
            if (!DesignMode)
            {
                Games = new RestCollection<Game>("http://localhost:32095/", "Game");

                CreateGameCommand = new RelayCommand(() =>
                {
                    Games.Add(new Game() { Name = SelectedGame.Name });
                });

                UpdateGameCommand = new RelayCommand( () =>
                {

                });

                DeleteGameCommand = new RelayCommand(() =>
                {
                    Games.Delete(SelectedGame.GameId);
                },
                () =>
                {
                    return SelectedGame != null;
                });

                SelectedGame = new Game();
            }
        }


    }
}
