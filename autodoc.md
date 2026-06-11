# Just Give Up Man! (JGUM) - Proje Dokümantasyonu

Bu dosya projede yer alan tüm temel dizinlerin ve dosyaların kısa açıklamalarını, aralarındaki bağlantıları ve önemli işlevlerini içerir. Mod, Bannerlord için dinamik teslim olma ve müzakere mekanikleri ekler.

## 📌 Ana Modül (Core)
* **[JGUMSubModule.cs](JGUM/JGUMSubModule.cs)**: Modun giriş noktasıdır. `MBSubModuleBase` sınıfından türer.
  * *Önemli Fonksiyonlar*: `OnSubModuleLoad` (MCM Bridge aktivasyonunu dener), `OnBeforeInitialModuleScreenSetAsRoot` (ayarları başlatır), `OnGameStart` (Campaign behavior'larını kaydeder).

## ⚙️ Yapılandırma ve Ayarlar (Config & MCM Bridge)
Proje doğrudan MCM (Mod Configuration Menu) moduna bağımlı olmamak için **Bridge (Köprü)** deseni kullanır.
* **[JgumSettingsManager.cs](JGUM/Config/JgumSettingsManager.cs)**: Mod içi ayarların yönetildiği ana statik sınıftır. Thread-safe (güvenli) çalışır. Dışarıdan veya varsayılan JSON yapılandırmasından beslenir.
* **[JgumJsonModel.cs](JGUM/Config/JgumJsonModel.cs)**: MCM bulunmadığı durumlarda ayarları tutan JSON veri modelidir.
* **[BridgeBootstrap.cs](JGUM.MCMBridge/BridgeBootstrap.cs)**: MCMv5 yüklüyse Reflection üzerinden dinamik olarak çalıştırılıp MCM ayarlarını ana moda entegre eder.

## 🧠 Kampanya Davranışları (Behaviors)
Modun kalbini oluşturan, oyun içi olayları dinleyen dinleyicilerdir (Campaign Behaviors).
* **[SiegeSurrenderBehavior.cs](JGUM/Behaviors/SiegeSurrenderBehavior.cs)**: Kuşatmaları takip eder. Açlık, duvar hasarı, kuşatma aletleri ve moral durumuna göre garnizonun teslim olma şansını hesaplar.
* **[LordEncounterSurrenderBehavior.cs](JGUM/Behaviors/LordEncounterSurrenderBehavior.cs)**: Haritadaki düşman lord karşılaşmalarını yakalar. Güç oranları ve lord özellikleri (Merhamet, Cesaret vb.) baz alınarak teslim olma durumu belirlenir.
* **[PatrolEncounterSurrenderBehavior.cs](JGUM/Behaviors/PatrolEncounterSurrenderBehavior.cs)**: Devriye ve daha küçük birlik karşılaşmalarındaki teslim olma durumlarını yönetir.
* **[VoluntarySurrenderBehavior.cs](JGUM/Behaviors/VoluntarySurrenderBehavior.cs)**: Aktif bir savaş olmadan gerçekleşen gönüllü teslimiyet mekaniklerini ele alır.
* **[FiefPurchaseOfferBehavior.cs](JGUM/Behaviors/FiefPurchaseOfferBehavior.cs)**: Tımar (Fief) satın alma veya devretme teklifi mekaniklerini oyuna ekler.

### 🗣️ Kuşatma Müzakereleri (Siege Negotiation Behavior)
Müzakere sistemi büyük olduğu için, tek bir `partial` sınıf üzerinden dört farklı dosyaya bölünerek tasarlanmıştır:
* **[Core.cs](JGUM/Behaviors/SiegeNegotiationBehavior/SiegeNegotiationBehavior.Core.cs)**: Müzakere mini-oyununun temel yaşam döngüsünü ve state (durum) takibini yapar.
* **[MenuAndRequests.cs](JGUM/Behaviors/SiegeNegotiationBehavior/SiegeNegotiationBehavior.MenuAndRequests.cs)**: Kuşatma menülerine "Müzakere et" seçeneğini ekler ve diyalog (Conversation) işlemlerini tetikler.
* **[Persuasion.cs](JGUM/Behaviors/SiegeNegotiationBehavior/SiegeNegotiationBehavior.Persuasion.cs)**: İkna sistemi (Persuasion) kontrollerini ve mini-oyun aşamalarını barındırır.
* **[StateCleanup.cs](JGUM/Behaviors/SiegeNegotiationBehavior/SiegeNegotiationBehavior.StateCleanup.cs)**: Diyalog veya müzakere sonlandığında önbelleklenen değerlerin sıfırlanmasını/temizlenmesini sağlar.

## 🤖 Yapay Zeka Davranışları (AI Behaviors)
* **[AISiegeSurrenderBehavior.cs](JGUM/AIBehaviors/AISiegeSurrenderBehavior.cs)**: Yapay zeka ordularının kendi aralarındaki kuşatmalarda teslim olma mantığını devreye sokar.
* **[AILordEncounterSurrenderBehavior.cs](JGUM/AIBehaviors/AILordEncounterSurrenderBehavior.cs)**: Yapay zeka lordlarının kendi aralarındaki (oyuncunun olmadığı) meydan savaşlarında teslim olma hesaplamalarını yapar.

## 🧮 Hesaplayıcılar (Calculators)
Davranışlar (Behaviors) için gereken matematiksel formülleri ve oranları hesaplayan sistem motorlarıdır.
* **[SiegeSurrenderCalculator.cs](JGUM/Calculators/SiegeSurrenderCalculator.cs)**: Kuşatma sırasındaki teslimiyet olasılıklarını (garnizon oranı, yiyecek, hasar vb. birleştirerek) hesaplar.
* **[SiegePressureCalculator.cs](JGUM/Calculators/SiegePressureCalculator.cs)**: Kuşatan ordunun, savunan şehir/kale üzerindeki baskı puanını kümülatif olarak ölçer.
* **[LordSurrenderCalculator.cs](JGUM/Calculators/LordSurrenderCalculator.cs)**: Meydan savaşlarında lordların kişilik özellikleri üzerinden ne kadar kolay/zor teslim olacaklarını belirler.
* **[NegotiationCalculator.cs](JGUM/Calculators/NegotiationCalculator.cs)**: İkna denemeleri ve müzakere sırasında şans/zorluk seviyesini (Kolay, Normal, Zor) belirler.
* **[StringCalculator.cs](JGUM/Calculators/StringCalculator.cs)**: Yerelleştirme ve metin varyantları (örneğin `{=id_count}`) için kullanılır; diyalogların dinamik varyasyonlarla tekrar etmeden gösterilmesini sağlar.

## 🛠️ Eylemler (Actions) & Diğer Sistemler
* **[CaptureSettlementByNegotiationAction.cs](JGUM/Actions/CaptureSettlementByNegotiationAction.cs)**: Müzakere başarıyla sonuçlandığında yerleşkenin el değiştirmesi işlemini, oyunun mantığına uygun bir `Action` olarak yürütür.
* **[AiSurrenderLogEntry.cs](JGUM/Data/AiSurrenderLogEntry.cs)**: Yapay zekanın kendi arasındaki teslimiyet durumlarını oyun içi kayıt ve bildirim defterine (Log) işler ve artık teslimiyetin sonucunu da detaylandırır.
* **[JgumInteropEvents.cs](JGUM/Interop/JgumInteropEvents.cs)**: Diğer modların (Third-party) JGUM içerisindeki olaylara (teslimiyet olması, müzakere sonucunda şehrin el değiştirmesi vb.) dışarıdan "Event" dinleyerek bağlanabilmesini sağlar. `JgumSurrenderRecord` sınıfı artık teslimiyetin sonucunu (kabul edildi/reddedildi) da içerir.