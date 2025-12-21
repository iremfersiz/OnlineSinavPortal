-- Veritabanını Temizleme Scripti
-- Bu scripti SQL Server Management Studio'da çalıştırın

USE OnlineSinavPortalDB;
GO

-- Foreign key constraint'leri önce sil
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Sinavlar_AspNetUsers_UserId')
    ALTER TABLE Sinavlar DROP CONSTRAINT FK_Sinavlar_AspNetUsers_UserId;
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SinavSonuclari_AspNetUsers_UserId')
    ALTER TABLE SinavSonuclari DROP CONSTRAINT FK_SinavSonuclari_AspNetUsers_UserId;
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SinavSonuclari_Ogrenciler_OgrenciId')
    ALTER TABLE SinavSonuclari DROP CONSTRAINT FK_SinavSonuclari_Ogrenciler_OgrenciId;
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SinavSonuclari_Sinavlar_SinavId')
    ALTER TABLE SinavSonuclari DROP CONSTRAINT FK_SinavSonuclari_Sinavlar_SinavId;
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Sorular_Sinavlar_SinavId')
    ALTER TABLE Sorular DROP CONSTRAINT FK_Sorular_Sinavlar_SinavId;
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Secenekler_Sorular_SoruId')
    ALTER TABLE Secenekler DROP CONSTRAINT FK_Secenekler_Sorular_SoruId;
GO

-- Tüm tabloları sil (bağımlılık sırasına göre)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Secenekler')
    DROP TABLE [Secenekler];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Sorular')
    DROP TABLE [Sorular];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SinavSonuclari')
    DROP TABLE [SinavSonuclari];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Ogrenciler')
    DROP TABLE [Ogrenciler];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Sinavlar')
    DROP TABLE [Sinavlar];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Admins')
    DROP TABLE [Admins];
GO

-- Identity tablolarını sil
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserTokens')
    DROP TABLE [AspNetUserTokens];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserRoles')
    DROP TABLE [AspNetUserRoles];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserLogins')
    DROP TABLE [AspNetUserLogins];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserClaims')
    DROP TABLE [AspNetUserClaims];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoleClaims')
    DROP TABLE [AspNetRoleClaims];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoles')
    DROP TABLE [AspNetRoles];
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUsers')
    DROP TABLE [AspNetUsers];
GO

-- Migration geçmişini temizle
IF EXISTS (SELECT * FROM sys.tables WHERE name = '__EFMigrationsHistory')
    DROP TABLE [__EFMigrationsHistory];
GO

PRINT 'Veritabani temizlendi! Migration uygulayabilirsiniz.';
GO

