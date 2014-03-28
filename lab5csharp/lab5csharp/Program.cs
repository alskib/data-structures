using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace lab5csharp
{
    class Program
    {
        private const String filepath = "C:\\Users\\franklin\\Documents\\My Dropbox\\class\\cosc3319\\labs\\lab5csharp\\lab5data.txt";

        static void NotMain(string[] args)
        {
            int menuInput = 1;
            double fillPercent, alpha, E;
            int tableSize, fillAmount;
            String input;
            StreamReader reader;
            String[] hashTable;

            while (menuInput != 0)
            {
                Console.WriteLine("Lab 5 menu");
                Console.WriteLine("1. C-A (Linear Insert (55%)");
                Console.WriteLine("2. C-B (Linear Insert (93%)");
                Console.WriteLine("3. C-Ca (Random Insert (55%)");
                Console.WriteLine("4. C-Cb (Random Insert (93%)");
                Console.WriteLine("5. My hash - linear probe (55%)");
                Console.WriteLine("6. My hash - linear probe (93%)");
                Console.WriteLine("7. My hash - random probe (55%)");
                Console.WriteLine("8. My hash - random probe (93%)");
                Console.WriteLine("0. Exit");
                menuInput = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (menuInput)
                {
                    case 0:
                        break;
                    case 1:
                        tableSize = 256;
                        fillPercent = .55;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = LinearInsert(fillAmount, tableSize, reader);
                        //for (int i = 0; i < hashTable.Length; i++)
                            //Console.WriteLine(hashTable[i]);
                        reader = File.OpenText(filepath);
                        LinearProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        LinearProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    case 2:
                        tableSize = 256;
                        fillPercent = .93;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = LinearInsert(fillAmount, tableSize, reader);
                        reader = File.OpenText(filepath);
                        LinearProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        LinearProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    case 3:
                        tableSize = 256;
                        fillPercent = .55;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = RandomInsert(fillAmount, tableSize, reader);
                        reader = File.OpenText(filepath);
                        RandomProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        RandomProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    case 4:
                        tableSize = 256;
                        fillPercent = .93;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = RandomInsert(fillAmount, tableSize, reader);
                        reader = File.OpenText(filepath);
                        RandomProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        RandomProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    case 5:
                        tableSize = 256;
                        fillPercent = .55;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = myInsert(fillAmount, tableSize, reader);
                        reader = File.OpenText(filepath);
                        myLinearProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        myLinearProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    case 6:
                        tableSize = 256;
                        fillPercent = .93;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = myInsert(fillAmount, tableSize, reader);
                        reader = File.OpenText(filepath);
                        myLinearProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        myLinearProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    case 7:
                        tableSize = 256;
                        fillPercent = .55;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = myInsert(fillAmount, tableSize, reader);
                        reader = File.OpenText(filepath);
                        myRandomProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        myRandomProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    case 8:
                        tableSize = 256;
                        fillPercent = .93;
                        fillAmount = (int)(Math.Ceiling((double)(tableSize * fillPercent)));
                        Console.WriteLine("Filling hash table to {0}%, or {1} places.", fillPercent * 100, fillAmount);

                        reader = File.OpenText(filepath);
                        hashTable = myInsert(fillAmount, tableSize, reader);
                        reader = File.OpenText(filepath);
                        myRandomProbe(1, 40, hashTable, reader);

                        for (int i = 41; i < fillAmount - 40; i++)
                            input = reader.ReadLine();

                        myRandomProbe(fillAmount - 40, fillAmount, hashTable, reader);
                        printTable(hashTable);
                        alpha = (double)fillAmount / tableSize;
                        E = (double)(1 - alpha / 2) / (1 - alpha);
                        Console.WriteLine("Load factor {0} equates to {1} expected probes.", alpha, E);
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }

        public static String[] myInsert(double fillAmount, int tableSize, StreamReader reader)
        {
            String[] hashTable = new String[tableSize];
            String input;
            int r, char13, char14, char15, char16, hashAddress;
            bool done = false;
            r = 1;
            for (int i = 0; i < fillAmount; i++)
            {
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[0]);
                char14 = Convert.ToInt32(input[1]);
                char15 = Convert.ToInt32(input[2]);
                char16 = Convert.ToInt32(input[3]);

                hashAddress = (char13 * char14 + char15 * char16) & tableSize;
                done = false;
                while (!done)
                {
                    if (hashTable[hashAddress] == input)
                    {
                        Console.WriteLine("Hash address: " + hashAddress);
                        done = true;
                    }
                    if (hashTable[hashAddress] == null)
                    {
                        hashTable[hashAddress] = input;
                        done = true;
                    }
                    else
                    {
                        r = RandomGenerator(r);
                        hashAddress = hashAddress + r / 4;
                        if (hashAddress >= 256)
                            hashAddress = hashAddress - 256;
                        else
                            hashAddress++;
                    }
                }
            }
            return hashTable;
        }

        public static void myLinearProbe(int initAmount, int endAmount, String[] hashTable, StreamReader reader)
        {
            Console.WriteLine("probing for items {0} - {1}.", initAmount, endAmount);
            int minNumProbes = 1;
            int maxNumProbes = 0;
            double avgNumProbes = 0;
            int totalProbes = 0;
            int numProbes, char0, char1, char2, char3, hashAddress;
            String input;

            for (int i = 0; i <= endAmount; i++)
            {
                numProbes = 1;
                input = reader.ReadLine();

                char0 = Convert.ToInt32(input[0]);
                char1 = Convert.ToInt32(input[1]);
                char2 = Convert.ToInt32(input[2]);
                char3 = Convert.ToInt32(input[3]);

                hashAddress = (char0 * char1 + char2 * char3) % 256;

                bool done = false;
                while (!done)
                {
                    if (hashTable[hashAddress] == input)
                    {
                        if (numProbes < minNumProbes)
                            minNumProbes = numProbes;
                        if (numProbes > maxNumProbes)
                            maxNumProbes = numProbes;
                        done = true;
                    }
                    else
                    {
                        if (hashAddress == hashTable.Length - 1)
                            hashAddress = 1;
                        else
                            hashAddress++;
                        numProbes++;
                        totalProbes++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / 40;
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine();
        }

        public static void myRandomProbe(int initAmount, int endAmount, String[] hashTable, StreamReader reader)
        {
            Console.WriteLine("Probing for items {0} - {1}.", initAmount, endAmount);
            String input;
            int r, numProbes, char0, char1, char2, char3, hashAddress;
            double avgNumProbes;
            int minNumProbes = 1;
            int maxNumProbes = 0;
            int totalProbes = 0;

            for (int i = 0; i <= endAmount; i++)
            {
                r = 1;
                numProbes = 1;
                input = reader.ReadLine();

                char0 = Convert.ToInt32(input[0]);
                char1 = Convert.ToInt32(input[1]);
                char2 = Convert.ToInt32(input[2]);
                char3 = Convert.ToInt32(input[3]);

                hashAddress = (char0 * char1 + char2 * char3) % 256;

                bool done = false;
                while (!done)
                {
                    if (hashTable[hashAddress] == input)
                    {
                        if (numProbes < minNumProbes)
                            minNumProbes = numProbes;
                        if (numProbes > maxNumProbes)
                            maxNumProbes = numProbes;
                        done = true;
                    }
                    else
                    {
                        numProbes++;
                        r = RandomGenerator(r);
                        hashAddress = hashAddress + r / 4;
                        if (hashAddress >= 256)
                            hashAddress = hashAddress - 256;
                        totalProbes++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / 40;
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine();
        }

        public static String[] LinearInsert(double fillAmount, int tableSize, StreamReader reader)
        {
            String[] hashTable = new String[tableSize];
            String input;
            int char13, char14, char15, char16, hashAddress;
            bool done = false;

            for (int i = 0; i < fillAmount; i++)
            {
                input = reader.ReadLine();
                Console.WriteLine(input);

                char13 = Convert.ToInt32(input[12]);
                char14 = Convert.ToInt32(input[13]);
                char15 = Convert.ToInt32(input[14]);
                char16 = Convert.ToInt32(input[15]);

                hashAddress = (char13 * char14 + char15 * char16) % tableSize;
                Console.WriteLine(hashAddress);
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

        public static void LinearProbe(int initAmount, int endAmount, String[] hashTable, StreamReader reader)
        {
            Console.WriteLine("probing for items {0} - {1}.", initAmount, endAmount);
            int minNumProbes = 1;
            int maxNumProbes = 0;
            double avgNumProbes = 0;
            int totalProbes = 0;
            int numProbes, char13, char14, char15, char16, hashAddress;
            String input;

            for (int i = 0; i <= endAmount; i++)
            {
                numProbes = 1;
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[12]);
                char14 = Convert.ToInt32(input[13]);
                char15 = Convert.ToInt32(input[14]);
                char16 = Convert.ToInt32(input[15]);

                hashAddress = (char13 * char14 + char15 * char16) % 256;

                bool done = false;
                while (!done)
                {
                    if (hashTable[hashAddress] == input)
                    {
                        if (numProbes < minNumProbes)
                            minNumProbes = numProbes;
                        if (numProbes > maxNumProbes)
                            maxNumProbes = numProbes;
                        done = true;
                    }
                    else
                    {
                        if (hashAddress == hashTable.Length - 1)
                            hashAddress = 1;
                        else
                            hashAddress++;
                        numProbes++;
                        totalProbes++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / 40;
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine();
        }

        public static String[] RandomInsert(double fillAmount, int tableSize, StreamReader reader)
        {
            String[] hashTable = new String[tableSize];
            String input;
            int r, char13, char14, char15, char16, hashAddress;
            bool done = false;
            r = 1;

            for (int i = 0; i < fillAmount; i++)
            {
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[12]);
                char14 = Convert.ToInt32(input[13]);
                char15 = Convert.ToInt32(input[14]);
                char16 = Convert.ToInt32(input[15]);

                hashAddress = (char13 * char14 + char15 * char16) & tableSize;
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
                        r = RandomGenerator(r);
                        hashAddress = hashAddress + r / 4;
                        if (hashAddress >= 256)
                            hashAddress -= 256;
                    }
                }
            }
            return hashTable;
        }

        public static void RandomProbe(int initAmount, int endAmount, String[] hashTable, StreamReader reader)
        {
            Console.WriteLine("probing for items {0} - {1}.", initAmount, endAmount);
            int minNumProbes = 1;
            int maxNumProbes = 0;
            double avgNumProbes = 0;
            int totalProbes = 0;
            int r, numProbes, char13, char14, char15, char16, hashAddress;
            String input;

            for (int i = initAmount; i <= endAmount; i++)
            {
                r = 1;
                numProbes = 1;
                input = reader.ReadLine();

                char13 = Convert.ToInt32(input[0]);
                char14 = Convert.ToInt32(input[1]);
                char15 = Convert.ToInt32(input[2]);
                char16 = Convert.ToInt32(input[3]);

                hashAddress = (char13 * char14 + char15 * char16) % 256;

                bool done = false;
                while (!done)
                {
                    if (hashTable[hashAddress] == input)
                    {
                        if (numProbes < minNumProbes)
                            minNumProbes = numProbes;
                        if (numProbes > maxNumProbes)
                            maxNumProbes = numProbes;
                        done = true;
                    }
                    else
                    {
                        r = RandomGenerator(r);
                        hashAddress = hashAddress + r / 4;
                        if (hashAddress >= 256)
                            hashAddress -= 256;
                        numProbes++;
                        totalProbes++;
                    }
                }
            }
            avgNumProbes = (double)totalProbes / 40;
            Console.WriteLine("Min Probes: {0}", minNumProbes);
            Console.WriteLine("Max Probes: {0}", maxNumProbes);
            Console.WriteLine("Avg Probes: {0}", avgNumProbes);
            Console.WriteLine();
        }

        public static int RandomGenerator(double R)
        {
            R = (R * 5) % (Math.Pow(2, 10));
            return (int)R;
        }

        public static void printTable(String[] hashTable)
        {
            Console.WriteLine("Print table? 1 yes, 0 no.");
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == 1)
            {
                Console.WriteLine("HASH TABLE");
                Console.WriteLine("-----------------------------");
                for (int i = 0; i < hashTable.Length; i++)
                    Console.WriteLine("{0} - " + hashTable[i], i);
            }
        }
    }

}
