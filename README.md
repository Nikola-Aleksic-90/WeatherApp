# WeatherApp

Ovo je prva C# WPF desktop aplikacija koja primenjuje MVVM arhitekturu. Cilj MVVM artitekture je razdvajanje koda (logike) od dizajna (korisničkog interfejsa).
U odnosu na prethodnu WPF aplikaciju implementirane su dodatne XAML funkcije, izvršeno je povezivanje sa API, izvršen je Data Binding (povezivanje podataka iz modela sa korisničkim interfesom). 

Aplikacija je iz Udemy kursa za WPF programiranje. Ispod su navedene stavke koje sam nauči praveći ovu aplikaciju.

XAML View:
Aplikacija služi da korisnik, preko TextBox-a unese naziv grada, izabere grad iz ListBox-a i da se u dnu stranice prikažu trenutne informacije o vremenu

API:
Za dobijanje temperature i padavina korišćen je API od AccuWeather web aplikacije

Model:
Model je automatski generisan preko web aplikacije JSON Utils, a na osnovu JSON koji se dobija od API-ja.

ViewModel:
Da izbegnemo kod u pozadini XAML (View-a) kod je smešten u ViewModel klasu koja je veza između View-a i Model-a. Ovo je osnova arhitekture MVVM.

Interfejsi:
Implementirani su
- INotifyPropertyChanged koji ažurira prikaz u View i ažurira Model
- ICommand koji služi da izbegnemo EventHandler u kodu iza View
- IConvert koji služi da podatak za padavine tipa bool prevedemo u string da li ima ili nema padavina

Pomoćne klase za:
- slanje query ka API AccuWeather sa podacima za uneti grad i API KEY-jem
- deserijalizaciju JSON (razdvajanje dobijenih podataka od API za popunjavanje modela)

ObservableCollection<T>:
Lista koja je spregnuta sa modelom, prati promene, ažurira se i na kraju daje prikaz u View
