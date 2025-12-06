# Online Sınav Portalı

ASP.NET Core MVC ile geliştirilmiş Online Sınav Portalı projesi.

## 📋 Proje Hakkında

Bu proje, öğrenci numarası son rakamı **8** olan öğrenciler için hazırlanmış **Online Sınav Portalı** projesidir.

## ✨ Özellikler

### Ara Sınav İçin Tamamlanan Özellikler

✅ **Veri Tabanı Tasarımı (Code-First)**
- Admin, Sinav, Soru, Secenek modelleri
- Entity Framework Core ile Code-First yaklaşımı

✅ **Model ve ViewModellerin Oluşturulması**
- AdminLoginViewModel, AdminRegisterViewModel
- SinavViewModel
- SoruViewModel ve SecenekViewModel

✅ **Veri Tabanı Bağlantısı ve Migration İşlemleri**
- SQL Server veritabanı bağlantısı
- Migration ile veritabanı oluşturma

✅ **Repository Yapısının Oluşturulması**
- Generic Repository Pattern
- IAdminRepository, ISinavRepository, ISoruRepository

✅ **Yönetici (Admin) Panelin Tasarımı**
- Bootstrap 4+ ile modern ve responsive tasarım
- Sidebar navigasyon
- Dashboard sayfası

✅ **Cookie Bazlı Oturum Açma ve Yetkilendirme Sistemi**
- Cookie Authentication
- Login/Logout işlemleri
- Yetkilendirme kontrolleri

✅ **Yönetici Sayfalarının Kodlanması**
- Sınav yönetimi (CRUD işlemleri)
- Soru yönetimi (CRUD işlemleri)
- Dashboard

✅ **AJAX Metodunun Kullanılması**
- Soru detaylarını AJAX ile getirme (Modal)
- Soru silme işlemi AJAX ile

## 🛠️ Teknik Detaylar

- **Framework:** .NET 8.0
- **Mimari:** MVC (Model-View-Controller)
- **Veritabanı:** SQL Server
- **ORM:** Entity Framework Core 8.0
- **Tasarım:** Bootstrap 4+
- **Authentication:** Cookie-based Authentication

## 📦 Kurulum

1. Projeyi klonlayın:
   ```bash
   git clone <repository-url>
   cd OnlineSinavPortal
   ```

2. `appsettings.json` dosyasındaki connection string'i kendi SQL Server bilgilerinize göre güncelleyin:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=RG;Database=OnlineSinavPortalDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```

3. Migration'ları uygulayın:
   ```bash
   dotnet ef database update
   ```

4. Projeyi çalıştırın:
   ```bash
   dotnet run
   ```

## 🔐 Varsayılan Admin Kullanıcısı

- **Kullanıcı Adı:** admin
- **Öğrenci Numarası:** 12345678
- **Şifre:** admin123

## 📁 Proje Yapısı

```
OnlineSinavPortal/
├── Controllers/
│   ├── AdminController.cs
│   ├── AdminDashboardController.cs
│   ├── HomeController.cs
│   ├── SinavController.cs
│   └── SoruController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── DbInitializer.cs
├── Models/
│   ├── Admin.cs
│   ├── Sinav.cs
│   ├── Soru.cs
│   └── Secenek.cs
├── Repositories/
│   ├── IRepository.cs
│   ├── Repository.cs
│   ├── IAdminRepository.cs
│   ├── AdminRepository.cs
│   ├── ISinavRepository.cs
│   ├── SinavRepository.cs
│   ├── ISoruRepository.cs
│   └── SoruRepository.cs
├── ViewModels/
│   ├── AdminLoginViewModel.cs
│   ├── AdminRegisterViewModel.cs
│   ├── SinavViewModel.cs
│   └── SoruViewModel.cs
└── Views/
    ├── Admin/
    ├── AdminDashboard/
    ├── Home/
    ├── Sinav/
    └── Soru/
```

## 🚀 Kullanım

1. Projeyi çalıştırdıktan sonra ana sayfaya (`/`) gidin
2. "Kayıt Ol" linkine tıklayarak yeni bir admin hesabı oluşturabilir veya varsayılan admin bilgileri ile giriş yapabilirsiniz
3. Dashboard'dan sınav oluşturabilir, düzenleyebilir ve silebilirsiniz
4. Her sınav için soru ekleyebilir ve yönetebilirsiniz

## 📝 Notlar

- Proje ara sınav için gerekli tüm özellikleri içermektedir
- Final için ek özellikler (kullanıcı arayüzü, SignalR, ASP.NET Identity vb.) eklenecektir
- Connection string'i kendi SQL Server bilgilerinize göre güncellemeyi unutmayın

## 👤 Geliştirici

Bu proje, İnternet Programcılığı I dersi kapsamında hazırlanmıştır.

**Geliştirici:** iremfersiz

## 📄 Lisans

Bu proje eğitim amaçlıdır.

---
*Son güncelleme: 2025*

