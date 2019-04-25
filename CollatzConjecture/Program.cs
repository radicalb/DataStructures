using System;

namespace CollatzConjecture
{
    class Program
    {
        static void Main(string[] args)
        {
            collazConjR(17);

            /* BITWISE OPERATIONS
            int x = 128;
            Console.WriteLine("x={0}, x>>1 = x/(2^1) = x/2 {1}", x, x>>1 );
            Console.WriteLine("x={0}, x>>2 = x/(2^2) = x/4 {1}", x, x >> 2);

            Console.WriteLine("x^1 = abcd efgh AND 0000 0001 = 0000 000h = h = 0/1 => even/odd");

            x = 10;

            Console.WriteLine("x={0}, x<<2 = x*(2^2) = x*4 {1}", x, x << 2);
            */

        }

        /// <summary>
        /// Metoda prikaže Collatz Conjecture za dano številko. 
        /// Uporabljen je iterativni pristop.
        /// Metoda se prekine, ko x doseže 1, ker je od tam naprej CC
        /// neskončna zanka.
        /// </summary>
        /// <param name="x"></param>
        static void collazConj(int x)
        {

            while (x != 1)
            {
                if (x % 2 == 0)
                {
                    Console.Write($"{x}/2=");
                    x /= 2;
                    Console.WriteLine(x);
                }
                else
                {
                    Console.Write($"{x}*3+1=");
                    x *= 3;
                    x++;
                    Console.WriteLine(x);
                }
            }
        }

        /// <summary>
        /// Metoda prikaže Collatz Conjecture za dano številko. 
        /// Uporabljen je rekurzivni pristop.
        /// Metoda se prekine, ko x doseže 1, ker je od tam naprej CC
        /// neskončna zanka.
        /// </summary>
        /// <param name="x"></param>
        static void collazConjR(int x)
        {
            if (x == 1)
            {
                Console.WriteLine("Prišel do 1, prekinjam zanko.");
            }
            else
            {
                if ((x & 1) == 1)
                {
                    int res = x * 3 + 1;
                    Console.WriteLine($"{x}*3+1={res}");
                    collazConjR(res);
                }
                else
                {
                    int res = x /2;
                    Console.WriteLine($"{x}/2={res}");
                    collazConjR(res);
                }

            }

        }
            
        

    }
}
