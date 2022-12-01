using CinemaWithWPF.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace CinemaWithWPF.UserControls;

public partial class UserControl_Movie : UserControl
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



    public UserControl_Movie(Movie movie)
    {
        InitializeComponent();

        Movie = movie;
        
        DataContext = this;
    }
}
