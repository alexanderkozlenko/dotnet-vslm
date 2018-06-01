using BenchmarkDotNet.Attributes;
using Community.VisualStudio.LayoutManager.Data;

namespace Community.AspNetCore.JsonRpc.Benchmarks.Suites
{
    public abstract class LayoutPackageBenchmarks
    {
        private static readonly LayoutPackage _package = new LayoutPackage("p", "v", "c", "l");

        [Benchmark]
        public string ObjectToString()
        {
            return _package.ToString();
        }

        [Benchmark]
        public int ObjectGetHashCode()
        {
            return _package.GetHashCode();
        }

        [Benchmark]
        public bool EquitableEquals()
        {
            return _package.Equals(_package);
        }

        [Benchmark]
        public string GetDirectoryName()
        {
            return _package.GetDirectoryName();
        }
    }
}