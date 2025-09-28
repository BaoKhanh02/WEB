using System;
using MemoryGameLibrary;

namespace MemoryGameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryGame game = new MemoryGame();

            Console.WriteLine("===== Memory Game (Console) =====");
            Console.WriteLine("Nhap so cap the (vi du 4 = 8 the): ");
            int pairs;
            while (!int.TryParse(Console.ReadLine(), out pairs) || pairs <= 0)
            {
                Console.Write("Nhap so nguyen duong: ");
            }

            game.Init(pairs);

            while (!game.IsWin())
            {
                Console.Clear();
                Console.WriteLine("===== Memory Game =====");
                Console.WriteLine("Thoi gian: {0} giay | Diem: {1}", game.TimeElapsed, game.Score);

                // In trang thai ban
                for (int i = 0; i < pairs * 2; i++)
                {
                    string val = game.GetCardValue(i);
                    Console.Write("[{0}] ", val); // hien gia tri cho don gian
                }
                Console.WriteLine("\n");

                // Nguoi choi chon 2 the
                Console.Write("Chon the thu 1 (0..{0}): ", pairs * 2 - 1);
                int c1 = int.Parse(Console.ReadLine());

                Console.Write("Chon the thu 2 (0..{0}): ", pairs * 2 - 1);
                int c2 = int.Parse(Console.ReadLine());

                string r1 = game.FlipCard(c1);
                string r2 = game.FlipCard(c2);

                if (r2 == "MATCH")
                {
                    Console.WriteLine("Ban da tim duoc mot cap!");
                }
                else if (r2 == "FAIL")
                {
                    Console.WriteLine("Sai roi, thu lai nhe!");
                }
                else
                {
                    Console.WriteLine("Chua du 2 the de kiem tra.");
                }

                Console.WriteLine("Nhan phim bat ky de tiep tuc...");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine("Chuc mung, ban da thang!");
            Console.WriteLine("Thoi gian: {0} giay", game.TimeElapsed);
            Console.WriteLine("Diem: {0}", game.Score);
            Console.WriteLine("Nhan Enter de thoat...");
            Console.ReadLine();
        }
    }
}
