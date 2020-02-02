using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace Polaroider.Tests.Writer
{
    public class SnapshotsWriterTests
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
        public void SaveSnapshots()
        {
            var snapshotId = _snapshotResolver.ResloveId();

            var writer = new SnapshotWriter();

            // record the current snapshot
            var dataOne = "this is\r\na\r\ntest";
            var token = SnapshotTokenizer.Tokenize(dataOne)
                .SetMetadata(() => new {id = "one"});

            writer.Write(token, snapshotId);

            var dataTwo = "this is\r\na second\r\ntest";
            token = SnapshotTokenizer.Tokenize(dataTwo)
                .SetMetadata(() => new { id = "two" });

            writer.Write(token, snapshotId);

            // ensure there are 2 snapshots in the file
            var reader = new SnapshotReader();
            var saved = reader.Read(snapshotId);
            saved.Count().Should().Be(2);

            // match the snapshots
            dataOne.MatchSnapshot(() => new { id = "one"});
            dataTwo.MatchSnapshot(() => new { id = "two"});

            //delete file and folder
            System.IO.File.Delete(snapshotId.GetFilePath());
        }

        [Test]
        public void UpdateSnapshots()
        {
            var snapshotId = _snapshotResolver.ResloveId();

            // ensure testdata
            System.IO.File.Delete(snapshotId.GetFilePath());

            var writer = new SnapshotWriter();

            // record the current snapshot
            var dataOne = "this is\r\na\r\ntest";
            var token = SnapshotTokenizer.Tokenize(dataOne)
                .SetMetadata(() => new {id = "one"});

            writer.Write(token, snapshotId);

            var dataTwo = "this is\r\na second\r\ntest";
            token = SnapshotTokenizer.Tokenize(dataTwo)
                .SetMetadata(() => new {id = "two"});

            writer.Write(token, snapshotId);

            // ensure there are 2 snapshots in the file
            var reader = new SnapshotReader();
            var saved = reader.Read(snapshotId);
            saved.Count().Should().Be(2);



            // update a snapshot
            dataTwo = "this is\r\na updated second\r\ntest";
            token = SnapshotTokenizer.Tokenize(dataTwo)
                .SetMetadata(() => new {id = "two"});

            writer.Write(token, snapshotId);

            // ensure there are 2 snapshots in the file
            saved = reader.Read(snapshotId);
            saved.Count().Should().Be(2);

            // match the snapshots
            dataOne.MatchSnapshot(() => new {id = "one"});
            dataTwo.MatchSnapshot(() => new {id = "two"});


            dataOne = "this is\r\na updated first\r\ntest";
            token = SnapshotTokenizer.Tokenize(dataOne)
                .SetMetadata(() => new {id = "one"});


            writer.Write(token, snapshotId);

            // ensure there are 2 snapshots in the file
            saved = reader.Read(snapshotId);
            saved.Count().Should().Be(2);

            // match the snapshots
            dataOne.MatchSnapshot(() => new {id = "one"});
            dataTwo.MatchSnapshot(() => new {id = "two"});

            //delete file and folder
            System.IO.File.Delete(snapshotId.GetFilePath());
        }
    }
}
