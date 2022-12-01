using CinemaWithWPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CinemaWithWPF.Windows;


public partial class MoreInformationAboutTheFilm : Window
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null!)
    {
        PropertyChangedEventHandler handler = PropertyChanged!;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }


    private Movie movie;
    public Movie Movie
    {
        get { return movie; }
        set
        {
            movie = value;
            OnPropertyChanged();
        }
    }


    public MoreInformationAboutTheFilm(Movie movie)
    {
        InitializeComponent();
        Movie = movie;
        DataContext = this;
    }
}