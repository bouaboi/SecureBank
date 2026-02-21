using SecureBank.Models;


namespace SecureBank.Global_Classes
{
    public static class clsSession
    {

        public static User LoggedInUser { get; private set; }
        public static Client LoggedInClient { get; private set; }
        public static Client ForNewClient { get; private set; }
        public static bool IsEmployee => LoggedInUser != null;
        public static bool IsClient => LoggedInClient != null;
        public static bool IsNewClient => ForNewClient != null;
        public static bool IsLoggedIn => IsEmployee || IsClient;

        public static string FullName
        {
            get
            {
                if (IsEmployee)
                    return $"{LoggedInUser.FirstName} {LoggedInUser.LastName}";
                else if (IsClient)
                    return $"{LoggedInClient.FirstName} {LoggedInClient.LastName}";
                return "";
            }
        }

        public static int GetID()
        {
            if (IsEmployee)
                return LoggedInUser.UserID;
            else if (IsClient)
                return LoggedInClient.ClientID;
            else if (IsNewClient)
                return ForNewClient.ClientID;
                return -1;
        }

        public static void LoginAsUser(User user)
        {
            LoggedInUser = user;
            LoggedInClient = null;
        }

        public static void LoginAsClient(Client client)
        {
            LoggedInClient = client;
            LoggedInUser = null;
        }

        public static void Logout()
        {
            LoggedInUser = null;
            LoggedInClient = null;
        }

        public static void NewClientId(Client client)
        {
            ForNewClient = client;
        }

    }
}