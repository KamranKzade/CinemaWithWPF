using CinemaWithWPF.Models;
using CinemaWithWPF.UserControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaWithWPF;


public partial class MainWindow : Window
{
    private List<string> _movieDataBase;
    HttpClient httpClient = new HttpClient();
    string jsonStr;



    public MainWindow()
    {
        InitializeComponent();
        _movieDataBase = new();

        DataContext = this;

        _movieDataBase = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("../../../DataBase/MovieDataBase.json"))!;
    }


    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {


        foreach (var m in _movieDataBase)
        {
            jsonStr = await httpClient.GetStringAsync($"http://www.omdbapi.com/?apikey=82bcd4c7&t={m}&plot=full");

            if (!jsonStr.Contains("Error"))
            {
                var movie = JsonSerializer.Deserialize<Movie>(jsonStr);
                wrapPanel.Children.Add(new UserControl_Movie(movie!));
            }
        }
    }

    private void TextBox_Search_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            Seartch_Film();
        }
    }


    private async void Button_Click(object sender, RoutedEventArgs e) => await Seartch_Film();





    private void TextBox_MouseEnter(object sender, MouseEventArgs e) => TextBox_Search.Text = string.Empty;

    private async Task Seartch_Film()
    {
        if (string.IsNullOrWhiteSpace(TextBox_Search.Text))
        {
            MessageBox.Show("Please Enter Movie Name", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        jsonStr = await httpClient.GetStringAsync($@"http://www.omdbapi.com/?apikey=82bcd4c7&t={TextBox_Search.Text}");

        if (!jsonStr.Contains("Error"))
        {
            var movie = JsonSerializer.Deserialize<Movie>(jsonStr);
            wrapPanel.Children.Add(new UserControl_Movie(movie!));


            _movieDataBase.Add(movie?.Title!);
            string str = JsonSerializer.Serialize(_movieDataBase);
            File.WriteAllText("../../../DataBase/MovieDataBase.json", str);
        }
        else
            MessageBox.Show("No Result Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }

}