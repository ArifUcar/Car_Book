using UdemyCarBook.Domain.Base;

public class Contact : BaseEntity
{
    /// <summary>
    /// İletişim kuran kişinin adı
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Email adresi
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Konu
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Mesaj içeriği
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Mesajın okunma durumu
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Mesajın yanıtlanma durumu
    /// </summary>
    public bool IsReplied { get; set; }

    /// <summary>
    /// Yanıt tarihi
    /// </summary>
    public DateTime? ReplyDate { get; set; }

    /// <summary>
    /// Yanıt içeriği
    /// </summary>
    public string? ReplyMessage { get; set; }
} 