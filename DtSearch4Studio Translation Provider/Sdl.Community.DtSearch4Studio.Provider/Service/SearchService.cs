using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
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
		public SearchService()
		{

		}

		public SearchService(CultureInfo sourceLanguage, CultureInfo targetLanguage)
		{
			_sourceLanguage = sourceLanguage;
			_targetLanguage = targetLanguage;
		}
		#endregion

		#region Public Methods		
		//public List<WordItem> GetResults(string indexPath, string segment)
		//{
		//	var wordListBuilder = new WordListBuilder();
		//	wordListBuilder.OpenIndex(indexPath);
		//	wordListBuilder.ListWords(segment, 5);

		//	var words = new List<WordItem>();
		//	for (int i = 0; i < wordListBuilder.Count; ++i)
		//	{
		//		var item = new WordItem();
		//		item.MakeFromWordListBuilder(wordListBuilder, i);
		//		if (item.HitCount > 0)
		//		{
		//			words.Add(item);
		//		}
		//	}

		//	foreach (var word in words)
		//	{
		//		AddWordDetails(word, indexPath);
		//	}
		//	return words;
		//}

		//public void AddWordDetails(WordItem word, string indexPath)
		//{
		//	using (var searchResults = new dtSearch.Engine.SearchResults())
		//	{
		//		using (var searchJob = new SearchJob())
		//		{
		//			searchJob.IndexesToSearch.Add(indexPath);
		//			searchJob.Request = word.Word;
		//			searchJob.MaxFilesToRetrieve = 100;
		//			searchJob.AutoStopLimit = 100000;
		//			searchJob.SearchFlags = SearchFlags.dtsSearchTypeAllWords;
		//		    searchJob.Execute(searchResults);

		//			searchResults.GetNthDoc(searchResults.CurrentItem.DocId);
		//			// the below values will be used to display in separate columns to Translation Results window
		//			word.DocumentName = searchResults.CurrentItem?.DisplayName;
		//			word.IndexName = Path.GetFileName(indexPath);
		//		}
		//	}
		//}

		public List<WordItem> GetResults(string indexPath, string text)
		{
			var words = new List<WordItem>();
			using (var searchResults = new dtSearch.Engine.SearchResults())
			{
				using (var searchJob = new SearchJob())
				{
					searchJob.IndexesToSearch.Add(indexPath);
					searchJob.Request = text;
					searchJob.MaxFilesToRetrieve = 100;
					searchJob.AutoStopLimit = 100000;
					// dtSearch UI options/settings can be set like this-> 	searchJob.SearchFlags |= SearchFlags.dtsSearchStemming;

					var ok = searchJob.Execute(searchResults);
					if(ok)
					{
						var result = ProcessSearchResults(searchResults);
						//var hasResults = searchResults.GetNthDoc(0);
						//to do: use the currentItem to set the add in words, each wordTranslation which will be returned from the dtSearch SearchJob
					}
					else
					{
						MessageBox.Show($"No results found for searched text: {text}", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
					}					
				}
			}
			return words;
		}
		
		public SearchResult CreateSearchResult(WordItem word, string sourceSegmentText)
		{
			var searchSegment = new Segment(_sourceLanguage);
			searchSegment.Add(sourceSegmentText);
			var translationSegment = new Segment(_targetLanguage);
			translationSegment.Add(word.Word);

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

		#region Private Methods
		private string ProcessSearchResults(dtSearch.Engine.SearchResults searchResults)
		{
			//using (var fc = new FileConverter())
			//{
			//	if (SetupFileConverter(fc, searchResults, 0))
			//	{
			//		fc.Execute();
			//		if (!fc.Failed())
			//		{
			//			return fc.OutputString;
			//		}
			//	}
			//}
			return string.Empty;
		}

		//private bool SetupFileConverter(FileConverter fc, dtSearch.Engine.SearchResults results, int ordinalInResults)
		//{
		//	if (!fc.SetInputItem(results, ordinalInResults))
		//	{
		//		return false;
		//	}
		//	fc.DocTypeTag = "<!DOCTYPE html>";
		//	fc.Footer = string.Empty;
		//	//fc.Footer = "<hr><i>" + fc.InputFile + "</i>";
		//	// DocScript is used to implement hit navigation, and DocStyles defines certain
		//	// standard CSS styles to format the output.
		//	string script = Constants.DocScript.Replace("%HITCOUNT%", fc.Hits.Length.ToString());
		//	fc.HtmlHead = script + Constants.DocStyles;
		//	string baseRef = fc.InputFile;
		//	fc.BaseHRef = baseRef;
		//	fc.BeforeHit = "<span id=\"hit%%ThisHit%%\" style=\"background-color: #FFFF00;\">";
		//	fc.AfterHit = "</span>";
		//	fc.OutputToString = true;
		//	// Use the styles in DocStyles to format output
		//	fc.Flags = fc.Flags | ConvertFlags.dtsConvertUseStyles |
		//			// Update search hits if the index is out of date
		//			ConvertFlags.dtsConvertAutoUpdateSearch |
		//			// Disable JavaScript in input HTML
		//			ConvertFlags.dtsConvertRemoveScripts;
		//	fc.OutputFormat = OutputFormat.itHTML;
		//	return true;
		//}
		#endregion
	}
}