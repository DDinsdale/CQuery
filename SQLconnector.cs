using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
namespace Dod_Gy
{
    public class SQLconnector
    {
        static void Main(string[] args)
        {
            
            //Exceptions
            try{
                string connectionString;
                SqlConnection cnn;
                connectionString = @"Data Source=no.database.here.com;Database=Is;User ID=Wally;Password=Where";
                cnn = new SqlConnection(connectionString);
                cnn.Open();
            }
            catch{
                Console.WriteLine("Inccorect Connection profile.");  
            }
            
            string CS;
            SqlConnection connection;
            CS = @"Data Source=database-1.ctxjhczktjx4.us-east-1.rds.amazonaws.com;Database=Movies;User ID=admin;Password=password;MultipleActiveResultSets=true";
            connection = new SqlConnection(CS);
            connection.Open();

            //Read 1
            string movielist = "SELECT * FROM MOVIE";
            using var cmd = new SqlCommand(movielist, connection);
            using SqlDataReader rdr = cmd.ExecuteReader();
            List<Movie> movies = new List<Movie>();
            while (rdr.Read())
            {
                movies.Add(new Movie(rdr.GetInt32(0), 
                rdr.GetString(1), 
                rdr.GetInt16(2), 
                rdr.GetInt16(3)));
            }
            //Read 2
            string titlelist = "SELECT * FROM MOVIE WHERE TITLE LIKE 'The%' OR TITLE LIKE '%The%' OR TITLE LIKE '%The'";
            using var Title = new SqlCommand(titlelist, connection);
            using SqlDataReader treader = Title.ExecuteReader();
            while (treader.Read())
            {
                Console.WriteLine("{0}", treader.GetString(1));
            }
            Console.WriteLine();
            
            //Read 3
            string lukelist = "SELECT M.TITLE FROM MOVIE M INNER JOIN CASTING C ON C.MOVIENO = M.MOVIENO INNER JOIN ACTOR A ON C.ACTORNO = A.ACTORNO WHERE A.FULLNAME LIKE 'Luke Wilson'";
            using var llist = new SqlCommand(lukelist,connection);
            using SqlDataReader lreader = llist.ExecuteReader();
            while (lreader.Read()){
                Console.WriteLine("{0}", lreader.GetString(0));
            }
            Console.WriteLine();

            //Read 4
            List<int> MovieRuntime = new List<int>();
            foreach(var r in movies){
                MovieRuntime.Add(r.RUNTIME);
            }
            int totalruntime = MovieRuntime.Sum();
            Console.WriteLine(totalruntime);
            
            //Update 1
            using SqlCommand updateruntime = connection.CreateCommand();
            updateruntime.CommandText = "UPDATE MOVIE SET RUNTIME = @RT WHERE TITLE = @T";
            Console.Write("Movie Title: ");
            updateruntime.Parameters.AddWithValue("@T", Console.ReadLine());
            Console.Write("Set Runtime: ");
            updateruntime.Parameters.AddWithValue("@RT", Console.ReadLine());

            //Update 2
            using SqlCommand updatename = connection.CreateCommand();
            updatename.CommandText = "UPDATE ACTOR SET SURNAME = @S, FULLNAME = @F WHERE GIVENNAME = @G AND SURNAME = @OS";
            Console.Write("Given Name: ");
            string givenname = Console.ReadLine();
            Console.Write("Surname: ");
            string surname = Console.ReadLine();
            Actor A1 = new Actor();
            A1.GIVENNAME = givenname;
            A1.SURNAME = surname;
            A1.setFullName(A1);
            string fullname = A1.FULLNAME;
            updatename.Parameters.AddWithValue("@S", A1.SURNAME);
            updatename.Parameters.AddWithValue("@F", A1.FULLNAME);
            updatename.Parameters.AddWithValue("@G", givenname);
            updatename.Parameters.AddWithValue("@os", surname);


            //Create 1
            Movie M1 = new Movie(Convert.ToInt32(Console.ReadLine()), Console.ReadLine(),Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
            using SqlCommand createmovie = connection.CreateCommand();
            createmovie.CommandText = "INSERT INTO MOVIE (MOVIENO, TITLE, RELYEAR, RUNTIME) VALUES (@M, @T, @RY, @RT)";
            createmovie.Parameters.AddWithValue("@M", M1.MOVIENO);
            createmovie.Parameters.AddWithValue("@T", M1.TITLE);
            createmovie.Parameters.AddWithValue("@RY", M1.RELYEAR);
            createmovie.Parameters.AddWithValue("@RT", M1.RUNTIME);

            //Create 2
            Actor A2 = new Actor(Convert.ToInt32(Console.ReadLine()), Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
            using SqlCommand createactor = connection.CreateCommand();
            createactor.CommandText = "INSERT INTO ACTOR (ActorNo, FullName, GivenName, Surname) VALUES (@A, @F, @G, @S)";
            createactor.Parameters.AddWithValue("@A", A2.ACTORNO);
            createactor.Parameters.AddWithValue("@F", A2.FULLNAME);
            createactor.Parameters.AddWithValue("@G", A2.GIVENNAME);
            createactor.Parameters.AddWithValue("@S", A2.SURNAME);

            //Create 3
            int CastID = 1;
            Casting C1 = new Casting(CastID, A2, M1);
            CastID++;
            using SqlCommand createcasting = connection.CreateCommand();
            createcasting.CommandText = "INSERT INTO CASTING (CASTID, ACTORNO, MOVIENO) VALUES(@C, @A, @M)";
            createcasting.Parameters.AddWithValue("@C", C1.CASTID);
            createcasting.Parameters.AddWithValue("@A", C1.ACTORNO);
            createcasting.Parameters.AddWithValue("@M", C1.MOVIENO);
        }

    }
}