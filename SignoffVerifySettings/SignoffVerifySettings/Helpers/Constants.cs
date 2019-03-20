namespace Sdl.Community.SignoffVerifySettings.Helpers
{
	public static class Constants
	{
		// file phases
		public static readonly string PreparationPhase = "LanguageFileServerAssignmentsSettings_Preparation";
		public static readonly string TranslationPhase = "LanguageFileServerAssignmentsSettings_Translation";
		public static readonly string ReviewPhase = "LanguageFileServerAssignmentsSettings_Review";
		public static readonly string FinalisationPhase = "LanguageFileServerAssignmentsSettings_Finalisation";

		public static readonly string IsCurrentAssignment = "IsCurrentAssignment";

		public static readonly string NumberVerifier2017CommunityPath = @"SDL Community\Number Verifier\Number Verifier 2017";
		public static readonly string NumberVerifierSettingsJson = "NumberVerifierSettings.json";

		public static readonly string ReportName = "SignoffVerifySettings";

		// Report messages
		public static readonly string NoVerificationRun = "No 'Verify Files' batch task was executed.";
		public static readonly string NoTranslationMemory = "No translation memory set";
		public static readonly string NoTermbase = "No termbase set";
		public static readonly string RegExRulesApplied = "RegEx rules were applied";
		public static readonly string NoRegExRules = "No RegEx rules were applied";
		public static readonly string QAChekerExecuted = "QA Checker had executed";
		public static readonly string NoQAChekerExecuted = "QA Checker did not executed";
	}
}