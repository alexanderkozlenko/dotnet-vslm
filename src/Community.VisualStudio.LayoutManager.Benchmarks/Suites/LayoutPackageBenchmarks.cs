using BenchmarkDotNet.Attributes;
using Community.VisualStudio.LayoutManager.Data;

namespace Community.AspNetCore.JsonRpc.Benchmarks.Suites
{
    public abstract class LayoutPackageBenchmarks
    {
        private static readonly LayoutPackage _package = new LayoutPackage("p", "v", "c", "l");

        [Benchmark(Description = "oto-str")]
        public string ObjectToString()
        {
            return _package.ToString();
        }

        [Benchmark(Description = "get-hsh")]
        public int ObjectGetHashCode()
        {
            return _package.GetHashCode();
        }

        [Benchmark(Description = "equ-obj")]
        public bool ObjectEquals()
        {
            return _package.Equals((object)_package);
        }

        [Benchmark(Description = "equ-equ")]
        public bool EquitableEquals()
        {
            return _package.Equals(_package);
        }

        [Benchmark(Description = "get-dir")]
        public string GetDirectoryName()
        {
            return _package.GetDirectoryName();
        }
    }
}