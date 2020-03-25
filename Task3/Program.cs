using System;


namespace Task3
{
    public class SlobodaFriends
    {
        private uint numberOfPeople;
        private uint specificPersonId;
        private byte[,] friendsArray;

        public void InitFromConsole()
        {
            Console.WriteLine("Please enter the number N of people in company: ");
            numberOfPeople = GetUIntFromConsole();
            Console.WriteLine("Please enter the number S of specific person: ");
            specificPersonId = GetUIntFromConsole();
            while (specificPersonId > numberOfPeople)
            {
                Console.WriteLine("The number of specific person should be smaller or equal than amount of people in company, please try again");
                specificPersonId = GetUIntFromConsole();
            }
            specificPersonId--;
            friendsArray = new byte[numberOfPeople, numberOfPeople];
            GetArrayFromConsole(friendsArray);
            WriteArrayToConsole(friendsArray);
        }
        public void Init(uint numberOfPeople, uint specificPersonId, byte[,] friendsArray)
        {


            this.numberOfPeople = numberOfPeople;
            Console.WriteLine($"Number of people in company is: {this.numberOfPeople}");
            this.specificPersonId = specificPersonId;
            Console.WriteLine($"Number of specific person is: {this.specificPersonId}");
            this.friendsArray = new byte[numberOfPeople, numberOfPeople];
            if (numberOfPeople < specificPersonId)
            {
                throw new Exception("Incorrect, The number of specific person should be smaller or equal than amount of people in company");
            }

            for (uint i = 0; i < friendsArray.GetLength(0); i++)
            {
                for (uint j = 0; j < friendsArray.GetLength(1); j++)
                {
                    if (friendsArray[i, j] != 1 && friendsArray[i, j] != 0)
                    {
                        throw new Exception("Incorrect, the array of friends should include only 0 and 1");
                    }
                }
            };
            for (uint i = 0; i < friendsArray.GetLength(0); i++)
            {
                for (uint j = 0; j < friendsArray.GetLength(1); j++)
                {
                    this.friendsArray[i, j] = friendsArray[i, j];
                }
            }
            WriteArrayToConsole(this.friendsArray);
        }


        public uint CountFriends()
        {
            uint numberOfFriends = 0;
            bool[] checkedPerson = new bool[numberOfPeople];
            for (uint i = 0; i < numberOfPeople; i++)
            {
                checkedPerson[i] = specificPersonId == i;
            }

            CheckForFriends(checkedPerson, friendsArray, ref numberOfFriends, specificPersonId);
            return numberOfFriends;
        }

        private void CheckForFriends(bool[] checkedArray, byte[,] workArray, ref uint counter, uint personId)
        {
            for (uint i = 0; i < workArray.GetLength(0); i++)
            {
                if (workArray[personId, i] == 1 && checkedArray[i] == false)
                {
                    counter++;
                    checkedArray[i] = true;
                    CheckForFriends(checkedArray, workArray, ref counter, i);
                }
            }
        }

        private void GetArrayFromConsole(byte[,] array)
        {

            for (uint i = 0; i < array.GetLength(0); i++)
            {
                for (uint j = 0; j < array.GetLength(1); j++)
                {
                    Console.WriteLine($"Enter {i + 1}, {j + 1} element");
                    array[i, j] = GetArrayElementFromConsole();
                }
            }
        }

        private void WriteArrayToConsole(byte[,] array)
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

        private uint GetUIntFromConsole()
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

        private byte GetArrayElementFromConsole()
        {
            byte value = 2;
            bool valueIsCorrect = false;
            while (!valueIsCorrect)
            {
                valueIsCorrect = byte.TryParse(Console.ReadLine(), out value) && (value == 0 || value == 1);
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
            byte[,] arr = new byte[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } };
            uint N = 3;
            uint S = 1;
            a.Init(N, S, arr);
            // a.InitFromConsole();
            Console.WriteLine($"number of friends is: {a.CountFriends()}");
        }


    }
}
