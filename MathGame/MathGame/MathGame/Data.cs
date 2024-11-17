namespace MathGame
{
    internal class User() 
        {
            public string? Name { get; set; }  
        }
    internal class UserManager
    {

        private static UserManager? _instance;


        public User CurrentUser { get; private set; }


        private UserManager()
        {
            CurrentUser = new User();  
        }

        public static UserManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserManager();
            }
            return _instance;
        }

        public void SetUserName(string name)
        {
            CurrentUser.Name = name;
        }

        public string? GetUserName()
        {
            return CurrentUser.Name;
        }
    }



   internal enum GameDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}
