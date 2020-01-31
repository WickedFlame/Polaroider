using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Polaroider.Tests.Writer
{
    public class SnapshotWriterTests
    {
        private SnapshotIdResolver _snapshotResolver;

        [SetUp]
        public void Setup()
        {
            _snapshotResolver = new SnapshotIdResolver();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            var id = _snapshotResolver.ResloveId();
            System.IO.Directory.Delete(System.IO.Path.GetDirectoryName(id.GetFilePath()));
        }

        [Test]
        public void SaveSnapshot()
        {
            var data = "this is\r\na\r\ntest";

            var snapshotId = _snapshotResolver.ResloveId();
            var token = SnapshotTokenizer.Tokenize(data);

            var writer = new SnapshotWriter();
            writer.Write(token, snapshotId);

            //reload snapshot to compare
            data.MatchSnapshot();

            //delete file and folder
            System.IO.File.Delete(snapshotId.GetFilePath());
        }

        [Test]
        public void UpdateSnapshot()
        {
            var snapshotId = _snapshotResolver.ResloveId();

            var writer = new SnapshotWriter();

            // record the current snapshot
            var data = "this is\r\na\r\ntest";
            var token = SnapshotTokenizer.Tokenize(data);
            writer.Write(token, snapshotId);

            // ensure the data matches
            data.MatchSnapshot();

            data = "this is\r\nnew\r\ndata";
            token = SnapshotTokenizer.Tokenize(data);
            writer.Write(token, snapshotId);

            // ensure the data is updated
            data.MatchSnapshot();

            //delete file and folder
            System.IO.File.Delete(snapshotId.GetFilePath());
        }
    }
}
