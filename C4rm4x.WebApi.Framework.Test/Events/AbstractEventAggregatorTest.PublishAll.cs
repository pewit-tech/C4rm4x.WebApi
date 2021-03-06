﻿#region Using

using C4rm4x.Tools.TestUtilities;
using C4rm4x.WebApi.Framework.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace C4rm4x.WebApi.Framework.Test.Events
{
 public partial class AbstractEventAggregatorTest
    {
        [TestClass]
        public class AbstractEventAggregatorPublishAllTest
        {
            #region Helper classes

            public class OtherTestEventData : ApiEventData { }

            #endregion

            [TestMethod, UnitTest]
            public void PublishAll_Removes_All_The_EventData_Items_Of_Specified_Type_From_The_Queue()
            {
                var sut = CreateSubjectUnderTest();

                sut.Enqueue(new TestEventData());
                sut.PublishAll<TestEventData>();

                Assert.IsFalse(sut.DataQueue.Any());
            }

            [TestMethod, UnitTest]
            public void PublishAll_Keeps_The_Rest_Of_The_EventData_Items_In_The_Queue()
            {
                var sut = CreateSubjectUnderTest();

                sut.Enqueue(new OtherTestEventData());
                sut.PublishAll<TestEventData>();

                Assert.IsTrue(sut.DataQueue.Any());
            }

            [TestMethod, UnitTest]
            public void PublishAll_Invokes_OnEventHandler_Of_Every_EventHandler_For_Every_EventData_Item_In_The_Queue_Of_Such_Specified_Type()
            {
                var datas = GetData<TestEventData>(GetRand(10))
                    .ToArray(); // Get the final list
                var handlers = GetHandlers<TestEventData>(GetRand(5))
                    .ToArray(); // Get the final list

                var sut = CreateSubjectUnderTest(handlers);

                foreach (var data in datas)
                    sut.Enqueue(data);

                sut.PublishAll<TestEventData>();

                foreach (var data in datas)
                    foreach (var handler in handlers)
                        Mock.Get(handler)
                            .Verify(h => h.OnEventHandler(data), Times.Once);
            } 

            private class TestHandler : IEventHandler<TestEventData>
            {

                public void OnEventHandler(TestEventData eventData)
                {
                    throw new System.NotImplementedException();
                }
            }

            private IEnumerable<TEvent> GetData<TEvent>(int numberOfDatas)
                where TEvent : ApiEventData
            {
                for (int i = 0; i < numberOfDatas; i++)
                    yield return ObjectMother.Create<TEvent>();
            }
        }
    }
}
