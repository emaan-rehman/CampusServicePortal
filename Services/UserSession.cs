using CampusServicePortal.Data;

namespace CampusServicePortal.Data
{
    public class UserSession
    {
        // Static means it persists across the entire app instance
        public static User? CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void Login(User user) => CurrentUser = user;
        public void Logout() => CurrentUser = null;
    }
}