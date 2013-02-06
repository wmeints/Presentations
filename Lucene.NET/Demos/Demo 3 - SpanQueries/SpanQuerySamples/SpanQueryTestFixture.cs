using System;
using System.Diagnostics;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Search.Spans;
using Lucene.Net.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Version = Lucene.Net.Util.Version;

namespace SpanQuerySamples
{
    [TestClass]
    public class SpanQueryTestFixture
    {
        private RAMDirectory _directory;
        private StandardAnalyzer _analyzer;

        [TestMethod]
        public void BasicSpanQuery()
        {
            Initialize();

            // Builds a query that search for two sets of terms that should 
            // be close to eachother in the text that is being indexed.
            // Please note, the distance between the two sets doesn't need to be short.
            // As long as there's a quick fox of a non-descript color and a lazy dog we're happy.
            SpanNearQuery nearQuery = new SpanNearQuery(
                new[]
                {
                    // The terms quick and fox must be close to eachother
                    new SpanNearQuery(new[]
                    {
                        new SpanTermQuery(new Term("text","quick")),
                        new SpanTermQuery(new Term("text","fox")) 
                    },2,true),

                    // The terms lazy and dog should also be close to eachother.
                    new SpanNearQuery(new[]
                    {
                        new SpanTermQuery(new Term("text","lazy")),
                        new SpanTermQuery(new Term("text","dog")) 
                    },1,true)
                },Int16.MaxValue,false);


            using (var reader = IndexReader.Open(_directory, true))
            {
                var searcher = new IndexSearcher(reader);
                var results = searcher.Search(nearQuery,null,Int16.MaxValue);

                foreach (var doc in results.ScoreDocs)
                {
                    var document = reader.Document(doc.Doc);

                    Trace.WriteLine(string.Format("{0} (rank : {1})",document.Get("text"),doc.Score));
                }
            }
        }

        private void Initialize()
        {
            _directory = new RAMDirectory();
            _analyzer = new StandardAnalyzer(Version.LUCENE_30);

            using (var writer = new IndexWriter(_directory, _analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                StoreDocument("The lazy fox jumps over the quick brown dog", writer);
                StoreDocument("The quick brown fox jumps over the lazy dog", writer);

                writer.Optimize();
                writer.Flush(true, true, true);
            }
        }

        private void StoreDocument(string text, IndexWriter writer)
        {
            var document = new Document();
            document.Add(new Field("text",text,Field.Store.YES,Field.Index.ANALYZED));

            writer.AddDocument(document);
        }
    }
}
