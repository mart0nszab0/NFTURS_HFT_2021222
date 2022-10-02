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
    public class PublisherEditorViewModel : ObservableRecipient
    {
        public RestCollection<Publisher> Publishers { get; set; }

        private Publisher selectedPublisher;

        public Publisher SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                if (value == null) return; //null check

                selectedPublisher = new Publisher()
                {
                    Name = value.Name,
                    PublisherId = value.PublisherId
                };

                OnPropertyChanged();
                (DeletePublisherCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreatePublisherCommand { get; set; }
        public ICommand UpdatePublisherCommand { get; set; }
        public ICommand DeletePublisherCommand { get; set; }

        public bool DesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public PublisherEditorViewModel()
        {
            if (!DesignMode)
            {
                Publishers = new RestCollection<Publisher>("http://localhost:32095/", "Publisher", "hub");

                CreatePublisherCommand = new RelayCommand(() =>
                {
                    Publishers.Add(new Publisher() { Name = SelectedPublisher.Name });
                });

                UpdatePublisherCommand = new RelayCommand(() =>
                {
                    Publishers.Update(SelectedPublisher);
                });

                DeletePublisherCommand = new RelayCommand(() =>
                {
                    Publishers.Delete(SelectedPublisher.PublisherId);
                },
                () =>
                {
                    return SelectedPublisher != null;
                });

                SelectedPublisher = new Publisher();
            }
        }


    }
}
