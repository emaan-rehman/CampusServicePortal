using CampusServicePortal.Data;

namespace CampusServicePortal.Data
{
    public class UserSession
    {
        public static User? CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void Login(User user) => CurrentUser = user;
        public void Logout() => CurrentUser = null;
    }
}