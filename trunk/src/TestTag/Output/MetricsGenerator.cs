//  Copyright (C) 2012 - TestTag Project
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestTag.Output
{
    public class MetricsGenerator : ITestOutputGenerator
    {
        private TextWriter writer;

        public MetricsGenerator(TextWriter streamWriter)
        {
            this.writer = streamWriter;
        }

        public void Write(TestPlan plan)
        {
            WriteGlobalMetrics(plan);
            WriteSuiteMetrics(plan);
            WriteStepsMetrics(plan);
        }

        private void WriteStepsMetrics(TestPlan plan)
        {
            if (!plan.Suites.Any() || !plan.Suites.All(s => s.TestCases.Any()))
                return;
            writer.WriteLine("STEPS BY TEST CASE:");
            int totalSteps = plan.Suites.Sum(s => s.TestCases.Sum(t => t.Steps.Count()));
            int totalTCs = plan.Suites.Sum(s => s.TestCases.Count);
            writer.WriteLine(" - Average: {0}", totalSteps / totalTCs);

            int maxSteps = plan.Suites.Max(s => s.TestCases.Max(t => t.Steps.Count()));
            var testCaseWithMax = GetTCs(plan).First(t => t.Steps.Count() == maxSteps);
            writer.WriteLine(" - Maximum: {0} ({2} -> {1})", maxSteps, testCaseWithMax.Name, plan.Suites.First(s => s.TestCases.Contains(testCaseWithMax)).Name);

            int minSteps = plan.Suites.Min(s => s.TestCases.Min(t => t.Steps.Count()));
            var testCaseWithMin = GetTCs(plan).First(t => t.Steps.Count() == minSteps);
            writer.WriteLine(" - Minimum: {0} ({2} -> {1})", minSteps, testCaseWithMin.Name, plan.Suites.First(s => s.TestCases.Contains(testCaseWithMin)).Name);
        }

        private IEnumerable<TestCase> GetTCs(TestPlan plan)
        {
            var ret = new List<TestCase>();
            plan.Suites.ForEach(s => ret.AddRange(s.TestCases));
            return ret;
        }

        private void WriteSuiteMetrics(TestPlan plan)
        {
            if (!plan.Suites.Any() || !plan.Suites.All(s => s.TestCases.Any()))
                return;
            writer.WriteLine("TEST CASES BY SUITE:");
            writer.WriteLine(" - Average: {0}", plan.Suites.Average(s => s.TestCases.Count));
            int maxCasesBySuite = plan.Suites.Max(s => s.TestCases.Count);
            int minCasesBySuite = plan.Suites.Min(s => s.TestCases.Count);
            writer.WriteLine(" - Maximum: {0} ({1})", maxCasesBySuite, plan.Suites.First(s => s.TestCases.Count == maxCasesBySuite).Name);
            writer.WriteLine(" - Minimum: {0} ({1})", minCasesBySuite, plan.Suites.First(s => s.TestCases.Count == minCasesBySuite).Name);
            writer.WriteLine();
        }

        private void WriteGlobalMetrics(TestPlan plan)
        {
            writer.WriteLine("TEST PLAN METRICS:");
            writer.WriteLine(" - {0} suites", plan.Suites.Count);
            writer.WriteLine(" - {0} test cases", plan.Suites.Sum(s => s.TestCases.Count));
            writer.WriteLine(" - {0} steps", plan.Suites.Sum(s => s.TestCases.Sum(t => t.Steps.Count())));
            writer.WriteLine();
        }

        public void Dispose()
        {
            writer.Dispose();
        }
    }
}
