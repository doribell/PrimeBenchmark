using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;

namespace PrimeBenchmark
{
    internal class Program
    {
        private static void Main()
        {
            BenchmarkRunner.Run<PrimeBenchmark>();
        }

        [MemoryDiagnoser]
        [Orderer(SummaryOrderPolicy.FastestToSlowest)]
        [RankColumn]
        public class PrimeBenchmark
        {
            [Benchmark(Baseline = true)]
            public void Prime1()
            {
                var primes = new List<int>(80000);

                for (var prim = 3; prim <= 1000000; prim++)
                    if (IsPrime1(prim))
                        primes.Add(prim);
            }

            [Benchmark]
            public void Prime2()
            {
                var primes = new List<int>(80000);

                for (var prim = 3; prim <= 1000000; prim++)
                    if (IsPrime2(prim))
                        primes.Add(prim);
            }

            private static bool IsPrime1(int number)
            {
                if (number <= 1) return false;
                if (number == 2) return true;
                if (number % 2 == 0) return false;

                var limit = (int)Math.Floor(Math.Sqrt(number));

                for (var i = 3; i <= limit; i += 2)
                    if (number % i == 0)
                        return false;

                return true;
            }

            private static bool IsPrime2(int number)
            {
                if (number == 1) return false;
                if (number == 2) return true;

                var limit = Math.Ceiling(Math.Sqrt(number));

                for (var i = 2; i <= limit; ++i)
                    if (number % i == 0)
                        return false;

                return true;
            }
        }
    }
}
