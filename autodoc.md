Verilen değişiklik (diff) kayıtlarına göre, projenin `autodoc.md` (otomatik dokümantasyon) dosyasını güncellemek için kullanabileceğiniz Markdown içeriği aşağıdadır. 

Bu değişiklikler temel olarak **Mod Sürümü**, **Veri Yapıları (API/Interop)** ve **Olay Kayıt Mantığı** olmak üzere üç ana başlık altında dokümante edilmelidir.

---

# `autodoc.md` Güncelleme Önerisi

## 1. Sürüm Bilgisi (Mod Version)
Dokümantasyonun en başında veya ilgili sürüm geçmişi (Changelog) bölümünde sürüm numarasını güncelleyin:

```markdown
- **Mevcut Sürüm:** v2.1.1.26  *(Önceki: v2.1.0.24)*
```

---

## 2. Interop ve Veri Yapıları (Interop & Data Structures)
`JgumInteropEvents` altında yer alan veri yapılarına yeni eklenen `JgumSurrenderOutcome` enum sınıfını ve `JgumSurrenderRecord` sınıfına eklenen yeni özelliği dokümantasyona ekleyin.

### `JGUM.Interop` Yapısı Güncellemesi

#### Yeni Enum: `JgumSurrenderOutcome`
Teslim olma taleplerinin sonucunu belirtmek için kullanılan yeni bir enum eklenmiştir.

```csharp
public enum JgumSurrenderOutcome
{
    Accepted, // Teslim olma kabul edildi
    Rejected  // Teslim olma reddedildi
}
```

#### Güncellenen Sınıf: `JgumSurrenderRecord`
Teslim olma kayıtlarını tutan veri yapısına, işlemin sonucunu (Kabul/Red) belirten `Outcome` özelliği eklenmiştir.

```csharp
public sealed class JgumSurrenderRecord
{
    public JgumSurrenderKind Kind { get; set; }
    public string? WinnerClanId { get; set; }
    public string? LoserFactionId { get; set; }
    public float CampaignTimeDays { get; set; }
    public bool AcceptedByPlayer { get; set; }
    // Yeni Eklenen Alan:
    public JgumSurrenderOutcome Outcome { get; set; } 
}
```

---

## 3. Teslim Olma Davranışları ve Olay Yönetimi (Behaviors & Event Handling)
Modun teslim olma mekanizmalarında (AI ve Oyuncu taraflı), her bir teslim olma olayının (Surrender Event) nasıl sonuçlandığı artık `Outcome` özelliği üzerinden takip edilmektedir.

Aşağıdaki tabloda, hangi davranış sınıfında hangi duruma göre `Outcome` değerinin atandığı belirtilmiştir:

| Sınıf (Behavior) | Olay / Durum (Event / State) | Sonuç (`Outcome`) | Açıklama |
| :--- | :--- | :--- | :--- |
| **AILordEncounterSurrenderBehavior** | `OnMapEventStarted` | `Accepted` | AI lord karşılaşmasında teslim olma kabul edildiğinde. |
| **AISiegeSurrenderBehavior** | `OnDailyTickSettlement` | `Accepted` | AI kuşatmasında teslim olma kabul edildiğinde. |
| **FiefPurchaseOfferBehavior** | `OnOfferAccepted` | `Accepted` | Mülk satın alma teklifi kabul edildiğinde. |
| **FiefPurchaseOfferBehavior** | `OnOfferRejected` | `Rejected` | Mülk satın alma teklifi reddedildiğinde. |
| **LordEncounterSurrenderBehavior** | `OnConversationEnded` (Kabul) | `Accepted` | Lord karşılaşmasında teslim olma kabul edildiğinde. |
| **LordEncounterSurrenderBehavior** | `RejectLordSurrender` | `Rejected` | Lord karşılaşmasında teslim olma reddedildiğinde. |
| **PatrolEncounterSurrenderBehavior**| `OnConversationEnded` (Kabul) | `Accepted` | Devriye karşılaşmasında teslim olma kabul edildiğinde. |
| **PatrolEncounterSurrenderBehavior**| `RejectPatrolSurrender` | `Rejected` | Devriye karşılaşmasında teslim olma reddedildiğinde. |
| **SiegeNegotiationBehavior** | `OnProactivePersuasionSuccess`| `Accepted` | Kuşatma ikna süreci başarıyla sonuçlandığında. |
| **SiegeNegotiationBehavior** | `OnProactivePersuasionFailure`| `Rejected` | Kuşatma ikna süreci başarısız olduğunda. |
| **SiegeSurrenderBehavior** | `AcceptSurrender` / `OnDailyTick`| `Accepted` | Kuşatma teslim olması kabul edildiğinde. |
| **SiegeSurrenderBehavior** | `RejectSurrenderInternal` | `Rejected` | Kuşatma teslim olması reddedildiğinde. |
| **VoluntarySurrenderBehavior** | `ExecuteSurrender` | `Accepted` | Gönüllü teslim olma gerçekleştiğinde. |
| **VoluntarySurrenderBehavior** | `OnSurrenderRejected` | `Rejected` | Gönüllü teslim olma reddedildiğinde. |

---

## Değişiklik Özeti (Changelog Entry)
Eğer `autodoc.md` dosyanızda bir **Sürüm Günlüğü (Changelog)** bulunuyorsa, aşağıdaki satırı ekleyebilirsiniz:

```markdown
### v2.1.1.26
- **[Yeni Özellik]** `JgumSurrenderOutcome` enum yapısı eklendi.
- **[Geliştirme]** `JgumSurrenderRecord` sınıfına `Outcome` (Kabul/Red) özelliği eklendi.
- **[Düzeltme/Entegrasyon]** Tüm teslim olma, kuşatma, lord ve devriye karşılaşması davranışları (Behaviors) yeni `Outcome` yapısını destekleyecek şekilde güncellendi. Artık teslim olma olaylarının sadece oyuncu tarafından kabul edilip edilmediği değil, olayın nihai olarak başarıyla (Accepted) mı yoksa başarısızlıkla (Rejected) mı sonuçlandığı da raporlanıyor.
```