using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Search;
using Lucene.Net.Search.Spans;

namespace QuerySamples
{
    public class QueryVisualizer
    {
        private bool _outputTermsOnly;
        private StringBuilder _outputBuilder = new StringBuilder();

        public QueryVisualizer()
            :this(false)
        {
            
        }

        public QueryVisualizer(bool outputTermsOnly)
        {
            _outputTermsOnly = outputTermsOnly;
        }

        public QueryVisualizer Process(Query query)
        {
            if (query is TermQuery)
            {
                ProcessTermQuery((TermQuery) query);
            }

            if (query is BooleanQuery)
            {
                ProcessBooleanQuery((BooleanQuery) query);
            }

            return this;
        }

        private void ProcessBooleanQuery(BooleanQuery query)
        {
            foreach (var clause in query.Clauses)
            {
                if (!_outputTermsOnly)
                {
                    _outputBuilder.Append(clause.Occur == Occur.MUST ? "+" : clause.Occur == Occur.MUST_NOT ? "-" : "");
                    _outputBuilder.Append("(");    
                }
                

                Process(clause.Query);

                if (!_outputTermsOnly)
                {
                    _outputBuilder.Append(")");
                }

            }
        }

        private void ProcessTermQuery(TermQuery query)
        {
            if (_outputTermsOnly)
            {
                _outputBuilder.AppendFormat("[{0}]", query.Term.Text);
            }
            else
            {
                _outputBuilder.AppendFormat("{0}:{1}", query.Term.Field, query.Term.Text);    
            }
            
        }

        public override string ToString()
        {
            return _outputBuilder.ToString();
        }
    }
}
