using System;
using System.IO;
using System.Management;

namespace ComputerInventory
{
    class Program
    {
        static void Main(string[] args)
        {
         MenuDeOpcoes(); //apresenta menu de opcoes
         Console.WriteLine(" ");
         Console.WriteLine(" ");          
         Console.WriteLine("Programa terminado");
        }

        static void MenuDeOpcoes() //Menu de opções
        {
            bool Funcionando = true;

            while (Funcionando)
           {
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("INVENTÁRIO DE COMPUTADOR");
            Console.WriteLine(" ");
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("  1. Inventário de Hardware");
            Console.WriteLine("  2. Inventário de Software");
            Console.WriteLine("  3. Inventário de Hardware e Software");
            Console.WriteLine("  S. Sair");
            Console.WriteLine(" ");
            Console.Write("OPÇÃO --> ");

            string Opcao = Console.ReadLine()!;

            switch (Opcao)
           {
                case "1":
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");                 
                    Console.WriteLine("Você escolheu a Opção 1.");
                    InventarioDeHardware();
                    break;
                case "2":
                    Console.WriteLine(" ");
                    Console.WriteLine(" "); 
                    Console.WriteLine("Você escolheu a Opção 2.");
                    InventarioDeSoftware();
                    break;
                case "3":
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");                 
                    Console.WriteLine("Você escolheu a Opção 3.");
                    InventarioDeHardware();
                    InventarioDeSoftware();
                    break;
                case "s"or"S":
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");                 
                    Console.WriteLine("Saindo do programa...");
                    Funcionando = false; // Define a variável de controle como false para sair do loop
                    break;
                default:
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");                 
                    Console.WriteLine("Opção inválida.");
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    break;
                }
           }
        }

        static void InventarioDeHardware() //cria arquivo com inventario de hardware
        {
            string arquivoDAT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InventarioHardware.dat");
            using (StreamWriter writer = new StreamWriter(arquivoDAT))
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

                writer.WriteLine("Inventário de Hardware salvo em: " + arquivoDAT);
            }
        }

        static void InventarioDeSoftware() //cria arquivo com inventario de software
        {
            string arquivoDAT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InventarioSoftware.dat");
            using (StreamWriter writer = new StreamWriter(arquivoDAT))
            {
                writer.WriteLine("Inventário de Software");
                writer.WriteLine("----------------------");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
                foreach (ManagementObject obj in searcher.Get())
                {
                    writer.WriteLine("Software: " + obj["Name"] + " - Versão: " + obj["Version"]);
                }

                writer.WriteLine("Inventário de Software salvo em: " + arquivoDAT);
            }
        }
    }
}
