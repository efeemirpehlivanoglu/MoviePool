using System;
using MoviePool.Models;
using MoviePool.Services;

// Menü döngüsünü kontrol etmek için bir değişken
bool isRunning = true;
MovieManager manager = new MovieManager(); // Havuz yöneticimizi çağırdık

while (isRunning)
{
    Console.Clear(); // Her menüye döndüğümüzde ekranı temizler, derli toplu durur.
    Console.WriteLine("🍿 === FİLM HAVUZUM === 🎬");
    Console.WriteLine("1. Havuza Yeni Film Ekle");
    Console.WriteLine("2. Rastgele Film Öner (Ne İzlesem?)");
    Console.WriteLine("3. Çıkış");
    Console.WriteLine("===========================");
    Console.Write("Seçiminiz (1-3): ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("\nEklenecek filmin adı: ");
            string title = Console.ReadLine();
            
            // Kullanıcı boşluk girip enter'a basarsa diye küçük bir güvenlik önlemi
            if (!string.IsNullOrWhiteSpace(title)) 
            {
                manager.AddMovie(title);
            }
            else
            {
                Console.WriteLine("\n[!] Film adı boş olamaz!");
            }
            break;

        case "2":
            var movie = manager.GetRandomUnwatchedMovie();
            
            if (movie == null)
            {
                Console.WriteLine("\n[!] Havuzda izlenmemiş film kalmadı! Lütfen yeni film ekleyin.");
            }
            else
            {
                // İşte uygulamanın en keyifli kısmı!
                Console.WriteLine($"\n🎉 BU AKŞAMKİ FİLMİNİZ: >> {movie.Title} << 🍿");
                Console.Write("Bu filmi izlendi olarak işaretleyip havuzdan kaldırmak ister misin? (E/H): ");
                
                string answer = Console.ReadLine();
                if (answer?.ToLower() == "e")
                {
                    manager.MarkMovieAsWatched(movie);
                }
                else
                {
                    Console.WriteLine("\n[+] Film havuza geri bırakıldı, daha sonra tekrar karşına çıkabilir.");
                }
            }
            break;

        case "3":
            isRunning = false; // Döngüyü kırar ve uygulamayı kapatır
            Console.WriteLine("\nUygulamadan çıkılıyor. İyi seyirler! 🎬");
            break;

        default:
            Console.WriteLine("\n[!] Lütfen menüden 1, 2 veya 3 numaralı seçenekleri giriniz.");
            break;
    }

    // Uygulama kapanmıyorsa kullanıcıdan bir tuşa basmasını bekle
    if (isRunning)
    {
        Console.WriteLine("\nMenüye dönmek için klavyeden herhangi bir tuşa basın...");
        Console.ReadKey();
    }
}