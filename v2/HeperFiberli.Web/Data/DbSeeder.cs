using HeperFiberli.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HeperFiberli.Web.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new()
                {
                    Name = "Yol Aydınlatma",
                    Slug = "yol-aydinlatma",
                    Description = "Enerji verimli yol ve cadde armatürleri ile güvenli, homojen aydınlatma.",
                    IsActive = true
                },
                new()
                {
                    Name = "Mimari Aydınlatma",
                    Slug = "mimari-aydinlatma",
                    Description = "Cephe ve mimari detayları öne çıkaran dinamik ışık çözümleri.",
                    IsActive = true
                },
                new()
                {
                    Name = "Endüstriyel Aydınlatma",
                    Slug = "endustriyel-aydinlatma",
                    Description = "Zorlu saha koşullarına uygun, uzun ömürlü endüstriyel armatürler.",
                    IsActive = true
                },
                new()
                {
                    Name = "Peyzaj / Kentsel Alan",
                    Slug = "peyzaj-kentsel-alan",
                    Description = "Park, meydan ve kıyı hatları için kontrastı yüksek, yumuşak ışık.",
                    IsActive = true
                }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.Products.AnyAsync())
        {
            var categories = await context.Categories.OrderBy(c => c.Id).ToListAsync();
            var products = new List<Product>
            {
                new()
                {
                    CategoryId = categories[0].Id,
                    Name = "Orion Yol Armatürü",
                    Code = "HF-OR-070",
                    ShortDescription = "Dar açılı optik, düşük enerji tüketimi.",
                    Description = "Orion yol armatürü, şehir içi arterlerde homojen aydınlatma sağlarken adaptif dimmer desteği sunar.",
                    Watt = 70,
                    Lumen = 9600,
                    IsActive = true
                },
                new()
                {
                    CategoryId = categories[0].Id,
                    Name = "Vega Cadde",
                    Code = "HF-VE-120",
                    ShortDescription = "Yüksek lümen, düşük bakım maliyeti.",
                    Description = "Vega, asimetrik optik yapısı ve akıllı kontrol seçenekleriyle şehir caddelerinde konforlu ışık sağlar.",
                    Watt = 120,
                    Lumen = 16800,
                    IsActive = true
                },
                new()
                {
                    CategoryId = categories[1].Id,
                    Name = "Aura Cephe Linear",
                    Code = "HF-AU-024",
                    ShortDescription = "Renk değiştiren, ince gövdeli linear.",
                    Description = "Aura linear armatür, bina cephelerinde çizgisel vurgular için RGBW ve DMX kontrol desteği sunar.",
                    Watt = 24,
                    Lumen = 2400,
                    IsActive = true
                },
                new()
                {
                    CategoryId = categories[1].Id,
                    Name = "Monument Washer",
                    Code = "HF-MW-080",
                    ShortDescription = "Geniş açılı projektör.",
                    Description = "Monument Washer, tarihi yapılar ve kulelerde yüksek renksel doğrulukla yüzey aydınlatması sağlar.",
                    Watt = 80,
                    Lumen = 9600,
                    IsActive = true
                },
                new()
                {
                    CategoryId = categories[2].Id,
                    Name = "Forge High-Bay",
                    Code = "HF-FH-150",
                    ShortDescription = "Depo ve üretim tesisleri için yüksek tavan çözümü.",
                    Description = "Forge, titreşimsiz ışık ve yüksek verimlilikle endüstriyel alanlarda konforlu çalışma ortamı sağlar.",
                    Watt = 150,
                    Lumen = 22500,
                    IsActive = true
                },
                new()
                {
                    CategoryId = categories[2].Id,
                    Name = "Titan Exproof",
                    Code = "HF-TI-090",
                    ShortDescription = "ATEX uyumlu exproof gövde.",
                    Description = "Titan, petrokimya ve ağır sanayi tesisleri için yüksek güvenlikli, patlamaya dayanıklı ışık sunar.",
                    Watt = 90,
                    Lumen = 12600,
                    IsActive = true
                },
                new()
                {
                    CategoryId = categories[3].Id,
                    Name = "Luna Bollard",
                    Code = "HF-LB-018",
                    ShortDescription = "Yumuşak ışık dağılımlı bollard.",
                    Description = "Luna bollard, park yollarında kamaşmayı azaltan optik yapısıyla konforlu yürüyüş deneyimi sağlar.",
                    Watt = 18,
                    Lumen = 1600,
                    IsActive = true
                },
                new()
                {
                    CategoryId = categories[3].Id,
                    Name = "Coastline Flood",
                    Code = "HF-CF-060",
                    ShortDescription = "Kıyı koruma kaplamalı projektör.",
                    Description = "Coastline Flood, deniz kenarı projelerinde tuzlu ortama dayanıklı gövde ve optik seçenekleriyle öne çıkar.",
                    Watt = 60,
                    Lumen = 7800,
                    IsActive = true
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

        if (!await context.Projects.AnyAsync())
        {
            var projects = new List<Project>
            {
                new()
                {
                    Name = "Ankara Kuzey Çevre Yolu",
                    City = "Ankara",
                    Country = "Türkiye",
                    CustomerName = "Karayolları",
                    Description = "Kritik kavşak ve viyadüklerde enerji verimli yol aydınlatma modernizasyonu.",
                    CompletionYear = 2023,
                    IsActive = true
                },
                new()
                {
                    Name = "İzmir Pasaport Sahil",
                    City = "İzmir",
                    Country = "Türkiye",
                    CustomerName = "İzmir Büyükşehir Belediyesi",
                    Description = "Kıyı bandında peyzaj ve yaya aksı için bollard ve projektör çözümleri.",
                    CompletionYear = 2022,
                    IsActive = true
                },
                new()
                {
                    Name = "Doha Finans Merkezi Cephe",
                    City = "Doha",
                    Country = "Katar",
                    CustomerName = "Al Noor Group",
                    Description = "60 katlı ofis kulesinde dinamik RGBW cephe aydınlatması ve kontrol sistemi.",
                    CompletionYear = 2024,
                    IsActive = true
                },
                new()
                {
                    Name = "Bursa Organize Sanayi",
                    City = "Bursa",
                    Country = "Türkiye",
                    CustomerName = "OSB Yönetimi",
                    Description = "Üretim tesislerinde yüksek tavan LED dönüşümü ile %40 enerji tasarrufu.",
                    CompletionYear = 2021,
                    IsActive = true
                },
                new()
                {
                    Name = "Tallinn Liman Park",
                    City = "Tallinn",
                    Country = "Estonya",
                    CustomerName = "City of Tallinn",
                    Description = "Limana komşu park alanında peyzaj aydınlatması ve yaya güvenliği iyileştirmesi.",
                    CompletionYear = 2023,
                    IsActive = true
                },
                new()
                {
                    Name = "Sarajevo Tarihi Çarşı",
                    City = "Sarajevo",
                    Country = "Bosna Hersek",
                    CustomerName = "Şehir Belediyesi",
                    Description = "Tarihi çarşı sokaklarında sıcak tonlu, kamaşma kontrollü yol ve cephe aydınlatması.",
                    CompletionYear = 2020,
                    IsActive = true
                }
            };

            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();
        }
    }
}
