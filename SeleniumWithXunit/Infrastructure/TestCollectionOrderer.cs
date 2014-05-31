using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SeleniumWithXunit.Infrastructure
{
    public class TestCollectionOrderer : ITestCollectionOrderer
    {
        public IEnumerable<IGrouping<ITestCollection, TTestCase>> OrderTestCollections<TTestCase>(IEnumerable<IGrouping<ITestCollection, TTestCase>> testCollections) where TTestCase : ITestCase
        {
            return testCollections.OrderByDescending(GetExecutionTimeFor);
        }

        private TimeSpan GetExecutionTimeFor<TTestCase>(IGrouping<ITestCollection, TTestCase> testCollection) where TTestCase : ITestCase
        {
            //A few possible options:
            //1. Have a hardcoded list of test execution times from your CI build
            //2. Decorate each test with an attribute that specifies the execution time
            //3. Retrive the result of the previous CI build (e.g. via API) and sort the tests in such a way that
            //   failed tests are executed first and then tests with longest excution time.
            return TimeSpan.FromSeconds(testCollection.Key.DisplayName.Length);
        }
    }
}