using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTag
{
    public interface ITestOutputGenerator
    {
        void Write(TestPlan plan);
        void Write(TestSuite suite);
        void Write(TestCase testCase);
        void Write(TestStep testCase);
    }
}
