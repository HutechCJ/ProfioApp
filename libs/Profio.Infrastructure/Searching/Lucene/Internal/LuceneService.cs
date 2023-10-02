using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;

namespace Profio.Infrastructure.Searching.Lucene.Internal;

public sealed class LuceneService<T> : ILuceneService<T> where T : class
{
  private readonly Analyzer _analyzer;
  private readonly IndexSearcher _indexSearcher;
  private readonly IndexWriter _indexWriter;
  private readonly QueryParser _queryParser;

  public LuceneService()
  {
    _analyzer = new VietnameseAnalyzer(LuceneVersion.LUCENE_48);
    _indexWriter = new(FSDirectory.Open("LuceneIndex"),
      new(LuceneVersion.LUCENE_48, _analyzer));
    _indexSearcher = new(_indexWriter.GetReader(true));
    _queryParser = new(LuceneVersion.LUCENE_48, "content", _analyzer);
  }

  public bool IsExistIndex(T item)
    => _indexSearcher
      .Search(new QueryParser(LuceneVersion.LUCENE_48, "id", _analyzer)
        .Parse(item.ToString()), 1)
      .ScoreDocs
      .Length > 0;

  public Dictionary<string, List<Document>> GetData(List<T> list)
  {
    var propertyIndex = new Dictionary<string, List<Document>>();
    foreach (var data in list)
    foreach (var dummy in data.GetType().GetProperties())
    foreach (var property in data.GetType().GetProperties())
    {
      if (!propertyIndex.ContainsKey(property.Name))
        propertyIndex.Add(property.Name, new());

      var value = property.GetValue(data, null);

      if (value is null) continue;

      var document = new Document
        { new StringField(property.Name, value.ToString(), Field.Store.YES) };

      propertyIndex[property.Name].Add(document);
    }

    return propertyIndex;
  }

  public IEnumerable<Document> Search(string query, int maxResults)
  {
    var booleanQuery = new BooleanQuery
    {
      { _queryParser.Parse(query), Occur.SHOULD },
      { new FuzzyQuery(new("content", query), 2), Occur.SHOULD }
    };

    var hits = _indexSearcher.Search(booleanQuery, maxResults).ScoreDocs;

    return hits.Select(hit => _indexSearcher.Doc(hit.Doc));
  }

  public void ClearAll() => _indexWriter.DeleteAll();

  public void Index(List<T> data, Lucene options)
  {
    var document = GetData(data);
    var docs = document.SelectMany(item
        => item.Value.Select(doc
          => new TextField(item.Key, doc.ToString(), Field.Store.YES)))
      .Select(field => new Document { field })
      .ToList();

    switch (options)
    {
      case Lucene.Create:
        _indexWriter.AddDocuments(docs);
        break;

      case Lucene.Update:
        _indexWriter.UpdateDocuments(new("id", data.ToString()), docs);
        break;

      case Lucene.Delete:
        _indexWriter.DeleteDocuments(new Term("id", "1"));
        break;

      default:
        throw new ArgumentOutOfRangeException(nameof(options), options, "Invalid option!");
    }

    _indexWriter.Flush(true, true);
  }
}
