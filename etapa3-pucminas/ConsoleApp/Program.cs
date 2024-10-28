using System;
using System.IO;
using System.Management;

namespace ComputerInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inventário de Computador");
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inventário de Hardware");
            Console.WriteLine("2. Inventário de Software");
            Console.WriteLine("3. Inventário de Hardware e Software");
            Console.Write("Opção: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    InventoryHardware();
                    break;
                case "2":
                    InventorySoftware();
                    break;
                case "3":
                    InventoryHardware();
                    InventorySoftware();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        static void InventoryHardware()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InventarioHardware.dat");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Inventário de Hardware");
                writer.WriteLine("----------------------");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    writer.WriteLine("Processador: " + obj["Name"]);
                }

                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
                foreach (ManagementObject obj in searcher.Get())
                {
                    writer.WriteLine("Memória RAM: " + Math.Round(Convert.ToDouble(obj["Capacity"]) / (1024 * 1024 * 1024), 2) + " GB");
                }

                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject obj in searcher.Get())
                {
                    writer.WriteLine("Disco Rígido: " + obj["Model"] + " - " + Math.Round(Convert.ToDouble(obj["Size"]) / (1024 * 1024 * 1024), 2) + " GB");
                }

                writer.WriteLine("Inventário de Hardware salvo em: " + filePath);
            }
        }

        static void InventorySoftware()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InventarioSoftware.dat");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Inventário de Software");
                writer.WriteLine("----------------------");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
                foreach (ManagementObject obj in searcher.Get())
                {
                    writer.WriteLine("Software: " + obj["Name"] + " - Versão: " + obj["Version"]);
                }

                writer.WriteLine("Inventário de Software salvo em: " + filePath);
            }
        }
    }
}
