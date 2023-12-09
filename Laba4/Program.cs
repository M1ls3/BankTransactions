using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;

namespace Laba4
{
    class Program
    {

        static Order[] ArrayWithOrders()
        {
            Console.WriteLine("Input amount of transaction: ");
            string k = null;
            int p;
            string value;
            int amount = int.Parse(Console.ReadLine());
            Order[] transaction = new Order[amount];
            for (int i = 0; i < transaction.Length; i++)
            {
                Console.WriteLine($"\nInput the {i + 1} line(Payer's current account, Recipient's current account , Transfer amount.)");
                Console.WriteLine($"According to IBAN (International Bank Account Number) current account consist of 14 numbers.\n");
                value = Console.ReadLine();
                Int32.TryParse(string.Join(null, value.Where(c => Char.IsDigit(c))), out p);
                k += p.ToString();
                transaction[i] = new Order(value);
            }
            return transaction;
        }
        //
        static void InputToTXT(Order[] transaction)
        {
            string data = "data.txt";
            StreamWriter filler = new StreamWriter(data);
            for (int i = 0; i < transaction.Length; i++)
            {
                filler.WriteLine(transaction[i].ToString());
            }
            filler.Close();
        }
        //
        static void InputToXML(Order[] transaction)
        {
            string data = "data.xml";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order[]));
            FileStream file = new FileStream(data, FileMode.Truncate);
            xmlSerializer.Serialize(file, transaction);
            file.Close();
        }
        //
        static void OutputFromTxtAllData()
        {
            string data = "data.txt";
            StreamReader reader = new StreamReader(data);
            int amount = System.IO.File.ReadAllLines(data).Length;
            Order[] transaction = new Order[amount];
            for (int i = 0; i < amount; i++)
            {
                transaction[i] = new Order(reader.ReadLine());
                Console.WriteLine(transaction[i].Show());
            }
            reader.Close();
        }
        //
        static void OutputFromTxtNeedData()
        {
            Console.WriteLine("Input current account: ");
            ulong input_num = UInt64.Parse(Console.ReadLine());
            string data = "data.txt";
            bool flag = false;
            StreamReader reader = new StreamReader(data);
            int amount = System.IO.File.ReadAllLines(data).Length;
            Order[] transaction = new Order[amount];
            for (int i = 0; i < amount; i++)
            {
                transaction[i] = new Order(reader.ReadLine());
                if (transaction[i].payAccount == input_num)
                {
                    flag = true;
                    Console.WriteLine(transaction[i].Show());
                }
                else if (transaction[i].receiveAccount == input_num)
                {
                    flag = true;
                    Console.WriteLine(transaction[i].Show());
                }
            }
            if (!flag)
            {
                Console.WriteLine($"Current account not exist.");
            }
            reader.Close();
        }
        //
        static void OutputFromXmlAllData()
        {
            string data = "data.xml";
            FileStream file = new FileStream(data, FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order[]));
            Order[] transaction = (Order[])xmlSerializer.Deserialize(file);
            for (int i = 0; i < transaction.Length; i++)
            {
                Console.WriteLine(transaction[i].Show());
            }
            file.Close();
        }
        //
        static void OutputFromXMLNeedData()
        {
            string data = "data.xml";
            Console.WriteLine("Input current account: ");
            ulong input_num = UInt64.Parse(Console.ReadLine());
            bool flag = false;
            FileStream file = new FileStream(data, FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order[]));
            Order[] transaction = (Order[])xmlSerializer.Deserialize(file);
            for (int i = 0; i < transaction.Length; i++)
            {
                if (transaction[i].payAccount == input_num)
                {
                    flag = true;
                    Console.WriteLine(transaction[i].Show());
                }
                else if (transaction[i].receiveAccount == input_num)
                {
                    flag = true;
                    Console.WriteLine(transaction[i].Show());
                }
            }
            if (!flag)
            {
                Console.WriteLine($"Current account not exist.");
            }
            file.Close();
        }
        //
        static void RunInput()
        {
            Console.Clear();
            Order[] transaction = ArrayWithOrders();
            Array.Sort(transaction);
            InputToTXT(transaction);
            InputToXML(transaction);
        }
        //
        static void OutputMenu()
        {
        savepoint:
            Console.Clear();
            Console.WriteLine("1 - View txt file.");
            Console.WriteLine("2 - View xml file.");
            Console.WriteLine("0 - Exit to the main menu.");
            int key, key2;
            do
            {
                key = Int32.Parse(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("1 - View all transactions.");
                        Console.WriteLine("2 - View transaction on current account.");
                        Console.WriteLine("0 - Exit to the previous menu.");
                        do
                        {
                            key2 = Int32.Parse(Console.ReadLine());
                            switch (key2)
                            {
                                case 1:
                                    OutputFromTxtAllData();
                                    break;
                                case 2:
                                    OutputFromTxtNeedData();
                                    break;
                                case 0:
                                    goto savepoint;

                            }
                        } while (key2 != 0);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("1 - View all transactions.");
                        Console.WriteLine("2 - View transaction on current account.");
                        Console.WriteLine("0 - Exit to the previous menu.");
                        do
                        {
                            key2 = Int32.Parse(Console.ReadLine());
                            switch (key2)
                            {
                                case 1:
                                    OutputFromXmlAllData();
                                    break;
                                case 2:
                                    OutputFromXMLNeedData();
                                    break;
                                case 0:
                                    goto savepoint;
                            }
                        } while (key2 != 0);
                        break;
                    case 0:
                        RunMenu();
                        break;
                    default: goto savepoint;

                }
            } while (key != 0);
        }
        //
        static void RunMenu()
        {
        savepoint:
            Console.Clear();
            Console.WriteLine("1 - Input data.");
            Console.WriteLine("2 - View data.");
            Console.WriteLine("0 - Exit.");
            int key;
            do
            {
                key = Int32.Parse(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        RunInput();
                        goto savepoint;
                    case 2:
                        OutputMenu();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default: goto savepoint;
                }
            } while (key != 0);

        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            RunMenu();
        }
    }
}
