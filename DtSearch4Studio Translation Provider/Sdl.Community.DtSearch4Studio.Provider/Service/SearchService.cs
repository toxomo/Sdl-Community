using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using dtSearch.Engine;
using Sdl.Community.DtSearch4Studio.Provider.Helpers;
using Sdl.Community.DtSearch4Studio.Provider.Model;
using Sdl.Core.Globalization;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemory;

namespace Sdl.Community.DtSearch4Studio.Provider.Service
{
	public class SearchService
	{
		#region Private 
		private CultureInfo _sourceLanguage;
		private CultureInfo _targetLanguage;

		#endregion
		#region Public Properties
		public static readonly Log Log = Log.Instance;
		#endregion

		#region Constructors
		public SearchService(CultureInfo sourceLanguage, CultureInfo targetLanguage)
		{
			_sourceLanguage = sourceLanguage;
			_targetLanguage = targetLanguage;
		}
		#endregion

		#region Public Methods		
		public List<WordItem> GetResults(string indexPath, string segment)
		{
			var wordListBuilder = new WordListBuilder();
			wordListBuilder.OpenIndex(indexPath);
			wordListBuilder.ListWords(segment, 6);

			var words = new List<WordItem>();
			for (int i = 0; i < wordListBuilder.Count; ++i)
			{
				var item = new WordItem();
				item.MakeFromWordListBuilder(wordListBuilder, i);
				if (item.HitCount > 0)
				{
					words.Add(item);
				}
			}

			foreach (var word in words)
			{
				AddWordDetails(word, indexPath);
			}
			return words;
		}

		public void AddWordDetails(WordItem word, string indexPath)
		{
			using (var searchResults = new dtSearch.Engine.SearchResults())
			{
				using (var searchJob = new SearchJob())
				{
					searchJob.IndexesToSearch.Add(indexPath);
					searchJob.Request = word.Word;
					searchJob.MaxFilesToRetrieve = 100;
					searchJob.AutoStopLimit = 100000;
					searchJob.SearchFlags = SearchFlags.dtsSearchTypeAllWords;
				    searchJob.Execute(searchResults);
				
					searchResults.GetNthDoc(searchResults.CurrentItem.DocId);
					word.DocumentName = searchResults.CurrentItem?.DisplayName;
					word.IndexName = Path.GetFileName(indexPath);
				}
			}
		}

		public SearchResult CreateSearchResult(WordItem word, string sourceSegmentText)
		{
			var searchSegment = new Segment(_sourceLanguage);
			searchSegment.Add(sourceSegmentText);
			var translationSegment = new Segment(_targetLanguage);
			// this will be changed when the word.DocumentName will be added into a separate column to Translation Memory results window
			translationSegment.Add($"Translation: {word.Word}; File name: {word.DocumentName}, Index name: {word.IndexName}");

			var unit = new TranslationUnit
			{
				SourceSegment = searchSegment,
				TargetSegment = translationSegment,
				ConfirmationLevel = ConfirmationLevel.Translated,
				Origin = TranslationUnitOrigin.MachineTranslation
			};
			unit.ResourceId = new PersistentObjectToken(unit.GetHashCode(), Guid.Empty);
			var searchResult = new SearchResult(unit);

			// We do not currently support scoring, so always say that we're 25% sure on this translation.
			searchResult.ScoringResult = new ScoringResult() { BaseScore = 25 };

			return searchResult;		
		}

		#endregion
	}
}