# 🏠 RealEstate Dapper — Emlak İlan Platformu

ASP.NET Core 8 ile geliştirilmiş, **Dapper** tabanlı bir emlak ilan platformu. Proje; ilanların listelendiği ve arandığı herkese açık bir site, ilan ve içerik yönetimi için bir **Admin paneli** ve emlak danışmanlarının kendi ilanlarını yönettiği bir **Danışman paneli** olmak üzere üç bölümden oluşur. OpenAI entegrasyonu ile yapay zekâ destekli ilan önerisi, ilan açıklaması yazma ve mesajlara cevap üretme özellikleri, SignalR ile de canlı istatistikler sunar.

---

## ✨ Özellikler

### 🌐 Herkese Açık Site
- Ana sayfada popüler ilanlar, popüler lokasyonlar, danışmanlar ve referanslar
- Kelime, kategori ve şehre göre **ilan arama**
- Fotoğraf galerisi, ilan bilgileri, olanaklar ve sorumlu danışman bilgisiyle **ilan detay sayfası**
- Danışmanın tüm ilanlarını gösteren **danışman profil sayfası**
- Hakkımızda, Biz Kimiz ve İletişim sayfaları
- Özel **401 / 403 / 404** hata sayfaları

### 🛠️ Admin Paneli
- İlan, kategori, danışman, hakkımızda ve olanak yönetimi (CRUD)
- İlanlara **fotoğraf galerisi** ekleme ve sıralama
- İlanlara **olanak atama** (otopark, havuz, asansör vb.) ve tek tıkla durum değiştirme
- Gelen mesajları görüntüleme ve **yapay zekâ ile cevap taslağı üretme**
- **SignalR ile canlı istatistikler**

### 👤 Danışman (Üye) Paneli
- Kendi ilanlarını listeleme, ekleme ve düzenleme
- Canlı önizlemeli yeni ilan formu ve **yapay zekâ ile ilan açıklaması yazma**
- Kullanıcının isteklerine göre mevcut ilanlardan öneri yapan **Yapay Zekâ Emlak Asistanı**

### 🔐 Kimlik Doğrulama ve Yetkilendirme
- **JWT** ile giriş; token UI tarafında cookie içinde saklanır
- **Rol bazlı yetkilendirme:** Admin ve danışman panelleri rol politikalarıyla korunur
- API'de ekleme, güncelleme ve silme işlemleri `[Authorize]` ile korunur, UI istekleri **Bearer token** gönderir

---

## 🧰 Kullanılan Teknolojiler

| Katman | Teknolojiler |
|---|---|
| Backend | ASP.NET Core 8 Web API, Dapper, MS SQL Server |
| Frontend | ASP.NET Core MVC, Razor, Bootstrap, jQuery |
| Kimlik Doğrulama | JWT Bearer, Cookie Authentication, Rol Bazlı Yetkilendirme |
| Gerçek Zamanlı | SignalR |
| Yapay Zekâ | OpenAI Chat Completions API |
| Diğer | View Components, Areas, Swagger, IHttpClientFactory |

---

## 🏗️ Proje Yapısı

```
RealEstate_Dapper
├── RealEstate_Dapper_Api      → Web API (Dapper repository'leri, JWT üretimi, SignalR)
└── RealEstate_Dapper_UI       → MVC arayüz (API'yi HttpClient ile tüketir)
    ├── Areas/Admin            → Admin paneli
    ├── Areas/EstateAgent      → Danışman paneli
    └── Views / ViewComponents → Herkese açık site
```

UI projesi veritabanına doğrudan erişmez; tüm veri işlemleri API üzerinden yapılır.

---

## 📸 Ekran Görüntüleri

### Herkese Açık Site

| Ana Sayfa | Arama |
|---|---|
| ![Ana Sayfa](<RealEstate_Dapper_UI/wwwroot/images/Ana Sayfa.png>) | ![Ana Sayfa Arama Ekranı](<RealEstate_Dapper_UI/wwwroot/images/Ana Sayfa Arama Ekranı.png>) |

| Popüler İlanlar | Popüler Lokasyonlar |
|---|---|
| ![Popüler İlanlar](<RealEstate_Dapper_UI/wwwroot/images/Popüler İlanlar.png>) | ![Popüler Lokasyonlar](<RealEstate_Dapper_UI/wwwroot/images/Popüler Lokasyonlar.png>) |

| İlan Detay | İlan Detay 2 |
|---|---|
| ![Ana Sayfa Emlak Detay](<RealEstate_Dapper_UI/wwwroot/images/Ana Sayfa Emlak Detay.png>) | ![Ana Sayfa Emlak Detay 2](<RealEstate_Dapper_UI/wwwroot/images/Ana Sayfa Emlak Detay 2.png>) |

| Danışman Detay | Çalışanlar |
|---|---|
| ![Ana Sayfa Danışman Detay](<RealEstate_Dapper_UI/wwwroot/images/Ana Sayfa Danışman Detay.png>) | ![Çalışanlar](<RealEstate_Dapper_UI/wwwroot/images/Çalışanlar.png>) |

