namespace Application.DTOs.Users
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpires =>DateTime.Now > Expires;
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public string ReplaceByToken { get; set; }
        public bool IsActive=> Revoked==null && !IsExpires;
    }
}
