-- Admin kullanıcısını güncellemek için SQL script
-- SSMS'de OnlineSinavPortalDB veritabanında çalıştırın

-- Önce OgrenciNumarasi kolonunu ekle (yoksa)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Admins]') AND name = 'OgrenciNumarasi')
BEGIN
    ALTER TABLE [Admins] ADD [OgrenciNumarasi] nvarchar(20) NULL;
END
GO

-- Admin kullanıcısını güncelle
-- Şifre: admin123 (SHA256 hash: jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=)
UPDATE [Admins] 
SET [OgrenciNumarasi] = '12345678',
    [Sifre] = 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg='
WHERE [KullaniciAdi] = 'admin';
GO