| Hakkımızda | Hakkımızda İstatistik |
|---|---|
| ![Hakkımızda](<RealEstate_Dapper_UI/wwwroot/images/Hakkımızda.png>) | ![Hakkımızda İstatistik](<RealEstate_Dapper_UI/wwwroot/images/Hakkımızda İstatistik.png>) |

| Biz Kimiz | Referanslar |
|---|---|
| ![Biz Kimiz Sayfası](<RealEstate_Dapper_UI/wwwroot/images/Biz Kimiz Sayfası.png>) | ![Referanslar](<RealEstate_Dapper_UI/wwwroot/images/Referanslar.png>) |

| İletişim | Alt Bölüm |
|---|---|
| ![İletişim](<RealEstate_Dapper_UI/wwwroot/images/İletişim.png>) | ![Alt Bölüm](<RealEstate_Dapper_UI/wwwroot/images/Alt Bölüm.png>) |

| 404 Sayfası |
|---|
| ![404 Sayfası](<RealEstate_Dapper_UI/wwwroot/images/404 Sayfası.png>) |

### Admin Paneli

| Dashboard | Dashboard 2 |
|---|---|
| ![Admin Dashboard](<RealEstate_Dapper_UI/wwwroot/images/Admin Dashboard.png>) | ![Admin Dashboard 2](<RealEstate_Dapper_UI/wwwroot/images/Admin Dashboard 2.png>) |

| Canlı İstatistikler (SignalR) | Gelen Mesajlar – AI ile Cevap Üretme |
|---|---|
| ![Admin Canlı İstatistikler](<RealEstate_Dapper_UI/wwwroot/images/Admin Canlı İstatistikler.png>) | ![Admin Gelen Mesajlar AI İle Cevap Üretme](<RealEstate_Dapper_UI/wwwroot/images/Admin Gelen Mesajlar AI İle Cevap Üretme.png>) |

| İlanlar | İlan Detay |
|---|---|
| ![Admin İlanlar](<RealEstate_Dapper_UI/wwwroot/images/Admin İlanlar.png>) | ![Admin İlan Detay](<RealEstate_Dapper_UI/wwwroot/images/Admin İlan Detay.png>) |

| İlan Olanakları | Popüler Lokasyonlar |
|---|---|
| ![Admin İlan Olanakları](<RealEstate_Dapper_UI/wwwroot/images/Admin İlan Olanakları.png>) | ![Admin Popüler Lokasyonlar](<RealEstate_Dapper_UI/wwwroot/images/Admin Popüler Lokasyonlar.png>) |

### Danışman (Üye) Paneli

| Dashboard | Dashboard 2 |
|---|---|
| ![Üye Dashboard](<RealEstate_Dapper_UI/wwwroot/images/Üye Dashboard.png>) | ![Üye Dashboard 2](<RealEstate_Dapper_UI/wwwroot/images/Üye Dashboard 2.png>) |

| AI Emlak Asistanı | İlanlarım |
|---|---|
| ![Üye Emlak AI Asistanı](<RealEstate_Dapper_UI/wwwroot/images/Üye Emlak AI Asistanı.png>) | ![Üye İlanlar](<RealEstate_Dapper_UI/wwwroot/images/Üye İlanlar.png>) |

| İlan Detay | İlan Özellikleri Düzenleme |
|---|---|
| ![Üye İlan Detay](<RealEstate_Dapper_UI/wwwroot/images/Üye İlan Detay.png>) | ![Üye İlan Özellikleri Düzenleme](<RealEstate_Dapper_UI/wwwroot/images/Üye İlan Özellikleri Düzenleme.png>) |

---

## 🚀 Kurulum

### Gereksinimler
- .NET 8 SDK
- MS SQL Server
- Visual Studio 2022 (veya `dotnet` CLI)
- Yapay zekâ özellikleri için bir OpenAI API anahtarı

### 1. Projeyi klonlayın
```bash
git clone https://github.com/<EmreHaci03>/RealEstate_Dapper.git
```

### 2. Veritabanını oluşturun
SQL Server'da `DbDapperRealEstate` adında bir veritabanı oluşturun ve tabloları/örnek verileri ekleyin.

### 3. Gizli ayarları girin (User Secrets)
Gizli bilgiler `appsettings.json` içinde tutulmaz. Visual Studio'da projeye sağ tıklayıp **Manage User Secrets** ile ekleyin.

**RealEstate_Dapper_Api**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=DbDapperRealEstate;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Key": "en-az-32-karakterlik-gizli-anahtariniz"
  }
}
```

**RealEstate_Dapper_UI**
```json
{
  "OpenAI": {
    "ApiKey": "sk-..."
  }
}
```

---

## 👥 Roller

| Rol | Erişim |
|---|---|
| **Admin** | Admin paneli: tüm içerik, ilan, mesaj ve istatistik yönetimi |
| **Member** | Danışman paneli: kendi ilanları ve AI asistanı |
| Ziyaretçi | Herkese açık site: ilan arama, detay ve iletişim |
