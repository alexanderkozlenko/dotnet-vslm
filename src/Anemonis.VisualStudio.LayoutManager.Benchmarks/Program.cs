﻿using System.Linq;

using Anemonis.AspNetCore.JsonRpc.Benchmarks.TestSuites;
using Anemonis.VisualStudio.LayoutManager.Benchmarks.Framework;

using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

using Perfolizer.Horology;

namespace Anemonis.VisualStudio.LayoutManager.Benchmarks
{
    public static class Program
    {
        public static void Main()
        {
            var configuration = ManualConfig.CreateEmpty();

            configuration.AddJob(Job.Default
                .WithWarmupCount(1)
                .WithIterationTime(TimeInterval.FromMilliseconds(250))
                .WithMinIterationCount(15)
                .WithMaxIterationCount(20)
                .WithToolchain(InProcessEmitToolchain.Instance));
            configuration.AddDiagnoser(MemoryDiagnoser.Default);
            configuration.AddColumnProvider(DefaultConfig.Instance.GetColumnProviders().ToArray());
            configuration.AddLogger(ConsoleLogger.Default);
            configuration.AddExporter(new SimpleBenchmarkExporter());
            configuration.SummaryStyle = SummaryStyle.Default
                .WithTimeUnit(TimeUnit.Nanosecond)
                .WithSizeUnit(SizeUnit.B);

            BenchmarkRunner.Run<LayoutPackageBenchmarks>(configuration);
        }
    }
}
