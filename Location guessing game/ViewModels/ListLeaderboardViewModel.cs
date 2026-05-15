using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LocationGuesser.Models;
using LocationGuesser.Models.Services;

namespace LocationGuesser.ViewModels
{
    public partial class ListLeaderboardViewModel : ObservableObject
    {
        private readonly ScoreService _scoreService;
        private readonly ListService _listService;

        [ObservableProperty]
        private ObservableCollection<UserListScoreDTO> _leaderboard;

        [ObservableProperty]
        private string _listName;

        public ListLeaderboardViewModel(ScoreService scoreService, ListService listService)
        {
            _scoreService = scoreService;
            _listService = listService;
            Leaderboard = new ObservableCollection<UserListScoreDTO>();

            ListName = _listService.SelectedListDto?.Name ?? "Leaderboard";
        }

        public async Task LoadLeaderboardAsync()
        {
            if (_listService.SelectedListDto == null) 
                return;

            var scores = await _scoreService.GetLeaderboardAsync(_listService.SelectedListDto.Id);

            Leaderboard.Clear();
            foreach (var score in scores)
            {
                Leaderboard.Add(score);
            }
        }
    }
}
