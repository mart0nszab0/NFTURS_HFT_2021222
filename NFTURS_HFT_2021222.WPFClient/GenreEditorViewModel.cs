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
    public class GenreEditorViewModel : ObservableRecipient
    {
        public RestCollection<Genre> Genres { get; set; }

        private Genre selectedGenre;

        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {

                //if (value == null) return;

                if (value != null)
                {
                    selectedGenre = new Genre()
                    {
                        Name = value.Name,
                        GenreId = value.GenreId
                    };

                    OnPropertyChanged();
                    (DeleteGenreCommand as RelayCommand).NotifyCanExecuteChanged();


                }
                //SetProperty(ref selectedGenre, value);
            }
        }


        public ICommand CreateGenreCommand { get; set; }
        public ICommand UpdateGenreCommand { get; set; }
        public ICommand DeleteGenreCommand { get; set; }

        public bool DesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public GenreEditorViewModel()
        {
            if (!DesignMode)
            {
                Genres = new RestCollection<Genre>("http://localhost:32095/", "Genre");

                CreateGenreCommand = new RelayCommand(() =>
                {
                    Genres.Add(new Genre() { Name = SelectedGenre.Name });
                });

                UpdateGenreCommand = new RelayCommand(() =>
                {
                    Genres.Update(SelectedGenre);
                });

                DeleteGenreCommand = new RelayCommand(() =>
                {
                    Genres.Delete(SelectedGenre.GenreId);
                },
                () =>
                {
                    return SelectedGenre != null;
                });

                SelectedGenre = new Genre();
            }
        }


    }
}
