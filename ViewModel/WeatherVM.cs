using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;
using System.Windows;

namespace WeatherApp.ViewModel
{
    // Nakon sto ukucamo : INotifyPropertyChanged moramo da dodamo using System.ComponentModel
    // Nakon toga na CTRL + . pa kliknemo "implement interface"
    public class WeatherVM : INotifyPropertyChanged
    {
        // Konstruktor
        // if naredba na pocetku sluzi da u XAML-u vidimo kako bi izgledao View KADA aplikacija bude radila
        // kada se aplikacija prvi put pokrene sva polja ce biti prazna
        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "Belgrade"
                };
                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        // porpfull snippet >
        // kucanjem propfull i dva puta pritisom na TAB dugme sam ispisuje kod ispod
        private string query;
        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged("Query");     // pod navodnike se stavlja "Query" jer je to naziv konstruktora
                                                // za druge OnPropertyChanged ce se stavljati drugi nazivi pod ""
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;
        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;
        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                if (selectedCity != null && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditions();
                }
            }
        }

        public SearchCommand SearchCommand { get; set; }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            // slededeci red koda je da kada korisnik unese novi grad da se ocisti postojeca lista
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        // ostaje nam da trigerujemo ovaj event iz neke druge metode
        public event PropertyChangedEventHandler PropertyChanged;

        // sledi maltene default nacin pisanja metode za property change
        // OnPropertyChanged je opste prihvacen naziv za ovu metodu
        private void OnPropertyChanged(string propertyName)
        {
            // upitnik znaci da vidimo da li postoji property changed ( da li postoje subscribe-eri za taj event
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            // Property se menja svaki put kada se pozove njen setter
        }
    }
}


