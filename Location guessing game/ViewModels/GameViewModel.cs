using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Location_guessing_game.Models;
using Microsoft.Maui.Devices.Sensors;
using System.Threading.Tasks;
using Location_guessing_game.Models.Services;

namespace Location_guessing_game.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly ListService _listService;
    private readonly ScoreService _scoreService;
    private readonly UserService _userService;

    private int _currentImageIndex = 0;
    private bool _finished = false;
    private int _totalScore = 0;

    [ObservableProperty]
    public partial ImageLocationDTO CurrentImage { get; set; }

    [ObservableProperty]
    public partial bool HasPlacedPin { get; set; }

    [ObservableProperty]
    public partial bool IsGuessed { get; set; }

    [ObservableProperty]
    public partial string DistanceMessage { get; set; }

    [ObservableProperty]
    public partial string ScoreMessage { get; set; }

    public GameViewModel(ListService listService, ScoreService scoreService, UserService userService)
    {
        _listService = listService;
        _scoreService = scoreService;
        _userService = userService;
        StartGame();
    }

    private void StartGame()
    {
        _currentImageIndex = 0;
        LoadImage();
    }

    private void LoadImage()
    {
        if (_currentImageIndex < _listService.SelectedListDto.Images.Count)
        {
            CurrentImage = _listService.SelectedListDto.Images[_currentImageIndex];
            HasPlacedPin = false;
            IsGuessed = false;
        }
    }

    public void CalculateScore(double guessLon, double guessLat)
    {
        IsGuessed = true;

        Location guessLocation = new Location(guessLat, guessLon);
        Location realLocation = new Location(CurrentImage.Latitude, CurrentImage.Longitude);

        double distanceKm = Location.CalculateDistance(guessLocation, realLocation, DistanceUnits.Kilometers);

        DistanceMessage = $"{Math.Round(distanceKm)} km";

        int score = 5000 - (int)(distanceKm * 2);
        if (score < 0) 
            score = 0;
        ScoreMessage = score.ToString();
        _totalScore += score;
    }

    public bool TryLoadNextRound()
    {
        ++_currentImageIndex;

        if (_currentImageIndex < _listService.SelectedListDto.Images.Count)
        {
            LoadImage();
            return true;
        }

        DistanceMessage = "End of game";
        ScoreMessage = _totalScore.ToString();
        _scoreService.SaveScoreAsync(new SaveScoreDTO()
        {
            ImageListId = _listService.SelectedListDto.Id,
            Score = _totalScore,
            UserId = _userService.CurrentUser.Id
        });
        return false;
    }

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}