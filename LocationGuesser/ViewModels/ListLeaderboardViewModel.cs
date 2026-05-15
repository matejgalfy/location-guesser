using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels
{
    public partial class ListLeaderboardViewModel : ObservableObject
    {
        private readonly ScoreService _scoreService;
        private readonly ImageListService _imageListService;

        [ObservableProperty]
        public partial ObservableCollection<UserImageListScoreDTO> Leaderboard { get; set; }

        [ObservableProperty]
        public partial string ListName { get; set; }

        public ListLeaderboardViewModel(ScoreService scoreService, ImageListService imageListService)
        {
            _scoreService = scoreService;
            _imageListService = imageListService;
            Leaderboard = new ObservableCollection<UserImageListScoreDTO>();

            ListName = _imageListService.SelectedListDto?.Name ?? "Leaderboard";
        }

        public async Task LoadLeaderboardAsync()
        {
            if (_imageListService.SelectedListDto == null) 
                return;

            var scores = await _scoreService.GetLeaderboardAsync(_imageListService.SelectedListDto.Id);

            Leaderboard.Clear();
            foreach (var score in scores)
            {
                Leaderboard.Add(score);
            }
        }
    }
}
