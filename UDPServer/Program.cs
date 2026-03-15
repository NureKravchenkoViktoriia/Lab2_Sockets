using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Налаштовуємо кінцеву точку для прослуховування всіх інтерфейсів на порту 8000
            IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Any, 8000);

            // Створюємо UDP сокет 
            Socket serverSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // Зв'язуємо сокет з локальною кінцевою точкою
            serverSock.Bind(ipEndpoint);

            Console.WriteLine("UDP Сервер запущено. Очікування повідомлень...");

            // Безкінечний цикл для прийому дейтаграм від різних клієнтів
            while (true)
            {
                byte[] buffer = new byte[1024];

                // Змінна для збереження адреси клієнта, від якого прийде пакет
                EndPoint remoteClient = new IPEndPoint(IPAddress.Any, 0);

                // Отримуємо дані ТА запам'ятовуємо адресу відправника 
                int bytesRead = serverSock.ReceiveFrom(buffer, ref remoteClient);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"\nОтримано повідомлення від {remoteClient}: {receivedMessage}");

                // Рахуємо літери 'a' та 'а'
                int countA = receivedMessage.Count(c => c == 'a' || c == 'A' || c == 'а' || c == 'А');
                string responseMessage = $"Кількість літер 'а': {countA}";

                // Відправляємо результат назад саме тому клієнту, який надіслав запит
                byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                serverSock.SendTo(responseData, remoteClient);
                Console.WriteLine("Відповідь відправлено.");
            }
        }
    }
}