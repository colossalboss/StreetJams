using System;
namespace StreetJamsAPI.ViewModels
{
    public class ArtistViewModel
    {
        public ArtistViewModel()
        {
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public int NumberOfSongs { get; set; }
    }
}
