using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ClientApp_MultiThreadServer_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("\nСоединение # " + i.ToString() + "\n");
                Connect("127.0.0.1", "Привет мир! # " + i.ToString());
            }
            Console.WriteLine("\n Нажмите Enter ...");
            Console.Read();
        }
        static void Connect(String server, String message)
        {
            try
            {
                //Создаём TcpClient
                //Для созданного в прошлом проекте TcpListener
                //Настраиваем его на IP нашего сервера и тот же порт
                Int32 port = 9595;
                TcpClient client = new TcpClient(server, port);
                //Переводим наше сообщение в ASCII, а потом в массив байт
                byte[] data = Encoding.ASCII.GetBytes(message);
                //Получаем  поток для чтения и записи данных 
                NetworkStream stream = client.GetStream();
                //Отправляем сообщение нашему серверу
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Отправлено: {0}", message);
                //Получаем ответ от сервера
                //Буфер для храненя принятого массива bytes
                data = new Byte[256];
                //Строка для хранения полученных ASCII данных
                String responseData = String.Empty;
                //Читаем первый пакет ответа сервера 
                //Можно читать всё сообщение 
                //Для этого надо организовать чтение в цикле, как на сервере
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Получено: {0}", responseData);
                //Закрываем всё 
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("ArgumentNullException: {0}", ex);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: {0}", ex);
            }
        }
    }
}
