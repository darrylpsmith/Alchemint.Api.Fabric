namespace Alchemint.Bar
{
    public interface IBarUser
    {
        string Email { get; set; }
        string Id { get; set; }
        string Password { get; set; }
        string Telephone { get; set; }
        string UserName { get; set; }
    }
}