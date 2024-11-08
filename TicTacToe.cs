using System;
using System.Threading;
using Collections;

namespace TicTacToeGame
{
    public class TicTacToe
    {
        private ArrayCollection a = new ArrayCollection(10); // สร้าง ArrayCollection สำหรับเก็บสถานะของช่องในกระดานเกม
        private int player = 1; // ติดตามผู้เล่นคนปัจจุบัน (1 = ผู้เล่น, 2 = คอมพิวเตอร์)
        private int status = 0; // สถานะเกม: 0 = เล่นต่อ, 1 = ชนะ, -1 = เสมอ

        public TicTacToe()
        {
            for (int i = 0; i <= 9; i++)
                a.add(i.ToString()[0]); // กำหนดช่องกระดานตั้งแต่ 0 ถึง 9 ด้วยตัวเลขเริ่มต้น
        }

        public void StartGame()
        {
            int choice; // เก็บการเลือกตำแหน่งของผู้เล่น
            do
            {
                Console.Clear(); // ล้างหน้าจอทุกครั้งที่เริ่มรอบใหม่
                Console.WriteLine("Player: X and Computer: O\n"); // แสดงข้อความบอกว่าผู้เล่นคือ X และคอมพิวเตอร์คือ O
                Console.WriteLine(player % 2 == 0 ? "Computer's Turn" : "Player's Turn"); // แสดงว่าตอนนี้เป็นเทิร์นของใคร
                Console.WriteLine(); 
                DisplayBoard(); // แสดงกระดานปัจจุบัน

                if (player % 2 == 0) // ถ้าเป็นเทิร์นของคอมพิวเตอร์
                {
                    choice = GetComputerMove(); // ให้คอมพิวเตอร์เลือกตำแหน่ง
                    if (choice != -1)
                    {
                        Console.WriteLine("Computer chooses position " + choice); // แสดงตำแหน่งที่คอมพิวเตอร์เลือก
                        Thread.Sleep(1000); // หยุดรอหนึ่งวินาทีเพื่อให้การเล่นของคอมพิวเตอร์ดูเป็นธรรมชาติ
                        a.set(choice, 'O'); // ใส่ 'O' ลงในตำแหน่งที่คอมพิวเตอร์เลือก
                    }
                }
                else // ถ้าเป็นเทิร์นของผู้เล่น
                {
                    do
                    {
                        Console.Write("Enter position: "); // ให้ผู้เล่นป้อนตำแหน่งที่ต้องการ
                        if (int.TryParse(Console.ReadLine(), out choice) &&
                            choice >= 1 && choice <= 9 &&
                            (char)a.get(choice) != 'X' && (char)a.get(choice) != 'O') // ตรวจสอบว่าตำแหน่งนั้นเป็นตัวเลขที่ว่าง
                        {
                            a.set(choice, 'X'); // ใส่ 'X' ลงในตำแหน่งที่ผู้เล่นเลือก
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid or occupied position. Try again."); // แสดงข้อความเมื่อผู้เล่นเลือกตำแหน่งที่ไม่ถูกต้องหรือไม่ว่าง
                        }
                    } while (true);
                }

                player++; // เปลี่ยนผู้เล่นเป็นคนถัดไป
                status = CheckWin(); // ตรวจสอบว่ามีผู้ชนะหรือไม่
            }
            while (status == 0); // เล่นต่อไปเรื่อย ๆ จนกว่าจะมีผู้ชนะหรือเสมอ

            Console.Clear();
            DisplayBoard(); // แสดงกระดานครั้งสุดท้ายหลังจบเกม
            Console.WriteLine(status == 1 ? $"Player {(player % 2) + 1} has won" : "Draw"); // แสดงผลลัพธ์เกม
            Console.ReadLine(); // รอให้ผู้เล่นกด Enter เพื่อจบเกม
        }

        private void DisplayBoard()
        {
            // แสดงกระดานเกมในรูปแบบ 3x3
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", a.get(1), a.get(2), a.get(3));
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", a.get(4), a.get(5), a.get(6));
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", a.get(7), a.get(8), a.get(9));
            Console.WriteLine("     |     |      ");
        }

