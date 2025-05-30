public class JwtUserTokenDto
{
    public Guid TokenId { get; set; }

    public string encryption { get; set; }
    public DateTime expiration { get; set; }

    public Guid userId { get; set; }
    public string userName { get; set; }
    public string userRole { get; set; }
}