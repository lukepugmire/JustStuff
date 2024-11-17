namespace MathGame
{
    class Program
    {

        private static Helpers helpers = new Helpers();
        public static void Main(string[] args)
        {
            Menu menu = new Menu();
            
            helpers.TypeString("Welcome!", 50);
            helpers.TypeString("Do You Want To Play A Game? Yes : No");
            string? YesNo = Console.ReadLine().Trim().ToLower();

            var userManager = UserManager.GetInstance();



            if (YesNo == "yes" || YesNo == "no")
            {
                switch (YesNo)
                {
                    case "yes":
                        helpers.TypeString("Please Enter Your Name...");
                        userManager.SetUserName(Console.ReadLine());
                        menu.DisplayMenu(userManager.GetUserName());
                        break;

                    case "no":
                        helpers.TypeString("Program Aborting....", 150);
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}