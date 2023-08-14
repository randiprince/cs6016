
using MySqlConnector;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "server=cs-db.eng.utah.edu; database = Library; uid = u1431008; password=";
        Console.WriteLine("enter your password: ");
        connectionString += ReadPass_hidden();
        Console.WriteLine(connectionString);

        TestDB(connectionString);
    }

    private static void TestDB(string str)
    {
        using (MySqlConnection MyConn = new MySqlConnection(str))
        {
            MyConn.Open();
            MySqlCommand MyCmd = MyConn.CreateCommand();
            Console.WriteLine("Enter the title of a book");
            MyCmd.CommandText = "select * from Titles";

            using (MySqlDataReader myReader = MyCmd.ExecuteReader())
            {
                while (myReader.Read())
                {
                    Console.WriteLine(myReader["ISBN"] + "---" + myReader["Author"]);
                }

                Console.WriteLine("Done");
            }

        }
    }

    private static string ReadPass_hidden()
    {
        string pwd = "";
        while(true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                break;
            }
            pwd += key.KeyChar;
        }
        return pwd;
    }
}