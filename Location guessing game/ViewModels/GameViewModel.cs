using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;
using Location_guessing_game.Services;
using Microsoft.Maui.Devices.Sensors;
using System.Threading.Tasks;

namespace Location_guessing_game.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly ListService _listService;
    private int _currentImageIndex = 0;
    private bool _finished = false;

    [ObservableProperty]
    public partial ImageLocation CurrentImage { get; set; }

    [ObservableProperty]
    public partial bool HasPlacedPin { get; set; }

    [ObservableProperty]
    public partial bool IsGuessed { get; set; }

    [ObservableProperty]
    public partial string DistanceMessage { get; set; }

    [ObservableProperty]
    public partial string ScoreMessage { get; set; }

    public GameViewModel(ListService listService)
    {
        _listService = listService;
        StartGame();
    }

    private void StartGame()
    {
        _currentImageIndex = 0;
        LoadImage();
    }

    private void LoadImage()
    {
        if (_currentImageIndex < _listService.SelectedList.Images.Count)
        {
            CurrentImage = _listService.SelectedList.Images[_currentImageIndex];
            HasPlacedPin = false;
            IsGuessed = false;
        }
        else
        { 
            // TODO 
            DistanceMessage = "Koniec hry!";
        }
    }

    public void CalculateScore(double guessLon, double guessLat)
    {
        IsGuessed = true;

        Location guessLocation = new Location(guessLat, guessLon);
        Location realLocation = new Location(CurrentImage.Latitude, CurrentImage.Longitude);

        // Výpočet vzdialenosti v kilometroch
        double distanceKm = Location.CalculateDistance(guessLocation, realLocation, DistanceUnits.Kilometers);

        DistanceMessage = $"{Math.Round(distanceKm)} km";

        // TODO Maybe edit scoring system
        int score = 5000 - (int)(distanceKm * 2);
        if (score < 0) 
            score = 0;
        ScoreMessage = score.ToString();
    }

    public bool TryLoadNextRound()
    {
        ++_currentImageIndex;

        if (_currentImageIndex < _listService.SelectedList.Images.Count)
        {
            LoadImage();
            return true;
        }

        DistanceMessage = "End of game";
        ScoreMessage = "";
        return false;
        

    }

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}