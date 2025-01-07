namespace UdemyCarBook.Application.Tools
{
    public class JwtTokenDefaults
    {
        public const string ValidAudience = "https://localhost";
        public const string ValidIssuer = "https://localhost";
        public const string Key = "UdemyCarBook2024!*UdemyCarBook2024!*";
        public const int Expire = 60; // Token geçerlilik süresi (dakika)
    }
} 