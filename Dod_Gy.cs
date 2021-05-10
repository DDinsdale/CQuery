using System;

using Xunit;

namespace Dod_Gy
{
    class Movie{
        
        public int MOVIENO;
        public string TITLE;
        public int RELYEAR;
        public int RUNTIME;
        public int NUMACTOR;

        //---------------------

        public Movie(){
            this.MOVIENO = 0;
            this.TITLE = "";
            this.RELYEAR = 0;
            this.RUNTIME= 0;
            this.NUMACTOR = 0;
        } 
        public Movie(int m, string t, int ry, int rt){
            this.MOVIENO = m;
            this.TITLE = t;
            this.RELYEAR = ry;
            this.RUNTIME= rt;
            this.NUMACTOR = 0;
        }  

        //----------------------
    
        public int NumActors(Movie x){
            return x.NUMACTOR;
        }
        public int GetAge(int year, Movie x){
            int difference = (year - x.RELYEAR);
            return difference;
        }

    }

    class Actor{
        public int ACTORNO;
        public string FULLNAME;
        public string GIVENNAME;
        public string SURNAME;

        //--------------------

        public Actor(){
            this.ACTORNO = 0;
            this.FULLNAME = "";
            this.GIVENNAME = "";
            this.SURNAME = "";
        }
        public Actor(int a, string f, string g, string s){
            this.ACTORNO = a;
            this.FULLNAME = f;
            this.GIVENNAME = g;
            this.SURNAME = s;
        }

        //---------------------

        public void setFullName(Actor x){
            x.FULLNAME = x.GIVENNAME + " " + x.SURNAME;
            Console.WriteLine(x.FULLNAME);
        }
    }
    class Casting{
        public int CASTID;
        public int ACTORNO;
        public int MOVIENO;

        public Casting(int c, Actor a, Movie m){
            this.CASTID = c;
            this.ACTORNO = a.ACTORNO;
            this.MOVIENO = m.MOVIENO;
            m.NUMACTOR++;
        }
    }

    
}
    

