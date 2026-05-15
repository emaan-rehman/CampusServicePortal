using CampusServicePortal.Data;

public class UserSession
{
    public User? CurrentUser { get; set; }
    // Ensure this logic is simple:
    public bool IsLoggedIn => CurrentUser != null;

    public void Login(User user) => CurrentUser = user;
    public void Logout() => CurrentUser = null;
}