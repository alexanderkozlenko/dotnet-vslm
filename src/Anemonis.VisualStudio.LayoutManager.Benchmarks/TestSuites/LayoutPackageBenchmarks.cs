using Anemonis.VisualStudio.LayoutManager.Data;

using BenchmarkDotNet.Attributes;

namespace Anemonis.AspNetCore.JsonRpc.Benchmarks.TestSuites
{
    public class LayoutPackageBenchmarks
    {
        private static readonly LayoutPackage _package1 = new LayoutPackage("p", "1", "a", "l");
        private static readonly LayoutPackage _package2 = new LayoutPackage("p", "2", "a", "l");

        [Benchmark]
        public string ObjectToString()
        {
            return _package1.ToString();
        }

        [Benchmark]
        public int ObjectGetHashCode()
        {
            return _package1.GetHashCode();
        }

        [Benchmark]
        public bool EquitableEquals()
        {
            return _package1.Equals(_package2);
        }
    }
}