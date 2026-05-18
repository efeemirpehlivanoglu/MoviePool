using System;

namespace MoviePool.Models
{
    public class Movie
    {
        public Guid Id { get; set; } 
        public string Title { get; set; } 
        public bool IsWatched { get; set; } = false; 
        public DateTime AddedDate { get; set; }

        public Movie(string title)
        {
            Id = Guid.NewGuid(); 
            Title = title;
            AddedDate = DateTime.Now; 
        }
    }
}