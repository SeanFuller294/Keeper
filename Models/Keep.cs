namespace Keeper.Models
{
  public class Keep
  {
    public int Id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string userId { get; set; }
    public string img { get; set; }
    public bool isPrivate { get; set; }
    public int views { get; set; }
    public int shares { get; set; }
    public int hasBeenKept { get; set; }
  }
}