# HeperFiberli V2

Yeni V2 uygulaması `v2/` klasöründe bulunur. ASP.NET Core MVC (.NET 8) ve EF Core (SQL Server) kullanır; LocalDB bağlantı dizesi `HeperFiberliV2Db` veritabanına işaret eder.

## Kurulum ve çalıştırma

1) Derleme  
`dotnet build`

2) İlk migration (oluşturma)  
`dotnet ef migrations add InitialCreate --project v2/HeperFiberli.Web --startup-project v2/HeperFiberli.Web --output-dir Data/Migrations`

3) Veritabanını güncelleme  
`dotnet ef database update --project v2/HeperFiberli.Web --startup-project v2/HeperFiberli.Web`

4) Uygulamayı çalıştırma  
`dotnet run --project v2/HeperFiberli.Web`

## Doğrulama adımları

- `http://localhost:5000/debug/db` veya `https://localhost:5001/debug/db` adresinde **Database=HeperFiberliV2Db** ve pozitif kayıt sayıları görüntülenir.
- `http://localhost:5000/` ana sayfası açılır ve içerik boştur değil.
- `http://localhost:5000/products` ve `http://localhost:5000/projects` sayfaları listeleme ve detayları gösterir.
