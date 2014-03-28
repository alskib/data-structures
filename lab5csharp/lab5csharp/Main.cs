// Franklin Leung
// Lab 5 - COSC 3319
// Fall 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace lab5csharp
{
    class MainClass
    {
        private const String filepath = @"C:\Users\franklin\Documents\My Dropbox\class\cosc3319\labs\lab5csharp\lab5data.txt";

        static void Main(string[] args)
        {
            outputSelector(60, 'l', 'd', 30, 30);
            outputSelector(90, 'l', 'd', 30, 30);
            outputSelector(60, 'l', 'm', 30, 30);
            outputSelector(90, 'l', 'm', 30, 30);
            outputSelector(60, 'r', 'd', 30, 30);
            outputSelector(90, 'r', 'd', 30, 30);
            outputSelector(60, 'r', 'm', 30, 30);
            outputSelector(90, 'r', 'm', 30, 30);

            Console.ReadLine();
        }

        // type1 - 'l' is linear, 'r' is random
        // type2 - 'd' is default hash function, 'm' is my hash function
        public static void outputSelector(int fillPercent, char type1, char type2, int frontNum, int backNum)
        {
            double alpha, E;
            int tableSize = 256;
            StreamReader reader;
            backNum -= 1; // to keep calculation consistent as subtraction would have 1 to many entries with the way my input is done

            int fillAmount = (int)(Math.Ceiling((double)(tableSize * (fillPercent * .01))));

            if (type2 == 'd')
            {
                reader = File.OpenText(filepath);
                if (type1 == 'l')
                {
                    String[] hashTable = LinearInsert(fillPercent, fillAmount, tableSize, reader); // insert data into table
                    reader = File.OpenText(filepath);
                    LinearProbe(1, frontNum, hashTable, reader);
                    reader = File.OpenText(filepath);
                    LinearProbe(fillAmount - backNum, fillAmount, hashTable, reader);
                    //printTable(hashTable);
                }
                if (type1 == 'r')
                {
                    String[] hashTable = RandomInsert(fillPercent, fillAmount, tableSize, reader);
                    reader = File.OpenText(filepath);
                    RandomProbe(1, frontNum, hashTable, reader);
                    reader = File.OpenText(filepath);
                    RandomProbe(fillAmount - backNum, fillAmount, hashTable, reader); 
                    //printTable(hashTable);
                }
                Console.WriteLine();
            }
            else //(type2 == 'm')
            {
                reader = File.OpenText(filepath);
                if (type1 == 'l')
                {
                    String[] hashTable = myLinearInsert(fillPercent, fillAmount, tableSize, reader);
                    reader = File.OpenText(filepath);
                    myLinearProbe(1, frontNum, hashTable, reader);
                    reader = File.OpenText(filepath);
                    myLinearProbe(fillAmount - backNum, fillAmount, hashTable, reader);
                    //printTable(hashTable);
                }
                if (type1 == 'r')
                {
                    String[] hashTable = myRandomInsert(fillPercent, fillAmount, tableSize, reader);
                    reader = File.OpenText(filepath);
                    myRandomProbe(1, frontNum, hashTable, reader);
                    reader = File.OpenText(filepath);
                    myRandomProbe(fillAmount - backNum, fillAmount, hashTable, reader);
                    //printTable(hashTable);
                }
                Console.WriteLine();
            }

            alpha = (double)fillAmount / tableSize;
            if (type1 == 'l')
                E = (1 - alpha / 2) / (1 - alpha);
            else
                E = -1 * (1 / alpha) * Math.Log(1 - alpha);
            Console.WriteLine("Load factor {0}, {1} expected probes.\n", alpha, E);
        }

        public static String[] LinearInsert(float fillPercent, int fillAmount, int tableSize, StreamReader reader)
        {
            String[] hashTable = new String[tableSize];
            String input;
            
            int char13, char14, char15, char16, hashAddress;
            bool done = false;

            Console.WriteLine("Linear Insert: FILLING HASH TABLE {0}% ({1} insertions).\n", fillPercent, fillAmount);

            for (int i = 1; i <= fillAmount; i++)
            {
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[12]);
                char14 = Convert.ToInt32(input[13]);
                char15 = Convert.ToInt32(input[14]);
                char16 = Convert.ToInt32(input[15]);

                hashAddress = (char13 * char14 + char15 * char16) % 256;

                done = false;
                while (!done)
                {
                    if (hashTable[hashAddress] == input)
                        done = true;
                    if (hashTable[hashAddress] == null)
                    {
                        hashTable[hashAddress] = input;
                        done = true;
                    }
                    else
                    {
                        if (hashAddress == tableSize)
                            hashAddress = 1;
                        else
                            hashAddress++;
                    }
                }
            }
            return hashTable;
        }

        public static void LinearProbe(int low, int high, String[] hashTable, StreamReader reader)
        {
            int minNumProbes = 1;
            int maxNumProbes = 0;
            double avgNumProbes = 0;
            int totalProbes = 0;
            int collisions = 0;
            int numProbes, char13, char14, char15, char16, hashAddress;
            String input;

            Console.WriteLine("Linear Probe: PROBING FOR ITEMS {0} - {1}.", low, high);

            // skip lines
            for (int j = 0; j < low - 1; j++)
                reader.ReadLine();

            for (int i = low; i <= high; i++)
            {
                numProbes = 1;
                
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[12]);
                char14 = Convert.ToInt32(input[13]);
                char15 = Convert.ToInt32(input[14]);
                char16 = Convert.ToInt32(input[15]);

                hashAddress = (char13 * char14 + char15 * char16) % 256;

                while (numProbes <= 256)
                {
                    totalProbes++;
                    if (hashTable[hashAddress] == input)
                    {
                        minNumProbes = Math.Min(minNumProbes, numProbes);
                        maxNumProbes = Math.Max(maxNumProbes, numProbes);
                        break;
                    }
                    else
                    {
                        if (hashAddress == hashTable.Length - 1)
                        {
                            hashAddress = 1;
                        }
                        else
                        {
                            hashAddress++;
                        }
                        numProbes++;
                        collisions++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / (high - low);
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine("Collisions: {0}", collisions);
            Console.WriteLine();
        }

        public static String[] RandomInsert(float fillPercent, int fillAmount, int tableSize, StreamReader reader)
        {
            String[] hashTable = new String[tableSize];
            String input;
            int R = 1;
            int char13, char14, char15, char16, hashAddress;
            bool done = false;

            Console.WriteLine("Random Insert: FILLING HASH TABLE {0}% ({1} insertions).\n", fillPercent, fillAmount);

            for (int i = 0; i < fillAmount; i++)
            {
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[12]);
                char14 = Convert.ToInt32(input[13]);
                char15 = Convert.ToInt32(input[14]);
                char16 = Convert.ToInt32(input[15]);

                hashAddress = (char13 * char14 + char15 * char16) % 256;

                done = false;
                while (!done)
                {
                    if (hashTable[hashAddress] == input)
                        done = true;
                    if (hashTable[hashAddress] == null)
                    {
                        hashTable[hashAddress] = input;
                        done = true;
                    }
                    else
                    {
                        R = RandomOffsetGenerator(R);
                        hashAddress += (int)R / 4;
                        if (hashAddress > 255)
                            hashAddress -= 256;
                    }
                }
            }
            return hashTable;
        }

        public static void RandomProbe(int low, int high, String[] hashTable, StreamReader reader)
        {
            int minNumProbes = 1;
            int maxNumProbes = 0;
            double avgNumProbes = 0;
            int totalProbes = 0;
            int collisions = 0;
            int R = 1;
            int numProbes, char13, char14, char15, char16, hashAddress;
            String input;

            Console.WriteLine("Random Probe: PROBING FOR ITEMS {0} - {1}.", low, high);

            // skip lines
            for (int j = 0; j < low - 1; j++)
                reader.ReadLine();

            for (int i = low; i <= high; i++)
            {
                numProbes = 1;
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[12]);
                char14 = Convert.ToInt32(input[13]);
                char15 = Convert.ToInt32(input[14]);
                char16 = Convert.ToInt32(input[15]);

                hashAddress = (char13 * char14 + char15 * char16) % 256;

                while (numProbes <= 256)
                {
                    Console.WriteLine(input + " at " + hashAddress + "?");
                    
                    totalProbes++;
                    if (hashTable[hashAddress] == input)
                    {
                        minNumProbes = Math.Min(minNumProbes, numProbes);
                        maxNumProbes = Math.Max(maxNumProbes, numProbes);
                        break;
                    }
                    else
                    {
                        R = RandomOffsetGenerator(R);
                        hashAddress += (int)(R / 4);
                        if (hashAddress > 255)
                            hashAddress -= 256;
                        collisions++;
                        numProbes++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / (high - low);
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine("Collisions: {0}", collisions);
            Console.WriteLine();
        }

        public static void printTable(String[] hashTable)
        {
            Console.WriteLine("Print table (Y/N)?");
            try
            {
                char input = Convert.ToChar(Console.ReadLine());
                if (input == 'y' || input == 'Y')
                {
                    Console.WriteLine("HASH TABLE");
                    Console.WriteLine("-----------------------------");
                    for (int i = 0; i < hashTable.Length; i++)
                        Console.WriteLine("{0, 3} - " + hashTable[i], i);
                }
            }
            catch
            {
                Console.WriteLine("Invalid input, assuming no.");
            }

        }

        public static int RandomOffsetGenerator(int R)
        {
            R = (R * 5) % 1024;
            return (int)R;
        }

        public static String[] myLinearInsert(float fillPercent, int fillAmount, int tableSize, StreamReader reader)
        {
            String[] hashTable = new String[tableSize];
            String input;

            int char01, char02, char03, char04, hashAddress;
            bool done = false;

            Console.WriteLine("My Linear Insert: FILLING HASH TABLE {0}% ({1} insertions).\n", fillPercent, fillAmount);

            for (int i = 0; i < fillAmount; i++)
            {
                input = reader.ReadLine();

                char01 = Convert.ToInt32(input[0]);
                char02 = Convert.ToInt32(input[1]);
                char03 = Convert.ToInt32(input[2]);
                char04 = Convert.ToInt32(input[3]);

                hashAddress = ((char01 * 13) * (char02 * 11) + (char03 * 17) * (char04 * 19)) % 256;
                
                done = false;
                while (!done)
                {
                    if (hashAddress > 255)
                        hashAddress %= 256;
                    if (hashTable[hashAddress] == input)
                        done = true;
                    if (hashTable[hashAddress] == null)
                    {
                        hashTable[hashAddress] = input;
                        done = true;
                    }
                    else
                    {
                        if (hashAddress == tableSize)
                            hashAddress = 1;
                        else
                            hashAddress++;
                    }
                }
            }
            return hashTable;
        }

        public static void myLinearProbe(int low, int high, String[] hashTable, StreamReader reader)
        {
            int minNumProbes = 1;
            int maxNumProbes = 0;
            double avgNumProbes = 0;
            int totalProbes = 0;
            int collisions = 0;
            int numProbes, char01, char02, char03, char04, hashAddress;
            String input;

            Console.WriteLine("My Linear Probe: PROBING FOR ITEMS {0} - {1}.", low, high);

            // skip lines
            for (int j = 0; j < low - 1; j++)
                reader.ReadLine();

            for (int i = low; i <= high; i++)
            {
                numProbes = 1;
                input = reader.ReadLine();

                char01 = Convert.ToInt32(input[0]);
                char02 = Convert.ToInt32(input[1]);
                char03 = Convert.ToInt32(input[2]);
                char04 = Convert.ToInt32(input[3]);

                hashAddress = ((char01 * 13) * (char02 * 11) + (char03 * 17) * (char04 * 19)) % 256;

                while (numProbes <= 256)
                {
                    totalProbes++;
                    if (hashTable[hashAddress] == input)
                    {
                        minNumProbes = Math.Min(minNumProbes, numProbes);
                        maxNumProbes = Math.Max(maxNumProbes, numProbes);
                        break;
                    }
                    else
                    {
                        if (hashAddress == hashTable.Length - 1)
                            hashAddress = 1;
                        else
                        {
                            collisions++;
                            hashAddress++;
                        }
                        numProbes++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / (high - low);
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine("Collisions: {0}", collisions);
            Console.WriteLine();
        }

        public static String[] myRandomInsert(float fillPercent, int fillAmount, int tableSize, StreamReader reader)
        {
            String[] hashTable = new String[tableSize];
            String input;
            int R = 1;
            int char01, char02, char03, char04, hashAddress;

            Console.WriteLine("My Random Insert: FILLING HASH TABLE {0}% ({1} insertions).\n", fillPercent, fillAmount);

            for (int i = 0; i < fillAmount; i++)
            {
                input = reader.ReadLine();

                char01 = Convert.ToInt32(input[0]);
                char02 = Convert.ToInt32(input[1]);
                char03 = Convert.ToInt32(input[2]);
                char04 = Convert.ToInt32(input[3]);

                hashAddress = ((char01 * 13) * (char02 * 11) + (char03 * 17) * (char04 * 19)) % 256;

                while (true)
                {
                    if (hashTable[hashAddress] == null)
                    {
                        hashTable[hashAddress] = input;
                        break;
                    }
                    else
                    {
                        R = RandomOffsetGenerator(R);
                        hashAddress += (int)R / 4;
                        if (hashAddress > 255)
                            hashAddress -= 256;
                    }
                }
            }
            return hashTable;
        }

        public static void myRandomProbe(int low, int high, String[] hashTable, StreamReader reader)
        {
            int minNumProbes = 1;
            int maxNumProbes = 0;
            double avgNumProbes = 0;
            int totalProbes = 0;
            int collisions = 0;
            int R = 1;
            int numProbes, char01, char02, char03, char04, hashAddress;
            String input;

            Console.WriteLine("My Random Probe: PROBING FOR ITEMS {0} - {1}.", low, high);

            // skip lines
            for (int j = 0; j < low - 1; j++)
                reader.ReadLine();

            for (int i = low; i <= high; i++)
            {
                numProbes = 0;
                input = reader.ReadLine();

                char01 = Convert.ToInt32(input[0]);
                char02 = Convert.ToInt32(input[1]);
                char03 = Convert.ToInt32(input[2]);
                char04 = Convert.ToInt32(input[3]);

                hashAddress = ((char01 * 13) * (char02 * 11) + (char03 * 17) * (char04 * 19)) % 256;

                while (numProbes <= 256)
                {
                    numProbes++;
                    totalProbes++;
                    if (hashTable[hashAddress] == input)
                    {
                        minNumProbes = Math.Min(minNumProbes, numProbes);
                        maxNumProbes = Math.Max(maxNumProbes, numProbes);
                        break;
                    }
                    else
                    {
                        R = RandomOffsetGenerator(R);
                        hashAddress += (int)(R / 4);
                        if (hashAddress > 255)
                            hashAddress -= 256;
                        collisions++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / (high - low);
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine("Collisions: {0}", collisions);
            Console.WriteLine();
        }
    }
}
