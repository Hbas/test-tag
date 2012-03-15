//  Copyright (C) 2012 - Henrique Borges 
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
using System.Xml;
using System.Text;

namespace TestTag
{
    public class TestCase
    {
        public int Order { get; set; }

        public string Name { get; set; }
        public string Summary { get; set; }

        public List<string> Preconditions { get; private set; }
        public List<string> Tags { get; private set; }
        
        private List<TestStep> steps;
        public IEnumerable<TestStep> Steps
        {
            get { return steps; }
        }


        public TestCase(string name)
            : this()
        {
            Name = name;
        }

        public TestCase()
        {
            Preconditions = new List<string>();
            Tags = new List<string>();
            steps = new List<TestStep>();
            Order = 100;
        }

        public void InsertOnBeginning(IEnumerable<TestStep> steps)
        {
            this.steps.InsertRange(0, steps);
            UpdateStepsNumeration();
        }

        public void Add(TestStep step)
        {
            this.steps.Add(step);
            UpdateStepsNumeration();
        }

        public void Add(IEnumerable<TestStep> steps)
        {
            this.steps.AddRange(steps);
            UpdateStepsNumeration();
        }

        private void UpdateStepsNumeration()
        {
            int stepNumber = 1;
            foreach (var s in Steps)
            {
                s.StepNumber = stepNumber++;
            }
        }



    }
}