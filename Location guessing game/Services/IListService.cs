using System;
using System.Collections.Generic;
using System.Text;
using Location_guessing_game.Models;

namespace Location_guessing_game.Services
{
    public interface IListService
    {
        Task<List<ImageList>> GetAvailableListsAsync();
        ImageList ActiveSet { get; set; }
    }
}
