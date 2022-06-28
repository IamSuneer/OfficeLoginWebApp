namespace OfficeLoginWebApp.Services
{
    public interface IDataService
    {
        public void LoginEntry(string userId, string IPAddress);

        public void logoutEntry(string userName);
    }
}
