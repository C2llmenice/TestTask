using System; 


namespace Task3
{
    public class SlobodaFriends
    {
        private uint numberOfPeople;
        private uint specificPersonId;
        private byte[,] friendsArray;
        /*
         * Initializes the number N of people in company, specific person S and the array of friendship from console
         */
        public void Initialize()
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

        /*
         * Function overload
         * Initializes the number N of people in company, specific person S and the array of friendship with input parameters
         */

        public void Initialize(uint numberOfPeople, uint specificPersonId, byte[,] friendsArray)
        {


            this.numberOfPeople = numberOfPeople;
            Console.WriteLine($"Number of people in company is: {this.numberOfPeople}");
            this.specificPersonId = specificPersonId;
            Console.WriteLine($"Number of specific person is: {this.specificPersonId}");
            this.specificPersonId--;
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

        /*
         * Returns a number of friends using the recursive function 'CheckForFriends'
         * Uses a boolean array with values is the specific person being checked or not to avoid circuits
         */

        public uint CountFriends()
        {
            uint numberOfFriends = 0;
            bool[] checkedPerson = new bool[numberOfPeople];
            checkedPerson[specificPersonId] = true;
            CheckForFriends(checkedPerson, friendsArray, ref numberOfFriends, specificPersonId);
            return numberOfFriends;
        }

        /* 
         *Function checkes a row for 1 and that the person isn't checked in the array of checkes, if true increases 
         *the counter of friends, sets the person checked and the function recursively invokes for that person    
         */

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

        /* 
         * Gets an array of 0 and 1 from console using 'GetArrayElementFromConsole()' function 
         */

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

        /*
         * Writes an array to console
         */

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

        /* 
         * Gets a value from console, if it's convertable to unsigned int and the value is not a 0, 
         * returns an unsigned int value, otherwise writes a message asking to enter a new value while not correct.
         */

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

        /* 
         * Gets a value from console, if it's convertable to byte and the value is 0 or 1, returns a byte value 
         * otherwise writes a message asking to enter a new value while not correct. 
         * Needed for friendship array filling. 
         */

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
            //function invokation
            SlobodaFriends test = new SlobodaFriends();
            //Initialize(3, 1, new byte[,] { { 0, 1, 0 }, { 1, 0, 1 }, { 0, 1, 0 } }); //test example
            test.Initialize();
            Console.WriteLine($"number of friends is: {test.CountFriends()}");
        }


    }
}