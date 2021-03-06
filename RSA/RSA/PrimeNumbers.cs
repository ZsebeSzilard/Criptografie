﻿using System;
using System.Collections.Generic;

namespace RSA
{
    class PrimeNumbers
    {
        private const string SOURCE = "resource/prime_numbers.txt";
        private const int SIZE = 200;
        private int[] numbers;
        private Random random;

        public PrimeNumbers()
        {
            numbers = new int[SIZE];
            random = new Random();
            FileOperation fo = new FileOperation();
            fo.FileName = SOURCE;
            IEnumerable<string> nums = fo.Read();
            int i = 0;

            foreach (string x in nums)
            {
                numbers[i++] = int.Parse(x);
            }
        }

        private bool IsPrime(int a)
        {
            bool r = true;

            if (a >= numbers[0] && a <= numbers[SIZE - 1])
            {
                for (int i = 0; i < SIZE; i++)
                {
                    if (numbers[i] == a)
                    {
                        break;
                    }
                }
            }

            else
            {
                int limit = (int)Math.Sqrt(a);

                for (int i = 2; i <= limit; i++)
                {
                    if (a % i == 0)
                    {
                        r = false;
                        break;
                    }
                }
            }

            return r;
        }

        public int RandomPrime()
        {
            int index = random.Next(0, SIZE);
            return numbers[index];
        }
    }
}