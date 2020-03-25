using System;


namespace Task3
{
    public class SlobodaFriends
    {
        private uint numberOfPeople;
        private uint specificPersonId;
        private byte[,] friendsArray;
        
        public SlobodaFriends ()
        {
            Console.WriteLine("Please enter the number N of people in company: ");
            uint numberOfPeople = GetUIntFromConsole();
            Console.WriteLine("Please enter the number S of specific person: ");
            uint specificPersonId = GetUIntFromConsole();
            if (specificPersonId > numberOfPeople)
            {
                Console.WriteLine("The number of specific person should be smaller or equal than amount of people in company, please try again");
                specificPersonId = GetUIntFromConsole();
            }
            friendsArray = new byte[numberOfPeople, numberOfPeople];
            GetArrayFromConsole(friendsArray);
            WriteArrayToConsole(friendsArray);
        }
      /*  public SlobodaFriends(uint numberOfPeople, uint specificPersonId, byte[,] friendsArray)
        {
            if (numberOfPeople < specificPersonId)
            {
                throw new Exception ("Incorrect, The number of specific person should be smaller or equal than amount of people in company");
            }

            for(uint i=0;i<friendsArray.GetLength(0);i++)
            {
                for(uint j=0; j<friendsArray.GetLength(1);j++)
                {
                    if (friendsArray[i, j] != 1 || friendsArray[i, j] != 0)
                    {
                        throw new Exception("Incorrect, the array of friends should include only 0 and 1");
                    }
                }
            };

            this.numberOfPeople = numberOfPeople;
            this.specificPersonId = specificPersonId;
            this.friendsArray = new byte[numberOfPeople, numberOfPeople];
            for (uint i = 0; i < friendsArray.GetLength(0); i++)
            {
                for (uint j = 0; j < friendsArray.GetLength(1); j++)
                {
                    this.friendsArray[i, j] = friendsArray[i, j];
                }
            }
            WriteArrayToConsole(this.friendsArray);
        }
    */

            public uint CountFriends ()
        {
            uint numberOfFriends = 0;
            bool[] checkedPerson = new bool[numberOfPeople];
            for (uint i = 0; i < numberOfPeople; i++)
            {
                checkedPerson[i] = specificPersonId - 1 == i;
            }
            CheckForFriends(checkedPerson, friendsArray, ref numberOfFriends, specificPersonId-1);
            return numberOfFriends;
        }

        private static void CheckForFriends(bool[] checkedArray, byte[,] workArray, ref uint counter, uint personId)
        {
            for (uint i =0;i<workArray.GetLength(0);i++)
            {
                if(workArray[personId, i]==1 && checkedArray[i]==false)
                {
                    counter++;
                    checkedArray[i] = true;
                    CheckForFriends(checkedArray, workArray, ref counter, i);
                }
            }
        }

        private static void GetArrayFromConsole(byte[,] array)
        {
            
            for (uint i = 0; i < array.GetLength(0); i++)
            {
                for (uint j = 0; j < array.GetLength(1); j++)
                {
                    Console.WriteLine($"Enter {i+1}, {j+1} element");
                    array[i, j] = GetArrayElementFromConsole();
                }
            }
        }

            private static void WriteArrayToConsole (byte[,] array)
        {
            Console.WriteLine("Friends array is: ");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        private static uint GetUIntFromConsole()
        {
            uint value = 0;
            bool valueIsCorrect = false;
            while (!valueIsCorrect)
            {
                valueIsCorrect = uint.TryParse(Console.ReadLine(), out value) && value != 0;
                if (!valueIsCorrect)
                {
                    Console.WriteLine("That's not a correct number, please try again");
                }
            };
            return value;
        }

        private static byte GetArrayElementFromConsole()
        {
            byte value=2;
            bool valueIsCorrect = false;
            while (!valueIsCorrect)
            {
                valueIsCorrect = byte.TryParse(Console.ReadLine(), out value) && (value == 0||value ==1);
                if (!valueIsCorrect)
                {
                    Console.WriteLine("That's not a 0 or 1, please try again");
                }
            };
            return value;
             
        }
    }
        
    
    class Program
    {
        static void Main(string[] args)
        {
            SlobodaFriends a = new SlobodaFriends();
            Console.WriteLine($"number of friends is: {a.CountFriends()}");
        }
    }
}
