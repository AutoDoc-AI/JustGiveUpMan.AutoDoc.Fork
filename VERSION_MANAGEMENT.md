# Otomatik Versiyon Yönetimi

## Özet
Versiyon numarası artık her build'de otomatik olarak güncellenecek:

- **Debug Build**: Son sayı artırılır
  - Örnek: `b0.7.18.1` → `b0.7.18.2`

- **Release Build**: Sondan ikinci sayı artırılır, son sayı 0 yapılır
  - Örnek: `b0.7.18.1` → `b0.7.19.0`

## Nasıl Çalışır?

1. **UpdateVersion.ps1** scripti (`JGUM/Build/UpdateVersion.ps1`):
   - `SubModule.xml` dosyasını okur
   - Mevcut versiyonu parse eder
   - Configuration'a göre versiyonu günceller (Debug veya Release)
   - Güncellenmiş versiyonla `SubModule.xml`'i yazıp kapatır

2. **JGUM.csproj** ve **JGUM.MCMBridge.csproj**:
   - `UpdateVersion` target'ı `PostBuildEvent`'ten önce çalışır
   - Script, `$(Configuration)` ve `$(SubModuleTemplatePath)` parametreleriyle çalıştırılır
   - Script başarıyla tamamlandıktan sonra normal post-build işlemi devam eder

## Manuel Test

Debug konfigürasyonuyla test etmek:
```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File "JGUM\Build\UpdateVersion.ps1" -SubModulePath "JGUM\SubModule.xml" -Configuration "Debug"
```

Release konfigürasyonuyla test etmek:
```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File "JGUM\Build\UpdateVersion.ps1" -SubModulePath "JGUM\SubModule.xml" -Configuration "Release"
```

## Dosyalar

- **JGUM/Build/UpdateVersion.ps1**: PowerShell scripti (yeni oluşturuldu)
- **JGUM/JGUM.csproj**: `UpdateVersion` target'ı eklendi
- **JGUM.MCMBridge/JGUM.MCMBridge.csproj**: `UpdateVersion` target'ı eklendi

## Notlar

- Versiyon formatı: `bMAJOR.MINOR.PATCH.BUILD` (örn: `b0.7.18.1`)
- İlk segment (`b0`) değişmez
- Script hata durumunda exit code 1 döner ve build devam etmez
- XML formatting korunur (indentation, encoding)

