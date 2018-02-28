using BenchmarkDotNet.Attributes;
using Community.VisualStudio.LayoutManager.Benchmarks.Framework;
using Community.VisualStudio.LayoutManager.Data;

namespace Community.AspNetCore.JsonRpc.Benchmarks.Suites
{
    [BenchmarkSuite(nameof(LayoutPackage))]
    public abstract class LayoutPackageBenchmarks
    {
        private static readonly LayoutPackage _package = new LayoutPackage("p", "v", "c", "l");

        [Benchmark(Description = "oto-str")]
        public void ObjectToString()
        {
            _package.ToString();
        }

        [Benchmark(Description = "get-hsh")]
        public void ObjectGetHashCode()
        {
            _package.GetHashCode();
        }

        [Benchmark(Description = "equ-obj")]
        public void ObjectEquals()
        {
            _package.Equals((object)_package);
        }

        [Benchmark(Description = "equ-equ")]
        public void EquitableEquals()
        {
            _package.Equals(_package);
        }

        [Benchmark(Description = "get-dir")]
        public void GetDirectoryName()
        {
            _package.GetDirectoryName();
        }
    }
}