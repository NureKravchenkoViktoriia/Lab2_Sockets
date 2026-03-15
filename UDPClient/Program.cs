using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо клієнтський UDP сокет
            Socket cliSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Вказуємо адресу сервера, куди будемо кидати дейтаграми
            IPEndPoint serverEndP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);

            Console.Write("Введіть повідомлення для UDP сервера: ");
            string message = Console.ReadLine();
            byte[] pack = Encoding.UTF8.GetBytes(message);

            // Відправляємо дейтаграму на сервер (без попереднього з'єднання)
            cliSock.SendTo(pack, serverEndP);

            // Чекаємо на відповідь від сервера
            byte[] buffer = new byte[1024];
            EndPoint remoteServer = new IPEndPoint(IPAddress.Any, 0);

            int bytesRead = cliSock.ReceiveFrom(buffer, ref remoteServer);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"\nВідповідь від сервера: {response}");

            cliSock.Close();
            Console.WriteLine("\nДля завершення натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}