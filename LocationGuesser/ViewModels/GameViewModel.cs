using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly ImageListService _imageListService;
    private readonly ScoreService _scoreService;
    private readonly UserService _userService;

    private int _currentImageIndex = 0;
    private int _totalScore = 0;

    [ObservableProperty] public partial ImageLocationDTO CurrentImage { get; set; } = new();

    [ObservableProperty]
    public partial bool HasPlacedPin { get; set; }

    [ObservableProperty]
    public partial bool IsGuessed { get; set; }

    [ObservableProperty]
    public partial string DistanceMessage { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ScoreMessage { get; set; } = string.Empty;

    public GameViewModel(ImageListService imageListService, ScoreService scoreService, UserService userService)
    {
        _imageListService = imageListService;
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
        if (_imageListService.SelectedListDto is not null &&
            _currentImageIndex < _imageListService.SelectedListDto.Images.Count)
        {
            CurrentImage = _imageListService.SelectedListDto.Images[_currentImageIndex];
            HasPlacedPin = false;
            IsGuessed = false;
        }
    }

    public void CalculateScore(double guessLon, double guessLat)
    {
        IsGuessed = true;

        var guessLocation = new Location(guessLat, guessLon);
        var realLocation = new Location(CurrentImage.Latitude, CurrentImage.Longitude);

        var distanceKm = Location.CalculateDistance(guessLocation, realLocation, DistanceUnits.Kilometers);

        DistanceMessage = $"{Math.Round(distanceKm)} km";

        var score = 5000 - (int)(distanceKm * 2);
        if (score < 0) 
            score = 0;
        ScoreMessage = score.ToString();
        _totalScore += score;
    }

    public bool TryLoadNextRound()
    {
        ++_currentImageIndex;

        if (_imageListService.SelectedListDto is not null && 
            _currentImageIndex < _imageListService.SelectedListDto.Images.Count)
        {
            LoadImage();
            return true;
        }

        DistanceMessage = "End of game";
        ScoreMessage = _totalScore.ToString();

        if (_imageListService.SelectedListDto is not null && _userService.CurrentUser is not null)
        {
            _ = _scoreService.SaveScoreAsync(new SaveScoreDTO
            {
                ImageListId = _imageListService.SelectedListDto.Id,
                Score = _totalScore,
                UserId = _userService.CurrentUser.Id
            });
        }

        return false;
    }

    [RelayCommand]
    async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}