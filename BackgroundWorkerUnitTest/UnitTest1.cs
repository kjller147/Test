using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestBackgroundWorkerForm;
using TestBackgroundWorkerForm.DataLayer;
using System.Data.EntityClient;
using System.Data.Entity;
using TestBackgroundWorkerForm.Models;
using System.Linq;

namespace BackgroundWorkerUnitTest
{
    [TestClass]
    public class UnitTest1
    {       

        [TestMethod]
        public void AddToDb()
        {
            var tracker = new StopwatchTracker(DateTime.Now);

            var ctx = new StopTrackerDbContext();

            ctx.stopwatchTrackers.Add(tracker);
            ctx.SaveChanges();

            Assert.IsNotNull(tracker.Id);
            ctx.Dispose();
        }

        [TestMethod]
        public void GetData()
        {
            var ctx = new StopTrackerDbContext();
            
            var newTracker = new StopwatchTracker(DateTime.Now);
            ctx.stopwatchTrackers.Add(newTracker);
            ctx.SaveChanges();

            var trackers = ctx.stopwatchTrackers.ToList();

            Assert.AreNotEqual(0, trackers.Count());
            ctx.Dispose();
        }

        [TestMethod]
        public void DeleteData()
        {
            var ctx = new StopTrackerDbContext();

            var trackers = ctx.stopwatchTrackers.ToList();

            ctx.stopwatchTrackers.RemoveRange(trackers);
            ctx.SaveChanges();

            var result = ctx.stopwatchTrackers.ToList();

            Assert.AreEqual(0, result.Count());
            ctx.Dispose();
        }
    }
}
