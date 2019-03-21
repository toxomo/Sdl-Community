<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
  <xsl:template match="/">
    <html>
			<style>
				.Title{
				font-family:verdana;
				color: #6E7E82;
				font-size:130%;
				}
			</style>
			<head>
				<p class="Title">Signoff Verify Settings</p>
			</head>
      <body>
				<b><xsl:text>Project: </xsl:text></b>	<xsl:value-of select="//ProjectInformation/Project/@Name"/>
				<br></br>
				
				<b><xsl:text>Source Language: </xsl:text>	</b> <xsl:value-of select="//ProjectInformation/SourceLanguage/@DisplayName"/>
				<br></br>
				
				<xsl:for-each select="//ProjectInformation/TargetLanguages/TargetLanguage">
					<b><xsl:text>Target Language: </xsl:text></b> <xsl:value-of select="DisplayName"/>
					<br></br>
				</xsl:for-each>
				
				<b><xsl:text>Run at (project level): </xsl:text></b>	<xsl:value-of select="//ProjectInformation/RunAt"/>
				<br></br>

				<xsl:for-each select="//ProjectInformation/TranslationMemories/TranslationMemory">
					<b><xsl:text>Translation Memory: </xsl:text></b> <xsl:value-of select="Name"/>
					<br></br>
				</xsl:for-each>

				<xsl:for-each select="//ProjectInformation/Termbases/Termbase">
					<b><xsl:text>Termbase: </xsl:text></b> <xsl:value-of select="Name"/>
					<br></br>
				</xsl:for-each>

				<b><xsl:text>RegEx rules: </xsl:text></b> <xsl:value-of select="//ProjectInformation/RegExRules"/>
				<br></br>
				<b><xsl:text>Check RegEx: </xsl:text></b> <xsl:value-of select="//ProjectInformation/CheckRegEx"/>
				<br></br>

				<b><xsl:text>QA Checker: </xsl:text></b>	<xsl:value-of select="//ProjectInformation/QAChecker"/>
				<br></br><br></br><br></br>
		
			 <b><xsl:text>Language Files </xsl:text></b>
			 <br></br>
			 <br></br>
  		 <table>
          <tr>
            <th>Name</th>
            <th>Target language</th>
            <th>Run at</th>
            <th>Phase</th>
            <th>Is current assigned</th>
            <th>Assignees number</th>
            <th>Number Verifier</th>
          </tr>
          <xsl:for-each select="//ProjectInformation/LanguageFiles/LanguageFile">
            <tr>
              <td><xsl:value-of select="@Name"/></td>
              <td><xsl:value-of select="@TargetLanguage"/></td>
              <td><xsl:value-of select="@RunAt"/></td>
              <td><xsl:value-of select="Phase/@AssignedPhase"/></td>
              <td><xsl:value-of select="Phase/@IsCurrentAssignment"/></td>
              <td><xsl:value-of select="Phase/@AssigneesNumber"/></td>
              <td><xsl:value-of select="NumberVerifier/@ExecutedDate"/></td>
            </tr>
          </xsl:for-each>
        </table>
				
			</body>
    </html>
  </xsl:template>
</xsl:stylesheet>