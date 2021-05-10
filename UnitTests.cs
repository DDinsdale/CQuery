using System;
using Xunit;
namespace Dod_Gy
{
    public class UnitTest
        {
        //NumActors
        [Theory]
        [InlineData(1,1)]
        [InlineData(6,6)]
        [InlineData(2,2)]
        [InlineData(9,9)]
        [InlineData(24,24)]
        public void NumActors_Test(int expected, int input){
            Movie M = new Movie();
            Actor A = new Actor();
            for (int i=0; i<input; i++){
                Casting C = new Casting(i, A, M);
            }
            Assert.Equal(expected, M.NumActors(M));
        }

        //GetAge
        [Theory]
        [InlineData(6,2014)]
        [InlineData(1,2019)]
        [InlineData(0,2020)]
        [InlineData(3,2017)]
        [InlineData(22,1998)]
        public void GetAge_Test(int expected, int input){
            Movie M1 = new Movie(1, "Title", input, 180);
            Assert.Equal(expected, M1.GetAge(2020, M1));
        }
    }
}