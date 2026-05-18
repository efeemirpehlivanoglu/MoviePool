using System;
using System.Collections.Generic;
using System.Linq;
using System.IO; // Dosya okuma/yazma işlemleri için gerekli
using System.Text.Json; // JSON formatına çevirme işlemleri için gerekli
using MoviePool.Models;

namespace MoviePool.Services
{
    public class MovieManager
    {
        private List<Movie> _movies;
        // Verilerimizin kaydedileceği dosyanın adı
        private const string FilePath = "movies.json"; 

        public MovieManager()
        {
            // Proje başladığında direkt boş liste oluşturmak yerine, önce dosyayı kontrol ediyoruz.
            LoadFromFile(); 
        }

        public void AddMovie(string title)
        {
            var newMovie = new Movie(title);
            _movies.Add(newMovie);
            SaveToFile(); // Listeye yeni film eklendiğinde dosyayı güncelle
            Console.WriteLine($"\n[+] '{title}' havuza başarıyla eklendi! (Durum: İzlenmedi)");
        }

        public Movie GetRandomUnwatchedMovie()
        {
            var unwatchedMovies = _movies.Where(m => !m.IsWatched).ToList();

            if (unwatchedMovies.Count == 0)
            {
                return null;
            }

            Random rnd = new Random();
            int randomIndex = rnd.Next(unwatchedMovies.Count);
            
            return unwatchedMovies[randomIndex];
        }

        public void MarkMovieAsWatched(Movie movie)
        {
            movie.IsWatched = true;
            SaveToFile(); // Film izlendi olarak güncellendiğinde dosyayı güncelle
            Console.WriteLine($"\n[v] '{movie.Title}' izlendi olarak işaretlendi.");
        }

        // --- ARKA PLAN İŞLEMLERİ (Kullanıcının görmediği, sistemi ayakta tutan metotlar) ---

        private void SaveToFile()
        {
            // Listemizi JSON formatında bir metne çeviriyoruz (WriteIndented = true ile okunaklı yapıyoruz)
            string jsonString = JsonSerializer.Serialize(_movies, new JsonSerializerOptions { WriteIndented = true });
            
            // Çevirdiğimiz bu metni "movies.json" dosyasına yazdırıyoruz
            File.WriteAllText(FilePath, jsonString);
        }

        private void LoadFromFile()
        {
            // Eğer daha önceden oluşturulmuş bir "movies.json" dosyası varsa
            if (File.Exists(FilePath))
            {
                string jsonString = File.ReadAllText(FilePath);
                // Dosyadaki metni tekrar List<Movie> objesine çeviriyoruz
                _movies = JsonSerializer.Deserialize<List<Movie>>(jsonString) ?? new List<Movie>();
            }
            else
            {
                // Dosya yoksa (program ilk defa çalışıyorsa) boş bir liste oluştur
                _movies = new List<Movie>();
            }
        }
    }
}