        private int CheckWin()
        {
            if (
                //แนวนอน
                (char)a.get(1) == (char)a.get(2) && (char)a.get(2) == (char)a.get(3) ||
                (char)a.get(4) == (char)a.get(5) && (char)a.get(5) == (char)a.get(6) ||
                (char)a.get(7) == (char)a.get(8) && (char)a.get(8) == (char)a.get(9) ||

                //แนวตั้ง
                (char)a.get(1) == (char)a.get(4) && (char)a.get(4) == (char)a.get(7) ||
                (char)a.get(2) == (char)a.get(5) && (char)a.get(5) == (char)a.get(8) ||
                (char)a.get(3) == (char)a.get(6) && (char)a.get(6) == (char)a.get(9) ||

                //แนวทะแยง
                (char)a.get(1) == (char)a.get(5) && (char)a.get(5) == (char)a.get(9) ||
                (char)a.get(3) == (char)a.get(5) && (char)a.get(5) == (char)a.get(7)
            )
            {
                return 1; // หากมีแถวใดที่เหมือนกันทั้งแถวแสดงว่ามีผู้ชนะ
            }
            else if (
                //เช็คว่าไม่มีช่องไหนว่่างแล้ว หากทุกเงื่อนไข (char)a.get(n) != 'n' เป็นจริง แสดงว่าทุกช่องถูกแทนที่ด้วย 'X' หรือ 'O' และไม่มีช่องว่างเหลืออยู่ ดังนั้นเสมอกัน
                (char)a.get(1) != '1' && (char)a.get(2) != '2' && (char)a.get(3) != '3' &&
                (char)a.get(4) != '4' && (char)a.get(5) != '5' && (char)a.get(6) != '6' &&
                (char)a.get(7) != '7' && (char)a.get(8) != '8' && (char)a.get(9) != '9')
            {
                return -1; // ถ้าทุกช่องเต็มแต่ไม่มีผู้ชนะ แสดงว่าเกมเสมอ
            }
            else
            {
                return 0; // ถ้ายังไม่มีผู้ชนะและยังมีช่องว่าง เกมจะดำเนินต่อไป
            }
        }

        private int GetComputerMove()
        {
            // พยายามหาตำแหน่งที่จะชนะ ถ้าพบให้เลือกตำแหน่งนั้น
            for (int i = 1; i <= 9; i++) 
            {
                if ((char)a.get(i) != 'X' && (char)a.get(i) != 'O') //เช็คว่าช่องที่ตำแหน่ง i ว่างอยู่หรือไม่
                {
                    a.set(i, 'O'); // ทดลองใส่ 'O' ลงในตำแหน่ง i
                    if (CheckWin() == 1) // ตรวจสอบว่าถ้าคอมพิวเตอร์ใส่ 'O' ในตำแหน่งนี้แล้วจะชนะหรือไม่
                    {
                        //ถ้าชนะ ให้คืนค่าตำแหน่ง i เป็นตำแหน่งที่ควรให้คอมพิวเตอร์เล่น และ ยกเลิกการทดสอบช่องถัดไป
                        a.set(i, (char)i.ToString()[0]);
                        return i;
                    }
                    a.set(i, (char)i.ToString()[0]); // ถ้าไม่ใช่ตำแหน่งชนะ ให้คืนค่านั้นกลับเป็นเลขเดิม
                }
            }

            // ถ้าไม่มีตำแหน่งชนะ ลองบล็อกผู้เล่นไม่ให้ชนะ
            for (int i = 1; i <= 9; i++) 
            {
                if ((char)a.get(i) != 'X' && (char)a.get(i) != 'O')
                {
                    a.set(i, 'X'); // ทดลองใส่ 'X' ในตำแหน่ง i เพื่อตรวจสอบว่าผู้เล่นจะชนะหรือไม่
                    if (CheckWin() == 1) // ถ้าผู้เล่นชนะเมื่อใส่ 'X' ในตำแหน่งนี้
                    {
                        a.set(i, 'O'); // คอมพิวเตอร์เลือกช่องนี้ทันทีโดยใส่ 'O' เพื่อป้องกันผู้เล่นชนะ
                        return i; // ส่งคืนค่าตำแหน่ง i เป็นช่องที่คอมพิวเตอร์จะเล่น
                    }
                    a.set(i, (char)i.ToString()[0]); // ถ้าตำแหน่งนี้ไม่ทำให้ผู้เล่นชนะ ให้คืนค่าช่องนั้นกลับเป็นเลขเดิม
                }
            }

            // หากไม่มีตำแหน่งชนะหรือต้องบล็อก เลือกตำแหน่งแบบสุ่มที่ว่างอยู่
            Random rnd = new Random();
            int pos;
            do
            {
                pos = rnd.Next(1, 10); //สุ่มเลือกเลขระหว่าง 1 ถึง 9

            } while ((char)a.get(pos) == 'X' || (char)a.get(pos) == 'O'); //เช็คว่าช่องที่สุ่มมาว่างหรือไม่

            return pos; //ส่งคืนตำแหน่ง pos ที่ว่างอยู่ให้คอมพิวเตอร์เลือก
        }
    }
}
