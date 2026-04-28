param(
    [string]$SubModulePath = (Join-Path $PSScriptRoot "..\SubModule.xml"),
    [string]$Configuration = "Debug"
)

# SubModule.xml dosyasını oku
if (-not (Test-Path $SubModulePath)) {
    Write-Error "SubModule.xml bulunamadı: $SubModulePath"
    exit 1
}

[xml]$xml = Get-Content $SubModulePath

# Mevcut versiyonu al
$currentVersion = $xml.Module.Version.value
Write-Host "Mevcut versiyon: $currentVersion"

# Versiyon formatı: b0.7.18.1
# Kısımlar: b0 . 7 . 18 . 1
if ($currentVersion -match '^(b\d+)\.(\d+)\.(\d+)\.(\d+)$') {
    $prefix = $matches[1]      # b0
    $major = [int]$matches[2]  # 7
    $minor = [int]$matches[3]  # 18
    $patch = [int]$matches[4]  # 1

    if ($Configuration -eq "Release") {
        # Release: sondan ikinciyi artır, sonuncuyu 0 yap
        $minor++
        $patch = 0
        Write-Host "Release Build: Minor versiyonu artırıldı, patch sıfırlandı"
    } else {
        # Debug: sonuncuyu artır
        $patch++
        Write-Host "Debug Build: Patch versiyonu artırıldı"
    }

    $newVersion = "$prefix.$major.$minor.$patch"
    Write-Host "Yeni versiyon: $newVersion"

    # XML'de versiyonu güncelle
    $xml.Module.Version.value = $newVersion

    # Dosyayı kaydet (formatting korunarak)
    $settings = New-Object System.Xml.XmlWriterSettings
    $settings.Indent = $true
    $settings.IndentChars = "  "
    $settings.NewLineChars = "`n"
    $settings.Encoding = [System.Text.Encoding]::UTF8

    $writer = [System.Xml.XmlWriter]::Create($SubModulePath, $settings)
    $xml.WriteTo($writer)
    $writer.Flush()
    $writer.Close()

    Write-Host "SubModule.xml başarıyla güncellendi"
    exit 0
} else {
    Write-Error "Versiyon formatı geçersiz: $currentVersion (beklenen format: b0.7.18.1)"
    exit 1
}

