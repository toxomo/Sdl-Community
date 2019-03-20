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
				<br></br>
			</body>
    </html>
  </xsl:template>
</xsl:stylesheet>