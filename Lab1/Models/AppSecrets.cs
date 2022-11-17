namespace Lab1.Models
{
    public class AppSecrets
    {
        public string ManagerPassword { get; set; }
        public string PlayerPassword { get; set; }

        public AppSecrets(string? managerPassword, string? playerPassword)
        {
            ManagerPassword = managerPassword;
            PlayerPassword = playerPassword;
        }
        public AppSecrets()
        {

        }
    }
}
