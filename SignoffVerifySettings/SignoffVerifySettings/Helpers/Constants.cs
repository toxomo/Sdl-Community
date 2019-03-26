namespace Sdl.Community.SignoffVerifySettings.Helpers
{
	public static class Constants
	{
		// file phases
		public static readonly string PreparationPhase = "LanguageFileServerAssignmentsSettings_Preparation";
		public static readonly string TranslationPhase = "LanguageFileServerAssignmentsSettings_Translation";
		public static readonly string ReviewPhase = "LanguageFileServerAssignmentsSettings_Review";
		public static readonly string FinalisationPhase = "LanguageFileServerAssignmentsSettings_Finalisation";

		public static readonly string NumberVerifier2017CommunityPath = @"SDL Community\Number Verifier\Number Verifier 2017";
		public static readonly string NumberVerifierSettingsJson = "NumberVerifierSettings.json";
		
		// Report values
		public static readonly string ReportName = "SignoffVerifySettings";
		public static readonly string ProjectInformation = "ProjectInformation";
		public static readonly string Project = "Project";
		public static readonly string Name = "Name";
		public static readonly string StudioVersion = "StudioVersion";
		public static readonly string Zero = "0";
		public static readonly string QASettingName = "QASettingName";
		public static readonly string SourceLanguage = "SourceLanguage";
		public static readonly string DisplayName = "DisplayName";
		public static readonly string TargetLanguages = "TargetLanguages";
		public static readonly string TargetLanguage = "TargetLanguage";
		public static readonly string RunAt = "RunAt";
		public static readonly string TranslationMemories = "TranslationMemories";
		public static readonly string TranslationMemory = "TranslationMemory";
		public static readonly string Termbases = "Termbases";
		public static readonly string Termbase = "Termbase";
		public static readonly string RegExRules = "RegExRules";
		public static readonly string CheckRegEx = "CheckRegEx";
		public static readonly string QAChecker = "QAChecker";
		public static readonly string LanguageFiles = "LanguageFiles";
		public static readonly string LanguageFile = "LanguageFile";
		public static readonly string LanguagePair = "LanguagePair";
		public static readonly string AssignedPhase = "AssignedPhase";
		public static readonly string IsCurrentAssignment = "IsCurrentAssignment";
		public static readonly string AssigneesNumber = "AssigneesNumber";
		public static readonly string Phases = "Phases";
		public static readonly string Phase = "Phase";
		public static readonly string NumberVerifier = "NumberVerifier";
		public static readonly string ExecutedDate = "ExecutedDate";
		public static readonly string VerificationSettings = "VerificationSettings";
		public static readonly string VerificationSetting = "VerificationSetting";
		public static readonly string FileName = "FileName";
		public static readonly string ApplicationVersion = "ApplicationVersion";
		public static readonly string ExecutedDateTime = "ExecutedDateTime";


		// Report messages
		public static readonly string NoVerificationRun = "No 'Verify Files' batch task was run.";
		public static readonly string NoTranslationMemory = "No translation memory set";
		public static readonly string NoTermbase = "No termbase set";
		public static readonly string RegExRulesApplied = "RegEx rules were applied";
		public static readonly string NoRegExRules = "No RegEx rules were applied";
		public static readonly string QAChekerExecuted = "Verification message reported";
		public static readonly string NoQAChekerExecuted = "No verification message reported";
		public static readonly string NoQAVerificationSettings = "No QA Verification Settings enabled";
		public static readonly string NoPhaseAssigned = "No phase assigned";
		public static readonly string NoUserAssigned = "No user(s) assigned";
		public static readonly string False = "False";
		public static readonly string True = "True";
		public static readonly string Enabled = "Enabled";
		public static readonly string Disabled = "Disabled";

		public static readonly string NoNumberVerifierExecuted = "Number Verifier did not executed";
	}
}